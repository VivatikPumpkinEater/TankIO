using UnityEngine;

public abstract class BaseRangeWeapon : MonoBehaviour
{
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] private Bullet _prefab;

    protected Vector2 UpVector => transform.up;
    
    public abstract void Shoot();
}