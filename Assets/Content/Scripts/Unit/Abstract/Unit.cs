using System;

public abstract class Unit<T> : IUnit
    where T : UnitView
{
    public event Action<IUnit> Die;

    protected readonly T View;
    
    protected readonly HealthBar HealthBar;
    protected readonly ProtectionBar ProtectionBar;
    
    public bool IsAlive => CurrentHealth > 0;

    protected float CurrentHealth => HealthBar.Value;

    protected float ProtectionFactor => ProtectionBar.Value;
    
    protected Unit(T view, BaseUnitSettings baseSettings)
    {
        View = view;

        HealthBar = view.HealthBar;
        HealthBar.Init(baseSettings.Health);
        
        ProtectionBar = view.ProtectionBar;
        var protectiveFactor = 1f - baseSettings.ProtectionFactor;
        ProtectionBar.Init(protectiveFactor);

        View.Damaged += OnTakeDamage;
    }

    public virtual void ManualUpdate(float deltaTime)
    {
        if (!IsAlive)
            return;
    }
    
    public virtual void ManualFixedUpdate(float fixedDeltaTime)
    {
        if (!IsAlive)
            return;
    }
    
    private void OnTakeDamage(float value)
    {
        var damage = value * ProtectionFactor;
        
        HealthBar.ApplyDamage(damage);
        
        if (CurrentHealth <= 0)
            StartDie();
    }

    protected virtual void StartDie()
    {
        Die?.Invoke(this);
        
        View.Damaged -= OnTakeDamage;
    }
}