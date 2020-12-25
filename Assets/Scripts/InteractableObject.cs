using System;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class InteractableObject : NetworkedBehaviour, ILootable
{
    public static Action<NetworkedObject> OnDestroy;
    private NetworkedObject _networkedObj;
    [SerializeField] private InteractableType _interactableType;
    public InteractableType InteractableType => _interactableType;
    [SerializeField] private float _interactionTime;
    public float InteractionTime => _interactionTime;
    public InventoryManager TargetInventory { get; set; }
    [SerializeField] private LootType _lootType;
    public LootType LootType => _lootType;


    private void Awake() => _networkedObj = GetComponent<NetworkedObject>();
    private void OnDisable() => OnDestroy?.Invoke(_networkedObj);
}
