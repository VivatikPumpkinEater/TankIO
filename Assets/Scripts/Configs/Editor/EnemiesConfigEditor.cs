using System.Linq;
using UnityEditor;

[CustomEditor(typeof(EnemiesConfig))]
public class EnemiesConfigEditor : Editor
{
    private EnemiesConfig _config;

    private void OnEnable()
    {
        _config = (EnemiesConfig)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CheckDuplicate();
    }

    private void CheckDuplicate()
    {
        var duplicateItems = _config.Models
            .GroupBy(x => x.View)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        if (duplicateItems.Any())
            EditorGUILayout.HelpBox("There's a duplicate", MessageType.Warning);
    }
}