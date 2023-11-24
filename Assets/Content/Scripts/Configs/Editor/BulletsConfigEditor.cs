using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletsConfig))]
public class BulletsConfigEditor : Editor
{
    private BulletsConfig _config;

    private void OnEnable()
    {
        _config = (BulletsConfig)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CheckDuplicate();
        
        if (GUILayout.Button("Generate types", GUILayout.Height(40)))
            _config.GenerateTypes();
    }

    private void CheckDuplicate()
    {
        var duplicateItems = _config.Prefabs
            .GroupBy(x => x != null ? x.name : null)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        if (duplicateItems.Any())
            EditorGUILayout.HelpBox("There's a duplicate", MessageType.Warning);
    }
}