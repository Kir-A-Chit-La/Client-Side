using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private Inventory inventory;
    private void OnEnable() => inventory = (Inventory)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Open Inventory Tester"))
        {
            InventoryEditorWindow.Open(inventory);
        }
    }
}
