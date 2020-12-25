using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ProgressBar))]
public class ProgressBarEditor : Editor
{
    private ProgressBar progressBar;

    public void OnEnable() => progressBar = (ProgressBar)target;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        progressBar.ApplyValuesInEditorMode();

        if(GUI.changed)
            SetDirty(progressBar.gameObject);
    }
    public void SetDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}
