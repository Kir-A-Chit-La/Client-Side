using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset()] public static bool OpenEditor(int instanceId, int line)
    {
        Item itemObj = EditorUtility.InstanceIDToObject(instanceId) as Item;
        if(itemObj != null)
        {
            ItemObjectEditorWindow.OpenWindow(itemObj);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(Item), true)]
public class ItemCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Item Editor"))
        {
            ItemObjectEditorWindow.OpenWindow((Item)target);
        }
    }
}
