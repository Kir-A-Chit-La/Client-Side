using System;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public event Action<int, BaseItemSlot> OnClickEvent;
    public event Action<int, BaseItemSlot> OnBeginDragEvent;
    public event Action<int, BaseItemSlot> OnDragEvent;
    public event Action<int, BaseItemSlot> OnEndDragEvent;
    public event Action<int, BaseItemSlot> OnDropEvent;

    [SerializeField] private Transform _equipmentSlotsParent;
    public EquipmentSlot[] equipmentSlots;

    private void OnValidate()
    {
        if(_equipmentSlotsParent != null)
            equipmentSlots = _equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }
    public void Init()
    {
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnClickEvent += slot => EventHelper(slot, OnClickEvent);
            equipmentSlots[i].OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);
            equipmentSlots[i].OnDragEvent += slot => EventHelper(slot, OnDragEvent);
            equipmentSlots[i].OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
            equipmentSlots[i].OnDropEvent += slot => EventHelper(slot, OnDropEvent);
        }
    }
    private void EventHelper(BaseItemSlot itemSlot, Action<int, BaseItemSlot> action)
	{
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i] == itemSlot)
            {
                action?.Invoke(i, itemSlot);
            }
        }
	}
    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i].equipmentType == item.EquipmentType)
            {
                previousItem = (EquippableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                equipmentSlots[i].Amount++;
                return true;
            }
        }
        previousItem = null;
        return false;
    }
    public bool AddItem(EquippableItem item)
    {
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i].equipmentType == item.EquipmentType && equipmentSlots[i].Item == null)
            {
                equipmentSlots[i].Item = item;
                equipmentSlots[i].Amount = 1;
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(EquippableItem item)
    {
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                equipmentSlots[i].Amount = 0;
                return true;
            }
        }
        return false;
    }
}
