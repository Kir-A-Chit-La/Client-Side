using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;

public class ClientHandle
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _netID = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.Instance.netID = _netID;

        ClientSend.WelcomeReceived();
    }

    public static void RegistrationResult(Packet _packet)
    {
        bool _isRegistered = _packet.ReadBool();
        if(_isRegistered)
        {
            Account.Current.SetID(_packet.ReadInt());
            Account.Current.SetUsername(_packet.ReadString());
            Account.Current.SetAvatar(_packet.ReadInt());

            DataManager.Instance.SaveUserData();
            GameManager.Instance.LoadScene((int)Scenes.MAIN_MENU);
        }
        else
        {
            StartSceneController.Instance.Result("This username is already in use");
        }
    }

    public static void VerificationResult(Packet _packet)
    {
        bool _isVerificated = _packet.ReadBool();
        if(_isVerificated)
        {
            Account.Current.SetVerificationStatus(_isVerificated);
            MainMenuController.Instance._password.interactable = false;
            MainMenuController.Instance._confirmPassword.interactable = false;
            MainMenuController.Instance._accountSettingsMessage.text = "Success!";
        }
        else
        {
            MainMenuController.Instance._accountSettingsMessage.text = "Something went wrong. Please, try again";
        }

    }

    public static void CharacterCreationResult(Packet _packet)
    {
        bool _isCreated = _packet.ReadBool();
        if(_isCreated)
        {
            int id = _packet.ReadInt();
            Account.Current.AddCharacter(new Character(id, _packet.ReadString(), _packet.ReadString(), _packet.ReadVector3(), _packet.ReadQuaternion()));
            MainMenuController.Instance.CharacterCreationResult(_isCreated, id);
        }
        else
        {
            MainMenuController.Instance.CharacterCreationResult(_isCreated, 0);
        }
        
    }

    public static void AccountData(Packet _packet)
    {
        bool _success = _packet.ReadBool();
        if(_success)
        {
            Account.Current.SetAvatar(_packet.ReadInt());
            Account.Current.SetVerificationStatus(_packet.ReadBool());
            int _charCount = _packet.ReadInt();
            if(_charCount > 0)
            {
                Account.Current.SetCharacters(_packet.ReadCharacters());
            }
            GameManager.Instance.LoadScene((int)Scenes.MAIN_MENU);
        }
        else
        {
            Debug.LogError("Failed to retrieve data.");
        }
    }

    public static void AccountSalt(Packet _packet)
    {
        bool _isExists = _packet.ReadBool();
        if(_isExists)
        {
            byte[] _salt = Hasher.Decode(_packet.ReadString());
            byte[] _hash = Hasher.HashPassword(StartSceneController.Instance._logInPasswordInputField.text, _salt);

            ClientSend.LogIn(StartSceneController.Instance._logInUsernameInputField.text, Hasher.Encode(_hash));
        }
        else
        {
            StartSceneController.Instance.Result("No verificated user found with given username or password");
            StartSceneController.Instance._logInUsernameInputField.interactable = true;
            StartSceneController.Instance._logInPasswordInputField.interactable = true;
        }
    }

    public static void LogInResult(Packet _packet)
    {
        bool _success = _packet.ReadBool();
        if(_success)
        {
            Account.Current.SetID(_packet.ReadInt());
            Account.Current.SetUsername(StartSceneController.Instance._logInUsernameInputField.text);
            Account.Current.SetAvatar(_packet.ReadInt());
            Account.Current.SetVerificationStatus(true);
            int _charCount = _packet.ReadInt();
            if(_charCount > 0)
            {
                Account.Current.SetCharacters(_packet.ReadCharacters());
            }
            DataManager.Instance.SaveUserData();
            GameManager.Instance.LoadScene((int)Scenes.MAIN_MENU);
        }
        else
        {
            StartSceneController.Instance.Result("No verificated user found with given username or password");
            StartSceneController.Instance._logInUsernameInputField.interactable = true;
            StartSceneController.Instance._logInPasswordInputField.interactable = true;
        }
    }
}
