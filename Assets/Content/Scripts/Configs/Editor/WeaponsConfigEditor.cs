using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponsConfig))]
public class WeaponsConfigEditor : Editor
{
    private SerializedProperty _modelsProperty;
    private WeaponsConfig _config;

    private void OnEnable()
    {
        _modelsProperty = serializedObject.FindProperty("_models");
        _config = (WeaponsConfig)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        if (GUILayout.Button("Add Element", GUILayout.Height(40)))
            _modelsProperty.arraySize++;
            
        EditorGUILayout.Space();

        if (_modelsProperty.arraySize > 0)
        {
            EditorGUI.indentLevel++;

            for (var i = 0; i < _modelsProperty.arraySize; i++)
            {
                var modelProperty = _modelsProperty.GetArrayElementAtIndex(i);

                DrawWeaponInspector(modelProperty);
                
                if (GUILayout.Button("Remove Element", GUILayout.Height(15)))
                {
                    _config.RemoveElement(i);
                    break;
                }

                EditorGUILayout.Space();
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawWeaponInspector(SerializedProperty modelProperty)
    {
        var nameProperty = modelProperty.FindPropertyRelative("Name");
        EditorGUILayout.LabelField(nameProperty.stringValue, EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(nameProperty);
        
        var weaponProperty = modelProperty.FindPropertyRelative("WeaponView");
        EditorGUILayout.PropertyField(weaponProperty);
        
        var settingsProperty = modelProperty.FindPropertyRelative("Settings");
        
        EditorGUILayout.LabelField("Weapon Settings", EditorStyles.boldLabel);
        var bulletTypeProperty = settingsProperty.FindPropertyRelative("BulletType");
        EditorGUILayout.PropertyField(bulletTypeProperty);

        var damageProperty = settingsProperty.FindPropertyRelative("Damage");
        EditorGUILayout.PropertyField(damageProperty);

        var speedProperty = settingsProperty.FindPropertyRelative("Speed");
        EditorGUILayout.PropertyField(speedProperty);

        var delayBetweenShotsProperty = settingsProperty.FindPropertyRelative("DelayBetweenShots");
        EditorGUILayout.PropertyField(delayBetweenShotsProperty);
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Bursts Settings", EditorStyles.boldLabel);
        var firingInBurstsProperty = settingsProperty.FindPropertyRelative("FiringInBursts");
        EditorGUILayout.PropertyField(firingInBurstsProperty);
        var firingInBursts = firingInBurstsProperty.boolValue;

        if (firingInBursts)
        {
            var burstsSettingsProperty = settingsProperty.FindPropertyRelative("BurstsSettings");
            EditorGUILayout.PropertyField(burstsSettingsProperty, true);
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Fractions Settings", EditorStyles.boldLabel);
        var shootingFractionsProperty = settingsProperty.FindPropertyRelative("ShootingFractions");
        EditorGUILayout.PropertyField(shootingFractionsProperty);
        var shootingFractions = shootingFractionsProperty.boolValue;

        if (!shootingFractions)
            return;
        
        var fractionsSettingsProperty = settingsProperty.FindPropertyRelative("FractionsSettings");
        EditorGUILayout.PropertyField(fractionsSettingsProperty, true);
    }
}