using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig : BaseConfig<PlayerConfig>
{
    [SerializeField] private PlayerSettings _playerSettings;

    [SerializeField] private List<WeaponsModel> _weaponsModels;

    public static PlayerSettings PlayerSettings => Instance._playerSettings;

#if UNITY_EDITOR

    /// <summary> ИСПОЛЬЗОВАТЬ ТОЛЬКО ДЛЯ ЭДИТОРА! </summary>
    public IEnumerable<WeaponsModel> WeaponsModels => _weaponsModels;

#endif
    
    [Serializable]
    public struct WeaponsModel
    {
        public string Name;
        public BaseWeapon Weapon;
        public float Force;
        public float Damage;
    }
}