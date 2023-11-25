using UnityEngine;

public class RangeWeaponView : MonoBehaviour
{
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] private Bullet _prefab;

    protected Vector2 UpVector => transform.up;
}