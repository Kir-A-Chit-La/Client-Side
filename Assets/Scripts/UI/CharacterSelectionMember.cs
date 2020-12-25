using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectionMember : MonoBehaviour
{
    public int characterID { get; private set; }
    public string characterName { get; private set; }
    public string characterGender { get; private set; }
    [SerializeField] private Text _name;
    [SerializeField] private Text _gender;
    [SerializeField] private GameObject _selectionToggle;
    [SerializeField] private Button _button;

    public void Initialize(Character character)
    {
        characterID = character.ID;
        characterName = character.Name;
        characterGender = character.Gender;

        _name.text = characterName;
        _gender.text = characterGender;
        
        _button.onClick.AddListener(() => MainMenuController.Instance.SelectCharacter(this));
    }
    public void Select() => _selectionToggle.SetActive(true);
    public void Unselect() => _selectionToggle.SetActive(false);
}
