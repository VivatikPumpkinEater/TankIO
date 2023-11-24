using UnityEngine;

public class Bullet : PoolObject, IFixedUpdate
{
    private Rigidbody2D _rigidbody2D;

    private bool _isActive;

    private Vector2 _direction;
    private float _speed;
    private float _damage;
    private float _maxDistance;
    private Vector2 _startPosition;
    private Rigidbody2D Rb => _rigidbody2D ??= GetComponent<Rigidbody2D>();

    public void Init(Vector2 direction, float maxDistance, float speed, float damage)
    {
        _startPosition = transform.position;
        _direction = direction;
        _maxDistance = maxDistance;
        _speed = speed;
        _damage = damage;

        _isActive = true;
    }
    
    public void ManualFixedUpdate(float fixedDeltaTime)
    {
        if (!_isActive)
            return;
        
        if (Vector2.Distance(_startPosition, Rb.position) >= _maxDistance)
            ReturnToPool();
        
        Rb.AddForce(_direction * _speed);
        Rb.velocity = Vector2.ClampMagnitude(Rb.velocity, _speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_isActive)
            return;

        var damageable = other.collider.GetComponent<IDamageable>();
        if (damageable == null)
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