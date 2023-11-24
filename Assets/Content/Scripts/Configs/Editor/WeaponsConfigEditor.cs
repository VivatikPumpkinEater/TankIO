using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponsConfig))]
public class WeaponsConfigEditor : Editor
{
    private SerializedProperty _modelsProperty;

    private void OnEnable()
    {
        _modelsProperty = serializedObject.FindProperty("_models");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Add Element", GUILayout.Height(40)))
                _modelsProperty.arraySize++;
            
            if (GUILayout.Button("RemoveElement", GUILayout.Height(40)))
                _modelsProperty.arraySize--;
        }
        GUILayout.EndHorizontal();

        if (_modelsProperty.isExpanded)
        {
            EditorGUI.indentLevel++;

            for (var i = 0; i < _modelsProperty.arraySize; i++)
            {
                var modelProperty = _modelsProperty.GetArrayElementAtIndex(i);
                var rangeWeaponProperty = modelProperty.FindPropertyRelative("RangeWeapon");

                EditorGUILayout.PropertyField(modelProperty);

                if (rangeWeaponProperty.objectReferenceValue != null)
                {
                    var rangeWeapon = (BaseRangeWeapon)rangeWeaponProperty.objectReferenceValue;
                    DrawRangeWeaponInspector(rangeWeapon);
                }

                EditorGUILayout.Space();
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawRangeWeaponInspector(BaseRangeWeapon rangeWeapon)
    {
        switch (rangeWeapon)
        {
            case SimpleRangeWeapon:
                EditorGUILayout.LabelField("Simple", EditorStyles.boldLabel);
                break;
        }
    }
}