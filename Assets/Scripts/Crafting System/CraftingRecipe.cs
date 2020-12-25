using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Item Item;
    [Range(1,99)] public int Amount;
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Custom/Items/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(IItemContainer itemContainer)
    {
        foreach(ItemAmount itemAmount in Materials)
        {
            if(itemContainer.ItemCount(itemAmount.Item.Id) < itemAmount.Amount)
                return false;
        }
        return true;
    }
    public void Craft(IItemContainer itemContainer)
    {
        if(CanCraft(itemContainer))
        {
            foreach(ItemAmount itemAmount in Materials)
            {
                for(int i = 0; i < itemAmount.Amount; i++)
                {
                    Item oldItem = itemContainer.RemoveItem(itemAmount.Item.Id);
                    oldItem.Destroy();
                }
            }
            foreach(ItemAmount itemAmount in Results)
            {
                for(int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainer.AddItem(itemAmount.Item.GetCopy());
                }
            }
        }
    }
}
