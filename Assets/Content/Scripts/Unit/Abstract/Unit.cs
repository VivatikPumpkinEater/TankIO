using System;
using Object = UnityEngine.Object;

public abstract class Unit<T> : IUnit
    where T : UnitView
{
    public Action<IUnit> Die;

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
        ProtectionBar.Init(baseSettings.ProtectionFactor);

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

    private void StartDie()
    {
        Die?.Invoke(this);
        
        View.Damaged -= OnTakeDamage;
        Object.Destroy(View.gameObject);
    }
}