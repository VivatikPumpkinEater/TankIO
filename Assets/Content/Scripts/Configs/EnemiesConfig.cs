using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesConfig : BaseConfig<EnemiesConfig>
{
    [SerializeField] private List<Model> _models;

#if UNITY_EDITOR

    /// <summary> ИСПОЛЬЗОВАТЬ ТОЛЬКО ДЛЯ ЭДИТОРА! </summary>
    public IEnumerable<Model> Models => _models;

#endif

    public static Model GetRandomEnemy()
    {
        var models = Instance._models;
        var index = Random.Range(0, models.Count);

        return models[index];
    }
    
    [Serializable]
    public struct Model
    {
        public EnemyView View;
        public EnemiesSettings Settings;
    }
}