﻿using System;
using UnityEngine;

public abstract class UnitView : MonoBehaviour, IDamageable
{
    public Action<float> Damaged;

    [SerializeField] private ActorType _actorType;
    [SerializeField] private ActorType _targetType;
    
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ProtectionBar _protectionBar;

    private Rigidbody2D _rigidbody2D;
    
    public HealthBar HealthBar => _healthBar;
    public ProtectionBar ProtectionBar => _protectionBar;

    public Rigidbody2D Rigidbody2D => _rigidbody2D ??= GetComponent<Rigidbody2D>();

    public Vector2 Position => Rigidbody2D.position;

    public ActorType ActorType => _actorType;

    public ActorType TargetType => _targetType;

    public void SetBars(HealthBar healthBar, ProtectionBar protectionBar)
    {
        _healthBar = healthBar;
        _protectionBar = protectionBar;
    }

    public void TakeDamage(float value) => Damaged?.Invoke(value);
}