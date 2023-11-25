using System;
using UnityEngine;

public class EnemyView : UnitView
{
    public Action<IDamageable> Damaged;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var damageable = other.collider.GetComponent<IDamageable>();
        if (damageable == null)
            return;
        
        if (damageable.ActorType != TargetType)
            return;
        
        Damaged?.Invoke(damageable);
    }
}