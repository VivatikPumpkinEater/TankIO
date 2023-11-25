using UnityEngine;

public class EnemyController : Unit<EnemyView>
{
    private readonly Transform _target;
    private readonly float _damage;

    private Vector2 TargetPosition => _target.position;

    private Vector2 Position => View.Position;
    
    public EnemyController(Transform target, EnemyView view, EnemiesSettings baseSettings) :
        base(view, baseSettings)
    {
        _target = target;
        _damage = baseSettings.Damage;
        
        View.Damaged += OnDamaged;
    }

    public override void ManualFixedUpdate(float fixedDeltaTime)
    {
        var direction = (TargetPosition - Position).normalized;
        View.Rigidbody2D.MovePosition(Position + direction * (1f * fixedDeltaTime));
    }

    private void OnDamaged(IDamageable damageable)
    {
        damageable.TakeDamage(_damage);
    }

    protected override void StartDie()
    {
        base.StartDie();
        
        View.Damaged -= OnDamaged;
        Object.Destroy(View.gameObject);
    }
}