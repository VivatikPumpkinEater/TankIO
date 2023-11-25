using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponsConfig : BaseConfig<WeaponsConfig>
{
    [SerializeField] private List<Model> _models;

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