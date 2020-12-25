using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(UITweener))]
public class UITweenerEditor : Editor
{
    private UITweener tweener;

    public void OnEnable()
    {
        tweener = (UITweener)target;
    }

    public override void OnInspectorGUI()
    {
        tweener._objectToAnimate = (GameObject)EditorGUILayout.ObjectField("Object to Animate", tweener._objectToAnimate, typeof(GameObject), true);
        tweener._objectToDisable = (GameObject)EditorGUILayout.ObjectField("Object to Disable", tweener._objectToDisable, typeof(GameObject), true);
        tweener._animationType = (AnimationType)EditorGUILayout.EnumPopup("Animation Type", tweener._animationType);
        tweener._easeType = (LeanTweenType)EditorGUILayout.EnumPopup("Ease Type", tweener._easeType);
        switch(tweener._animationType)
        {
            case AnimationType.MoveX:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'X' position", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'X' posititon", tweener._floatTo);
                break;
            case AnimationType.MoveY:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'Y' position", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'Y' position", tweener._floatTo);
                break;
            case AnimationType.Move:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._vectorFrom = EditorGUILayout.Vector3Field("Start position", tweener._vectorFrom);
                }
                tweener._vectorTo = EditorGUILayout.Vector3Field("End position", tweener._vectorTo);
                break;
            case AnimationType.MoveLocalX:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start local 'X' position", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End local 'X' position", tweener._floatTo);
                break;
            case AnimationType.MoveLocalY:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start local 'Y' position", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End local 'Y' position", tweener._floatTo);
                break;
            case AnimationType.MoveLocal:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._vectorFrom = EditorGUILayout.Vector3Field("Start local position", tweener._vectorFrom);
                }
                tweener._vectorTo = EditorGUILayout.Vector3Field("End local position", tweener._vectorTo);
                break;
            case AnimationType.RotateX:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'X' rotation", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'X' rotation", tweener._floatTo);
                break;
            case AnimationType.RotateY:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'Y' rotation", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'Y' rotation", tweener._floatTo);
                break;
            case AnimationType.RotateZ:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'Z' rotation", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'Z' rotation", tweener._floatTo);
                break;
            case AnimationType.Rotate:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._vectorFrom = EditorGUILayout.Vector3Field("Start rotation", tweener._vectorFrom);
                }
                tweener._vectorTo = EditorGUILayout.Vector3Field("End rotation", tweener._vectorTo);
                break;
            case AnimationType.RotateLocal:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._vectorFrom = EditorGUILayout.Vector3Field("Start local rotation", tweener._vectorFrom);
                }
                tweener._vectorTo = EditorGUILayout.Vector3Field("End local rotation", tweener._vectorTo);
                break;
            case AnimationType.ScaleX:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'X' scale", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'X' scale", tweener._floatTo);
                break;
            case AnimationType.ScaleY:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'Y' scale", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'Y' scale", tweener._floatTo);
                break;
            case AnimationType.ScaleZ:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start 'Z' scale", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End 'Z' scale", tweener._floatTo);
                break;
            case AnimationType.Scale:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._vectorFrom = EditorGUILayout.Vector3Field("Start scale", tweener._vectorFrom);
                }
                tweener._vectorTo = EditorGUILayout.Vector3Field("End sclae", tweener._vectorTo);
                break;
            case AnimationType.Alpha:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._floatFrom = EditorGUILayout.FloatField("Start alpha value", tweener._floatFrom);
                }
                tweener._floatTo = EditorGUILayout.FloatField("End alpha value", tweener._floatTo);
                break;
            case AnimationType.Color:
                tweener._startPositionOffset = EditorGUILayout.Toggle("Start offset", tweener._startPositionOffset);
                if(tweener._startPositionOffset)
                {
                    tweener._colorFrom = EditorGUILayout.ColorField("Start color", tweener._colorFrom);
                }
                tweener._colorTo = EditorGUILayout.ColorField("End color", tweener._colorTo);
                break;
        }
        tweener._duration = (float)EditorGUILayout.FloatField("Duration", tweener._duration);
        tweener._delay = (float)EditorGUILayout.FloatField("Delay", tweener._delay);
        tweener._loop = (bool)EditorGUILayout.Toggle("Loop", tweener._loop);
        tweener._pingPong = (bool)EditorGUILayout.Toggle("Ping-Pong", tweener._pingPong);
        tweener._showOnEnable = (bool)EditorGUILayout.Toggle("Show on Enable", tweener._showOnEnable);
        tweener._workOnDisable = (bool)EditorGUILayout.Toggle("Work on Disable", tweener._workOnDisable);
        
        if(GUI.changed)
        {
            SetDirty(tweener.gameObject);
        }

    }

    public void SetDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}