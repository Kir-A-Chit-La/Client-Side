using UnityEngine;
using UnityEditor;

public class ItemObjectEditorWindow : EditorWindow
{
    private static Item Item;
    public static void OpenWindow(Item itemObject)
    {
        ItemObjectEditorWindow window = GetWindow<ItemObjectEditorWindow>("Item Editor");
        Item = itemObject;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box", GUILayout.ExpandWidth(false));
        
        EditorGUILayout.PrefixLabel("Icon");
        Item.Preview = EditorGUILayout.ObjectField(Item.Preview, typeof(Sprite), false, GUILayout.Height(120f), GUILayout.Width(120f)) as Sprite;

        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("ID");
        EditorGUILayout.SelectableLabel(Item.Id, GUILayout.Height(20f) ,GUILayout.ExpandHeight(false));
        EditorGUILayout.EndHorizontal();
        Item.Name = EditorGUILayout.TextField("Name", Item.Name);
        EditorGUILayout.PrefixLabel("Description");
        Item.Description = EditorGUILayout.TextArea(Item.Description, GUILayout.MinHeight(50f));

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("Box");

        Item.Weight = EditorGUILayout.FloatField("Weight", Item.Weight);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Maximum Stack Size");
        Item.MaximumStackSize = EditorGUILayout.IntSlider(Item.MaximumStackSize, 1, 99);
        EditorGUILayout.EndHorizontal();
        Item.Prefab = EditorGUILayout.ObjectField(Item.Prefab, typeof(GameObject), false) as GameObject;

        EditorGUILayout.EndVertical();
    }
}
