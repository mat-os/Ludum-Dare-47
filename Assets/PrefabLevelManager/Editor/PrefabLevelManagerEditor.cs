using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(PrefabLevelManager))]
[CanEditMultipleObjects]
public class PrefabLevelManagerEditor : Editor
{
    private ReorderableList list1;

    private bool _isUseDebugLevel;
    private SerializedProperty debugLevel;

    public override void OnInspectorGUI()
    {
        //    base.OnInspectorGUI();
        var prefabLevelManager = (PrefabLevelManager) target;
        serializedObject.Update();
        
        //Debug level
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Debug Level", HeaderStyle(), GUILayout.ExpandWidth(true));
        debugLevel = serializedObject.FindProperty("DebugLevel");
        EditorGUILayout.ObjectField(debugLevel);
        _isUseDebugLevel = EditorGUILayout.Toggle("Is Enabled", prefabLevelManager.IsUseDebugLevel);
        prefabLevelManager.IsUseDebugLevel = _isUseDebugLevel;
        
        //Prefab levels
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("PrefabLevels", HeaderStyle(), GUILayout.ExpandWidth(true));
        ReorderableListUtility.DoLayoutListWithFoldout(list1);

        //Loop levels
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Loop Levels (Read Only)", HeaderStyle(), GUILayout.ExpandWidth(true));
        var loopLevels = serializedObject.FindProperty("LoopPrefabLevels");
        EditorGUILayout.PropertyField(loopLevels, new GUIContent("Loop Levels"), true);
        serializedObject.ApplyModifiedProperties();
    }



    private GUIStyle HeaderStyle()
    {
        return new GUIStyle(GUI.skin.label)
            {alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16};
    }

    private void OnEnable()
    {
        var property = serializedObject.FindProperty("PrefabLevels");

        list1 = ReorderableListUtility.CreateAutoLayout(
            property,
            new[] {"Order", "Level", "In Loop"},
            new float?[] {50, 150, 70});

        if (_isUseDebugLevel && debugLevel == null)
        {
            _isUseDebugLevel = false;
        }

        /*this.list2 = ReorderableListUtility.CreateAutoLayout(
            property,
            new string[] { "Drink Name", "Cost", "Key Color" },
            new float?[] { 100, 70 });
    }*/
    }
}