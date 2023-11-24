using UnityEngine;

public class PlayerView : UnitView
{
    [SerializeField] private Transform _weaponHolder;

    public Transform WeaponHolder => _weaponHolder;
}