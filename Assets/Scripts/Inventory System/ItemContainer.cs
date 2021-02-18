using System;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.NetworkedVar.Collections;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
    public event Action<int, BaseItemSlot> OnClickEvent;
    public event Action<int, BaseItemSlot> OnBeginDragEvent;
    public event Action<int, BaseItemSlot> OnDragEvent;
    public event Action<int, BaseItemSlot> OnEndDragEvent;
    public event Action<int, BaseItemSlot> OnDropEvent;
    
    public List<ItemSlot> itemSlots;

    public virtual void Init()
    {

    }
    protected void SubscribeForEvents(ItemSlot itemSlot)
    {
        itemSlot.OnClickEvent += slot => EventHelper(slot, OnClickEvent);
        itemSlot.OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);
        itemSlot.OnDragEvent += slot => EventHelper(slot, OnDragEvent);
        itemSlot.OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
        itemSlot.OnDropEvent += slot => EventHelper(slot, OnDropEvent);
    }
    protected void UnsubscribeFromEvents(ItemSlot itemSlot)
    {
        itemSlot.OnClickEvent -= slot => EventHelper(slot, OnClickEvent);
        itemSlot.OnBeginDragEvent -= slot => EventHelper(slot, OnBeginDragEvent);
        itemSlot.OnDragEvent -= slot => EventHelper(slot, OnDragEvent);
        itemSlot.OnEndDragEvent -= slot => EventHelper(slot, OnEndDragEvent);
        itemSlot.OnDropEvent -= slot => EventHelper(slot, OnDropEvent);
    }
    private void EventHelper(BaseItemSlot itemSlot, Action<int, BaseItemSlot> action)
	{
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i] == itemSlot)
            {
                action?.Invoke(i, itemSlot);
            }
        }
	}
    public virtual int ItemCount(string itemId)
    {
        int number = 0;

		for (int i = 0; i < itemSlots.Count; i++)
		{
			Item item = itemSlots[i].Item;
			if (item != null && item.Id == itemId)
			{
				number += itemSlots[i].Amount;
			}
		}
		return number;
    }
    public virtual bool ContainsItem(Item item)
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].Item == item)
            {
                return true;
            }
        }
        return false;
    }
    public virtual bool CanAddItem(Item item, int amount = 1)
	{
		int freeSpaces = 0;

		foreach (ItemSlot itemSlot in itemSlots)
		{
			if (itemSlot.Item == null || itemSlot.Item.Id == item.Id)
			{
				freeSpaces += item.MaximumStackSize - itemSlot.Amount;
			}
		}
		return freeSpaces >= amount;
	}
    public virtual bool AddItem(Item item)
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].Item == null || itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }
    public virtual Item RemoveItem(string itemId)
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            Item item = itemSlots[i].Item;
            if(item != null && item.Id == itemId)
            {
                itemSlots[i].Amount--;
                
                return item;
            }
        }
        return null;
    }
    public virtual bool RemoveItem(Item item)
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if(itemSlots[i].Item == item)
            {
                itemSlots[i].Amount--;
                
                return true;
            }
        }
        return false;
    }
    public virtual void Clear()
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item != null && Application.isPlaying) {
				itemSlots[i].Item.Destroy();
			}
			itemSlots[i].Item = null;
			itemSlots[i].Amount = 0;
		}
	}
}
