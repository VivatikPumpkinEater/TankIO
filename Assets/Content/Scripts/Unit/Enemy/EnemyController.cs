using UnityEngine;

public class EnemyController : Unit<EnemyView>
{
    private readonly Transform _target;

    private Vector2 TargetPosition => _target.position;

    private Vector2 Position => View.Position;
    
    public EnemyController(Transform target, EnemyView view, EnemiesSettings baseSettings) :
        base(view, baseSettings)
    {
        _target = target;
    }

    public override void ManualFixedUpdate(float fixedDeltaTime)
    {
        var direction = (TargetPosition - Position).normalized;
        View.Rigidbody2D.MovePosition(Position + direction * (1f * fixedDeltaTime));
    }
}