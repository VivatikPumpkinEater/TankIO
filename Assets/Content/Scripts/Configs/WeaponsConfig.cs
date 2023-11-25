using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsConfig : BaseConfig<WeaponsConfig>
{
    [SerializeField] private List<Model> _models;

    public static List<Model> GetAll() => Instance._models;

#if UNITY_EDITOR

    public void RemoveElement(int index)
    {
        _models.RemoveAt(index);
    }
    
#endif
    
    [Serializable]
    public class Model
    {
        public string Name;
        public RangeWeaponView WeaponView;
        public WeaponSettings Settings;
    }
}