using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class CraftingWindow : NetworkedBehaviour
{
	[Header("References")]
	[SerializeField] CraftingRecipeUI recipeUIPrefab;
	[SerializeField] RectTransform recipeUIParent;
	[SerializeField] List<CraftingRecipeUI> craftingRecipeUIs;

	[Header("Public Variables")]
	public ItemContainer ItemContainer;
	public List<CraftingRecipe> CraftingRecipes;

	private void OnValidate()
	{
		Init();
	}

	private void Start()
	{
		Init();
		Debug.Log(NetworkId + " " + GetBehaviourId());
	}

	private void Init()
	{
		recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
		UpdateCraftingRecipes();
	}

	public void UpdateCraftingRecipes()
	{
		for (int i = 0; i < CraftingRecipes.Count; i++)
		{
			if (craftingRecipeUIs.Count == i)
			{
				craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
			}
			else if (craftingRecipeUIs[i] == null)
			{
				craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
			}

			craftingRecipeUIs[i].ItemContainer = ItemContainer;
			craftingRecipeUIs[i].CraftingRecipe = CraftingRecipes[i];
			craftingRecipeUIs[i].OnCraftButtonPressed += recipeId => OnCraftButton(recipeId);
		}

		for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
		{
			craftingRecipeUIs[i].CraftingRecipe = null;
			craftingRecipeUIs[i].OnCraftButtonPressed -= OnCraftButton;
		}
	}

	private void OnCraftButton(string craftRecipeId)
	{
		InvokeServerRpc(CraftRecipe, craftRecipeId);
	}

	[ServerRPC] private void CraftRecipe(string recipeId) {}
}
