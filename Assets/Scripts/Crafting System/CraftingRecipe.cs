using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
	public Item Item;
	[Range(1, 99)]
	public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField] private string id;
    public string Id => id;
	public List<ItemAmount> Materials;
	public List<ItemAmount> Results;

    public virtual CraftingRecipe GetCopy() => this;

    private void OnValidate()
    {
        id = Hasher.GetStableHash64(name).ToString();
    }

	public bool CanCraft(IItemContainer itemContainer)
	{
		return HasMaterials(itemContainer) && HasSpace(itemContainer);
	}

	private bool HasMaterials(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Materials)
		{
			if (itemContainer.ItemCount(itemAmount.Item.Id) < itemAmount.Amount)
			{
				Debug.LogWarning("You don't have the required materials.");
				return false;
			}
		}
		return true;
	}

	private bool HasSpace(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Results)
		{
            Debug.LogError(itemAmount.Item.Name + " " + itemAmount.Amount);
			if (!itemContainer.CanAddItem(itemAmount.Item, itemAmount.Amount))
			{
				Debug.LogWarning("Your inventory is full.");
				return false;
			}
		}
		return true;
	}

	public bool Craft(IItemContainer itemContainer)
	{
		if (CanCraft(itemContainer))
		{
            return true;
			/*RemoveMaterials(itemContainer);
			AddResults(itemContainer);*/
		}
        return false;
	}

	private void RemoveMaterials(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Materials)
		{
			for (int i = 0; i < itemAmount.Amount; i++)
			{
				Item oldItem = itemContainer.RemoveItem(itemAmount.Item.Id);
				oldItem.Destroy();
			}
		}
	}

	private void AddResults(IItemContainer itemContainer)
	{
		foreach (ItemAmount itemAmount in Results)
		{
			for (int i = 0; i < itemAmount.Amount; i++)
			{
				itemContainer.AddItem(itemAmount.Item.GetCopy());
			}
		}
	}
}
