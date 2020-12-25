using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account : MonoBehaviour
{
    // FIND WAY TO SAVE CHARACTERS DATABASE ID TO ACCESS TO THEM ACCORDING TO IT
    public static Account Current;

    public int ID { get; private set; }
    public string Username { get; private set; }
    public int Avatar { get; private set; }
    public bool IsVerificated { get; private set; }
    public Dictionary<int, Character> Characters {get; private set; } = new Dictionary<int, Character>();

    public void SetID(int id)
    {
        ID = id;
    }
    public void SetUsername(string username)
    {
        Username = username;
    }
    public void SetAvatar(int avatar)
    {
        Avatar = avatar;
    }
    public void SetVerificationStatus(bool isVerificated)
    {
        IsVerificated = isVerificated;
    }
    public void SetCharacters(Dictionary<int, Character> characters)
    {
        Characters = characters;
    }
    public void AddCharacter(Character character)
    {
        Characters.Add(character.ID, character);
    }

    private void Awake()
    {
        if(Current == null)
        {
            Current = this;
        }
        else if(Current != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }
}
