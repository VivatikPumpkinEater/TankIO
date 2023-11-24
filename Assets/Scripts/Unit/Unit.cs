using System;

public abstract class Unit<T> : IDamageable where T : UnitView
{
    public Action Die;

    protected readonly T View;
    
    protected readonly HealthBar HealthBar;
    protected readonly ProtectionBar ProtectionBar;
    
    protected float CurrentHealth => HealthBar.Value;
    
    protected Unit(T view, BaseUnitSettings baseSettings)
    {
        View = view;

        HealthBar = view.HealthBar;
        HealthBar.Init(baseSettings.Health);
        
        ProtectionBar = view.ProtectionBar;
        ProtectionBar.Init(baseSettings.ProtectionFactor);
    }

    public void TakeDamage(float value)
    {
        HealthBar.ApplyDamage(value);
        
        if (CurrentHealth <= 0)
            Die?.Invoke();
    }
}