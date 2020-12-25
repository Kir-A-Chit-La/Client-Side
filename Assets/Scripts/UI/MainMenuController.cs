using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;
    [Header("Main Menu Objects")]
    [SerializeField] private Text _usernameText;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _accountSettings;
    [SerializeField] private Transform _characterListHolder;
    [SerializeField] private GameObject _characterSelectionMember;
    private CharacterSelectionMember _selectedCharacter = null;
    [Header("Character Creation")]
    [SerializeField] private GameObject _characterCreationMenu;
    [SerializeField] private InputField _characterCreationMenuNameInputField;
    private string _characterCreationMenuGender;
    [SerializeField] private Text _characterCreationMenuMessage;
    [Header("Account Settings")]
    [SerializeField] private GameObject _accountSettingsLogOutPopUp;
    [SerializeField] private Text _accountSettingsUsernameText;
    [SerializeField] private Image _accountSettingsAvatarImage;
    public InputField _password;
    public InputField _confirmPassword;
    public Text _accountSettingsMessage;
    [Header("Avatars")]
    [SerializeField] private Sprite[] _avatars;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }

        InitializeMenu();
    }

    public void OpenSettings() => _settings.SetActive(true);

    public void OpenAccountSettings() => _accountSettings.SetActive(true);

    public void OpenCharacterCreation() => _characterCreationMenu.SetActive(true);

    public void CloseSettings(){}

    public void CloseAccountSettings() => ClientSend.ChangeAvatarRequest(Account.Current.ID, Account.Current.Avatar);

    public void Play() => GameManager.Instance.Play(_selectedCharacter.characterID);

    public void ChangeAvatar()
    {
        int avatarID = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        Account.Current.SetAvatar(avatarID);
        _avatarImage.sprite = _avatars[avatarID];
        _accountSettingsAvatarImage.sprite = _avatars[avatarID];
    }

    public void ChangeGender() => _characterCreationMenuGender = EventSystem.current.currentSelectedGameObject.name;

    public void CreateCharacter()
    {
        if(_characterCreationMenuNameInputField.text.Length >= 6)
        {
            if(_characterCreationMenuGender != "")
            {
                ClientSend.CharacterCreationRequest(Account.Current.ID, _characterCreationMenuNameInputField.text, _characterCreationMenuGender);
            }
            else
            {
                _characterCreationMenuMessage.text = "You should select gender for your character";
            }
        }
        else
        {
            _characterCreationMenuMessage.text = "Name must be at least 6 characters and less then 12";
        }
    }

    public void CharacterCreationResult(bool isCreated, int id)
    {
        if(isCreated)
        {
            _characterCreationMenu.GetComponentInChildren<UITweener>().Disable();
            GameObject character = Instantiate(_characterSelectionMember);
            character.transform.SetParent(_characterListHolder, false);
            character.GetComponent<CharacterSelectionMember>().Initialize(Account.Current.Characters[id]);
        }
        else
        {
            _characterCreationMenuMessage.text = "This name is already in use. Please, try another one";
        }
    }

    public void SelectCharacter(CharacterSelectionMember character)
    {
        if(_selectedCharacter == character)
            return;
        
        _selectedCharacter.Unselect();
        _selectedCharacter = character;
        _selectedCharacter.Select();
    }

    public void LogOutPopUp() => _accountSettingsLogOutPopUp.SetActive(true);

    public void LogOut()
    {
        DataManager.Instance.DeleteUserData();
        GameManager.Instance.RestartGame();
    }

    public void Verificate()
    {
        if(_password.interactable && _confirmPassword.interactable)
        {
            if(_password.text.Length >= 6 && _confirmPassword.text.Length >= 6)
            {
                if(_password.text == _confirmPassword.text)
                {
                    ClientSend.VerificationRequest(Account.Current.ID, _password.text);
                }
                else
                {
                    _accountSettingsMessage.text = "_passwords does not match";
                }
            }
            else
            {
                _accountSettingsMessage.text = "_password must be at least 6 characters";
            }
        }
        else
        {
            _accountSettingsMessage.text = "Your account already verificated";
        }
    }

    private void InitializeMenu()
    {
        _usernameText.text = Account.Current.Username;
        _accountSettingsUsernameText.text = Account.Current.Username;
        _avatarImage.sprite = _avatars[Account.Current.Avatar];
        _accountSettingsAvatarImage.sprite = _avatars[Account.Current.Avatar];
        if(Account.Current.IsVerificated)
        {
            _password.interactable = false;
            _confirmPassword.interactable = false;
        }
        if(Account.Current.Characters.Count > 0)
        {
            foreach(var character in Account.Current.Characters)
            {
                GameObject _character = Instantiate(_characterSelectionMember);
                _character.transform.SetParent(_characterListHolder, false);
                _character.GetComponent<CharacterSelectionMember>().Initialize(character.Value);
            }
            _selectedCharacter = _characterListHolder.GetComponentInChildren<CharacterSelectionMember>();
            _selectedCharacter.Select();
        }
    }
}
