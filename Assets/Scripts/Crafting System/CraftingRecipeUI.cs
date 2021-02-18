using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraftingRecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform arrowParent;
    [SerializeField] private BaseItemSlot[] itemSlots;

    [Header("Public Variables")]
    public ItemContainer ItemContainer;

    public event Action<string> OnCraftButtonPressed;

    private CraftingRecipe _craftingRecipe;
    public CraftingRecipe CraftingRecipe
    {
        get => _craftingRecipe;
        set => SetCraftingRecipe(value);
    }

    private void OnValidate()
    {
        itemSlots = GetComponentsInChildren<BaseItemSlot>(true);
    }

    public void OnCraftButtonClick()
    {
        if(_craftingRecipe != null && ItemContainer != null)
        {
            if(_craftingRecipe.Craft(ItemContainer))
            {
                OnCraftButtonPressed?.Invoke(_craftingRecipe.Id.ToString());
            }
        }
    }

    private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
    {
        _craftingRecipe = newCraftingRecipe;

        if(_craftingRecipe != null)
        {
            int slotIndex = 0;
			slotIndex = SetSlots(_craftingRecipe.Materials, slotIndex);
			arrowParent.SetSiblingIndex(slotIndex);
			slotIndex = SetSlots(_craftingRecipe.Results, slotIndex);

			for (int i = slotIndex; i < itemSlots.Length; i++)
			{
				itemSlots[i].transform.parent.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
    }

    private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
	{
		for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
		{
			ItemAmount itemAmount = itemAmountList[i];
			BaseItemSlot itemSlot = itemSlots[slotIndex];

			itemSlot.Item = itemAmount.Item;
			itemSlot.Amount = itemAmount.Amount;
			itemSlot.transform.parent.gameObject.SetActive(true);
		}
		return slotIndex;
	}
}
