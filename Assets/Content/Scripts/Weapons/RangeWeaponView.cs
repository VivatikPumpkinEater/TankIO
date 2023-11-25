using UnityEngine;

public class RangeWeaponView : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;

    public Vector2 UpVector => transform.up;
    
    public Vector2 ShotPoint => _shotPoint.position;

}