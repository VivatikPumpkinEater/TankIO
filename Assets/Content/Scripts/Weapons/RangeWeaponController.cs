using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RangeWeaponController
{
    private readonly ActorType _targetType;
    private readonly RangeWeaponView _view;
    private readonly BulletPool _pool;
    private readonly WeaponSettings _settings;

    private int DelayBetweenShot => (int)(_settings.DelayBetweenShots * 1000f);

    private CancellationTokenSource _cts;

    private bool _ready;

    public RangeWeaponController(ActorType targetType, RangeWeaponView view, BulletPool pool, WeaponSettings settings)
    {
        _targetType = targetType;
        _view = view;
        _pool = pool;
        _settings = settings;
    }

    public void SetActive(bool value)
    {
        _view.gameObject.SetActive(value);
    }
    
    public async void Shot()
    {
        var repeat = _settings.FiringInBursts ? _settings.BurstsSettings.BulletCount : 1;
        var delay = _settings.FiringInBursts ? (int)(_settings.BurstsSettings.DelayInBurst * 1000) : 0;

        for (var i = 0; i < repeat; i++)
        {
            var directions = Spread();

            foreach (var direction in directions)
                ShootBullet(direction);
            
            await UniTask.Delay(delay);
        }
    }

    private void ShootBullet(Vector2 direction)
    {
        var shotPoint = _view.ShotPoint;
        var bullet = _pool.GetFreeElement(_settings.BulletType, shotPoint);
        bullet.Init(_targetType, direction, 50f, _settings.Speed, _settings.Damage);
    }

    private List<Vector2> Spread()
    {
        var directions = new List<Vector2>();

        if (!_settings.ShootingFractions)
        {
            directions.Add(_view.UpVector);
            return directions;
        }

        var count = _settings.FractionsSettings.CountInFractions;
        
        var angleStep = 45f / (count - 1);
        var startAngle = -45f / 2f;
        
        for (var i = 0; i < count; i++)
        {
            var angle = startAngle + i * angleStep;
            var rotation = Quaternion.Euler(0, 0, angle);
            var bulletDirection = rotation * _view.UpVector;
            
            directions.Add(bulletDirection);
        }
        
        return directions;
    }
}