using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    public static StartSceneController Instance;
    [Header("Registration PopUp")]
    [SerializeField] private GameObject _registerPopUp;
    [SerializeField] private InputField _registerUsernameInputField;
    [SerializeField] private Button _registerButton;
    [Header("Logging In PopUp")]
    [SerializeField] private GameObject _logInPopUp;
    public InputField _logInUsernameInputField;
    public InputField _logInPasswordInputField;
    [SerializeField] private Button _logInButton;
    [Header("Results PopUp")]
    [SerializeField] private GameObject _resultsPopUp;
    [SerializeField] private Text _resultsText;

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
    }

    public void StartScene()
    {
        Register();
    }

    public void Register()
    {
        _registerPopUp.SetActive(true);
        _resultsText.text = "";
    }

    public void LogIn()
    {
        _logInPopUp.SetActive(true);
    }

    public void RegisterRequest()
    {
        ClientSend.RegisterRequest(_registerUsernameInputField.text);
    }

    public void LogInRequest()
    {
        _logInUsernameInputField.interactable = false;
        _logInPasswordInputField.interactable = false;
        ClientSend.LogInRequest(_logInUsernameInputField.text);
    }

    public void Result(string result)
    {
        _resultsText.text = result;
        _resultsPopUp.SetActive(true);
    }

    public void RegisterVerifyInput()
    {
        if(_registerUsernameInputField.text.Length >= 6)
        {
            _registerButton.interactable = true;
        }
        else
        {
            _registerButton.interactable = false;
        }
    }

    public void LogInVerifyInput()
    {
        if(_logInUsernameInputField.text.Length >= 6 && _logInPasswordInputField.text.Length >= 6)
        {
            _logInButton.interactable = true;
        }
        else
        {
            _logInButton.interactable = false;
        }
    }
}
