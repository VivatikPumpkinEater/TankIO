using UnityEngine;

public class Bullet : PoolObject
{
    private Rigidbody2D _rigidbody2D;

    private bool _isActive;

    private ActorType _targetType;
    private Vector2 _direction;
    private float _speed;
    private float _damage;
    private float _maxDistance;
    private Vector2 _startPosition;
    
    private Rigidbody2D Rb => _rigidbody2D ??= GetComponent<Rigidbody2D>();

    public void Init(ActorType targetType, Vector2 direction, float maxDistance, float speed, float damage)
    {
        _targetType = targetType;
        _startPosition = transform.position;
        _direction = direction;
        _maxDistance = maxDistance;
        _speed = speed;
        _damage = damage;

        _isActive = true;
    }
    
    public void FixedUpdate()
    {
        if (!_isActive)
            return;
        
        if (Vector2.Distance(_startPosition, Rb.position) >= _maxDistance)
            ReturnToPool();
        
        Rb.AddForce(_direction * _speed);
        Rb.velocity = Vector2.ClampMagnitude(Rb.velocity, _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive)
            return;

        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null)
            return;
        
        if (damageable.ActorType != _targetType)
            return;
        
        damageable.TakeDamage(_damage);
        
        ReturnToPool();
    }

    protected override void ReturnToPool()
    {
        _isActive = false;

        Rb.velocity = Vector2.zero;
        Rb.angularVelocity = 0f;
        
        base.ReturnToPool();
    }
}