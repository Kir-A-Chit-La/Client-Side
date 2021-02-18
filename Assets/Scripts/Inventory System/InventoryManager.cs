using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MLAPI;
using MLAPI.NetworkedVar.Collections;
using System.Collections.Generic;
using MLAPI.Messaging;

public class InventoryManager : NetworkedBehaviour
{
    private PlayerStats _stats;
    private PlayerInputGrabber _input;
    [SerializeField] private ItemDatabase _itemDatabase;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Equipment _equipment;
    [SerializeField] private StatsPanel _statsPanel;
    [SerializeField] private Image _draggedItemImage;
    [SerializeField] private ItemSlot _selectedItemSlot;
    [SerializeField] private TMP_Text _selectedItemName;
    [SerializeField] private TMP_Text _selectedItemDescription;
    private BaseItemSlot _draggedItemSlot;
    private int _draggedItemSlotIndex;
    private NetworkedList<ItemSlotNetworkedData> _networkedInventoryData = new NetworkedList<ItemSlotNetworkedData>();
    private NetworkedList<ItemSlotNetworkedData> _networkedEquipmentData = new NetworkedList<ItemSlotNetworkedData>();

    public void Init(PlayerStats stats, PlayerInputGrabber input)
    {
        _stats = stats;
        _input = input;
        // Create static class to handle custom class serialization and call methods from him
        ItemSlotNetworkedData.RegisterSerialization();

        _statsPanel.SetStats(_stats.Strength, _stats.Armour, _stats.FreezeResistance, _stats.HeatResistance);
        _statsPanel.UpdateStatValues();
        Subscribe();
        _inventory.Init();
        _equipment.Init();
        UpdateInventoryMaxSlots();

        _networkedInventoryData.OnListChanged += changeEvent => OnNetworkedListChange(changeEvent.index, changeEvent.value, _inventory.itemSlots);
        _networkedEquipmentData.OnListChanged += changeEvent => OnNetworkedListChange(changeEvent.index, changeEvent.value, _equipment.equipmentSlots);
    }
    private void OnNetworkedListChange(int index, ItemSlotNetworkedData networkedItemSlot, IList<ItemSlot> list)
    {
        ItemSlot itemSlot = list[index];
        UpdateInventorySlots(itemSlot, networkedItemSlot);
    }
    private void UpdateInventorySlots(ItemSlot itemSlot, ItemSlotNetworkedData networkedItemSlot)
    {
        if(networkedItemSlot != null)
        {
            if(itemSlot is EquipmentSlot)
            {
                if(itemSlot.Item != null)
                    (itemSlot.Item as EquippableItem).Unequip(_stats);
                
                SetItemFromDatabase(itemSlot, networkedItemSlot);
                (itemSlot.Item as EquippableItem).Equip(_stats);
            }
            else
            {
                SetItemFromDatabase(itemSlot, networkedItemSlot);
            }
        }
        else
        {
            if(itemSlot is EquipmentSlot)
                if(itemSlot.Item != null)
                    (itemSlot.Item as EquippableItem).Unequip(_stats);
            
            itemSlot.Item = null;
        }
        _statsPanel.UpdateStatValues();
        UpdateInventoryMaxSlots();
    }
    private void SetItemFromDatabase(ItemSlot itemSlot, ItemSlotNetworkedData networkedItemSlot)
    {
        //Debug.Log("Setting item from database");
        itemSlot.Item = _itemDatabase.GetItemCopy(networkedItemSlot.itemId);
        itemSlot.Amount = networkedItemSlot.itemAmount;
    }
    private void SelectItem(int index, BaseItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            _selectedItemSlot.Item = itemSlot.Item;
            _selectedItemName.text = itemSlot.Item.Name;
            _selectedItemDescription.text = itemSlot.Item.Description;
        }

    }
    private void UpdateInventoryMaxSlots() => _inventory.MaxSlots = (int)_stats.BackpackSize.Value;
    private void BeginDrag(int index, BaseItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            _draggedItemSlot = itemSlot;
            _draggedItemSlotIndex = index;
            _draggedItemImage.sprite = itemSlot.Item.Preview;
            _draggedItemImage.transform.position = _input.GetPointerPosition();
            _draggedItemImage.gameObject.SetActive(true);
        }
    }
    private void Drag(int index, BaseItemSlot itemSlot)
    {
        _draggedItemImage.transform.position = _input.GetPointerPosition();
    }
    private void EndDrag(int index, BaseItemSlot itemSlot)
    {
        _draggedItemSlot = null;
        _draggedItemImage.gameObject.SetActive(false);
    }
    private void Drop(int index, BaseItemSlot dropItemSlot)
    {
        if(_draggedItemSlot == null)
            return;
        
        if(dropItemSlot.CanAddStack(_draggedItemSlot.Item) || (dropItemSlot.CanReceiveItem(_draggedItemSlot.Item) && _draggedItemSlot.CanReceiveItem(dropItemSlot.Item)))
        {
            InvokeServerRpc(ChangeItemSlots, _draggedItemSlotIndex, _draggedItemSlot.GetType().ToString(), index, dropItemSlot.GetType().ToString());
        }
    }
    [ServerRPC] private void ChangeItemSlots(int draggedItemSLotIndex, string draggedIemSlotType, int dropItemSlotIndex, string dropItemSlotType) {}
    private void Subscribe()
    {
        _inventory.OnClickEvent += SelectItem;
        _equipment.OnClickEvent += SelectItem;

        _inventory.OnBeginDragEvent += BeginDrag;
        _equipment.OnBeginDragEvent += BeginDrag;

        _inventory.OnDragEvent += Drag;
        _equipment.OnDragEvent += Drag;

        _inventory.OnEndDragEvent += EndDrag;
        _equipment.OnEndDragEvent += EndDrag;

        _inventory.OnDropEvent += Drop;
        _equipment.OnDropEvent += Drop;
    }
}
