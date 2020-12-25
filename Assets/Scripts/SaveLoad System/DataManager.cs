using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int ID;
    public string Username;

    public UserData(Account account)
    {
        ID = account.ID;
        Username = account.Username;
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public bool _dataExists;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SaveUserData()
    {
        SaveLoadSystem.SaveData(Account.Current);
    }

    public void LoadUserData()
    {
        UserData _data = SaveLoadSystem.LoadData();
        if(_data != null)
        {
            _dataExists = true;
            Account.Current.SetID(_data.ID);
            Account.Current.SetUsername(_data.Username);
        }
        else
        {
            _dataExists = false;
            Debug.Log("No user data found. Starting clear client.");
        }
    }

    public void DeleteUserData()
    {
        SaveLoadSystem.DeleteData();
    }
}
