#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


[CreateAssetMenu(fileName = "New Recipe Database", menuName = "Custom/Recipe Database")]
public class RecipeDatabase : ScriptableObject
{
    [SerializeField] private CraftingRecipe[] recipes;

    public CraftingRecipe GetRecipeReference(string recipeId)
    {
        foreach(CraftingRecipe recipe in recipes)
        {
            if(recipe.Id == recipeId)
                return recipe;
        }
        return null;
    }
    public CraftingRecipe GetRecipeCopy(string recipeId)
    {
        CraftingRecipe recipe = GetRecipeReference(recipeId);
        if(recipe == null)
            return null;
        
        return recipe.GetCopy();
    }
    #if UNITY_EDITOR
    private void LoadItems() => recipes = FindAssetsByType<CraftingRecipe>("Assets/Recipes");
    private void OnValidate() => LoadItems();
    private void OnEnable() => EditorApplication.projectChanged += LoadItems;
    private void OnDisable() => EditorApplication.projectChanged -= LoadItems;

    public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
    {
        string type = typeof(T).ToString().Replace("UnityEngine.", "");

        string[] guids;
        if(folders == null || folders.Length == 0)
            guids = AssetDatabase.FindAssets("t:" + type);
        else
            guids = AssetDatabase.FindAssets("t:" + type, folders);

        T[] assets = new T[guids.Length];

        for(int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
        return assets;
    }
    #endif
}
