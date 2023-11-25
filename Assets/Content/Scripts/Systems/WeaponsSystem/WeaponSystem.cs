using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem
{
    private readonly HUD _hud;
    private readonly List<WeaponData> _weapons = new();

    private int _currentWeaponIndex;
    private RangeWeaponController _currentWeapon;
    private BulletPool _bulletPool;

    public WeaponSystem(Transform weaponHolder, ActorType targetType, HUD hud)
    {
        _hud = hud;

        _bulletPool = new BulletPool(null);
        var weaponsModel = WeaponsConfig.GetAll();

        foreach (var model in weaponsModel)
        {
            var view = Object.Instantiate(model.WeaponView, weaponHolder);
            var controller = new RangeWeaponController(targetType, view, _bulletPool, model.Settings);
            controller.SetActive(false);
            _weapons.Add(new WeaponData(){Name = model.Name, Controller = controller});
        }
        
        SelectWeapon();
    }

    public void SwitchWeapon()
    {
        _currentWeapon?.SetActive(false);
        _currentWeaponIndex++;
        
        if (_currentWeaponIndex >= _weapons.Count)
            _currentWeaponIndex = 0;
        
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        var weaponData = _weapons[_currentWeaponIndex];
        _currentWeapon = weaponData.Controller;
        _currentWeapon.SetActive(true);
        
        _hud.UpdateWeaponName(weaponData.Name);
    }

    public void Shot()
    {
        _currentWeapon?.Shot();
    }

    private struct WeaponData
    {
        public string Name;
        public RangeWeaponController Controller;
    }
}