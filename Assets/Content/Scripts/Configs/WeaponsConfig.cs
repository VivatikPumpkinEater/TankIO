using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsConfig : BaseConfig<WeaponsConfig>
{
    [SerializeField] private List<Model> _models;
    
    [Serializable]
    public class Model
    {
        public string Name;
        public BaseRangeWeapon RangeWeapon;
        public WeaponSettings Settings = new SimpleWeaponSettings();
    }
}