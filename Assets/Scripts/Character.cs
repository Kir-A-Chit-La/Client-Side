using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Gender { get; private set; }
    public Vector3 Position { get; private set; }
    public Quaternion Rotation { get; private set; }

    public Character(int id, string characterName, string characterGender, Vector3 characterPosition, Quaternion characterRotation)
    {
        ID = id;
        Name = characterName;
        Gender = characterGender;
        Position = characterPosition;
        Rotation = characterRotation;
    }
}
