using System.Linq;
using UnityEditor;

[CustomEditor(typeof(PlayerConfig))]
public class PlayerConfigEditor : Editor
{
    private PlayerConfig _config;

    private void OnEnable()
    {
        _config = (PlayerConfig)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CheckDuplicate();
    }

    private void CheckDuplicate()
    {
        var duplicateItems = _config.WeaponsModels
            .GroupBy(x => x.Name)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        if (duplicateItems.Any())
            EditorGUILayout.HelpBox("There's a duplicate", MessageType.Warning);
    }
}