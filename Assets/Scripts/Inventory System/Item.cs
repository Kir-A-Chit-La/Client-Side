﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Custom/Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string id;
    public string Id => id;
    public string Name;
    public string Description;
    public float Weight;
    [Range(1, 99)] public int MaximumStackSize = 1;
    public Sprite Preview;
    public GameObject Prefab;

    public virtual Item GetCopy() => this;
    public virtual void Destroy() {}
    private void OnValidate()
    {
        id = Hasher.GetStableHash64(Name).ToString();
    }
}
