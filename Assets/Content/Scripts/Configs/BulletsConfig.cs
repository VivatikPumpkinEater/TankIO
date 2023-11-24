using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletsConfig : BaseConfig<BulletsConfig>
{
    [SerializeField] private List<Bullet> _prefabs;

    private Dictionary<BulletTypes, Bullet> _bulletPrefabs = new();

    protected override void OnInit()
    {
        foreach (var prefab in _prefabs)
        {
            var bulletType = (BulletTypes)Enum.Parse(typeof(BulletTypes), prefab.name);
            if (_bulletPrefabs.ContainsKey(bulletType))
                continue;
            
            _bulletPrefabs.Add(bulletType, prefab);
        }
    }

    public static Dictionary<BulletTypes, Bullet> BulletPrefabs => Instance._bulletPrefabs;

#if UNITY_EDITOR

    /// <summary> ИСПОЛЬЗОВАТЬ ТОЛЬКО ДЛЯ ЭДИТОРА! </summary>
    public IEnumerable<Bullet> Prefabs => _prefabs;

    /// <summary> ИСПОЛЬЗОВАТЬ ТОЛЬКО ДЛЯ ЭДИТОРА! </summary>
    public void GenerateTypes()
    {
        var types = new List<string>();
        foreach (var prefab in _prefabs)
        {
            if (prefab == null)
            {
                Debug.LogError("Elements must not be equal to NULL!");
                return;
            }
            
            types.Add(prefab.name);
        }
        
        TypesGenerator.Generate(true, "BulletTypes", types);
    }
    
#endif
}