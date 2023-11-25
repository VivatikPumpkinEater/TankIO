using System.Collections.Generic;
using UnityEngine;

public class EnemiesSystem
{
    private const int MaxCount = 10;
    private const float BorderOffset = 2f;

    private readonly Vector2 _worldSize;
    private readonly Transform _target;
    private readonly UpdateSystem _updateSystem;

    private List<IUnit> _enemies = new();

    public EnemiesSystem(Vector2 worldSize, Transform target, UpdateSystem updateSystem)
    {
        var borderOffset = Vector2.one * BorderOffset;

        _worldSize = worldSize + borderOffset;
        _target = target;
        _updateSystem = updateSystem;

        Init();
    }

    private void Init()
    {
        for (var i = 0; i < MaxCount; i++)
            CreateEnemy();
    }

    private Vector2 GetRandomPosition()
    {
        var vertical = Random.Range(0, 2) == 0;

        var position = Vector2.zero;

        if (vertical)
        {
            position.x = Random.Range(_worldSize.x, _worldSize.x);
            position.y = Random.Range(0, 2) == 0 ? -_worldSize.y : _worldSize.y;

            return position;
        }

        position.x = Random.Range(0, 2) == 0 ? -_worldSize.x : _worldSize.x;
        position.y = Random.Range(-_worldSize.y, _worldSize.y);

        return position;
    }

    private void CreateEnemy()
    {
        var model = EnemiesConfig.GetRandomEnemy();
        var view = Object.Instantiate(model.View, GetRandomPosition(), Quaternion.identity);
        var controller = new EnemyController(_target, view, model.Settings);
        
        _enemies.Add(controller);
        
        _updateSystem.Register(controller);
        controller.Die += OnDie;
    }

    private void OnDie(IUnit enemy)
    {
        _updateSystem.Unregister(enemy);
        _enemies.Remove(enemy);
        
        if (_enemies.Count < MaxCount)
            CreateEnemy();
    }
}