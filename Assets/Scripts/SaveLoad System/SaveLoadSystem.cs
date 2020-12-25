using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoadSystem
{
    public static void SaveData(Account account)
    {
        BinaryFormatter _formatter = new BinaryFormatter();
        string _path = Application.persistentDataPath + "/account.dat";

        FileStream _stream = new FileStream(_path, FileMode.Create);

        UserData _data = new UserData(account);

        _formatter.Serialize(_stream, _data);
        _stream.Close();
    }

    public static UserData LoadData()
    {
        string _path = Application.persistentDataPath + "/account.dat";
        if((File.Exists(_path)))
        {
            BinaryFormatter _formatter = new BinaryFormatter();
            FileStream _stream = new FileStream(_path, FileMode.Open);

            UserData _data = _formatter.Deserialize(_stream) as UserData;
            _stream.Close();
            return _data;
        }
        else
        {
            Debug.Log($"Save file not found in {_path}");
            return null;
        }
    }

    public static void DeleteData()
    {
        string _path = Application.persistentDataPath + "/account.dat";
        File.Delete(_path);
    }
}
