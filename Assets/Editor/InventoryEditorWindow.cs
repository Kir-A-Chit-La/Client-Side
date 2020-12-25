using UnityEngine;
using UnityEditor;

public class InventoryEditorWindow : ExtendedEditorWindow
{
    private static Inventory Inventory;
    private Item item;
    public static void Open(Inventory inventory)
    {
        InventoryEditorWindow window = GetWindow<InventoryEditorWindow>("Inventory Tester");
        window.minSize = new Vector2(800, 300);
        window.serializedObject = new SerializedObject(inventory);
        Inventory = inventory;
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("_items");

        EditorGUILayout.BeginHorizontal();

        item = EditorGUILayout.ObjectField("Item to add", item, typeof(Item), false) as Item;
        if(GUILayout.Button("Add item", GUILayout.Width(80f)))
            Inventory.AddItem(item);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150f), GUILayout.ExpandHeight(true));

        DrawSidebar(currentProperty);

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        if(selectedProperty != null)
        {
            EditorGUILayout.TextField("Name", (selectedProperty.objectReferenceValue as Item).Name);
            EditorGUILayout.FloatField("Weight", (selectedProperty.objectReferenceValue as Item).Weight);
            EditorGUILayout.IntField("Stack Size", (selectedProperty.objectReferenceValue as Item).MaximumStackSize);
            EditorGUILayout.ObjectField("Preview", (selectedProperty.objectReferenceValue as Item).Preview as Sprite, typeof(Sprite), false);
            if(GUILayout.Button("Remove this item"))
                Inventory.RemoveItem(selectedProperty.objectReferenceValue as Item);
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }
    private void OnInspectorUpdate()
    {
        if(serializedObject.UpdateIfRequiredOrScript())
            Repaint();
    }
}
