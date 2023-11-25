using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem
{
    private readonly HUD _hud;
    private readonly PlayerInput _playerInput;
    private readonly List<WeaponData> _weapons = new();

    private int _currentWeaponIndex;
    private RangeWeaponController _currentWeapon;
    private BulletPool _bulletPool;

    public WeaponSystem(Transform weaponHolder, ActorType targetType, HUD hud, PlayerInput playerInput)
    {
        _hud = hud;
        _playerInput = playerInput;

        _bulletPool = new BulletPool(null);
        var weaponsModel = WeaponsConfig.GetAll();

        foreach (var model in weaponsModel)
        {
            var view = Object.Instantiate(model.WeaponView, weaponHolder);
            var controller = new RangeWeaponController(targetType, view, _bulletPool, model.Settings);
            controller.SetActive(false);
            _weapons.Add(new WeaponData() { Name = model.Name, Controller = controller });
        }

        _playerInput.Player.SwitchWeapon.started += OnSwitchWeaponClick;
        _playerInput.Player.Shoot.started += OnShootClick;

        SelectWeapon();
    }

    private void OnSwitchWeaponClick(InputAction.CallbackContext context)
    {
        var value = (int)context.ReadValue<float>();
        SwitchWeapon(value);
    }

    private void OnShootClick(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void SwitchWeapon(int value)
    {
        _currentWeapon?.SetActive(false);
        _currentWeaponIndex += value;

        if (_currentWeaponIndex >= _weapons.Count)
            _currentWeaponIndex = 0;

        if (_currentWeaponIndex < 0)
            _currentWeaponIndex = _weapons.Count - 1;

        SelectWeapon();
    }

    private void SelectWeapon()
    {
        var weaponData = _weapons[_currentWeaponIndex];
        _currentWeapon = weaponData.Controller;
        _currentWeapon.SetActive(true);
        
        _hud.UpdateWeaponName(weaponData.Name);
    }

    private void Shoot()
    {
        _currentWeapon?.Shot();
    }

    public void Dispose()
    {
        _playerInput.Player.SwitchWeapon.started -= OnSwitchWeaponClick;
        _playerInput.Player.Shoot.started -= OnShootClick;

        foreach (var weaponData in _weapons)
            weaponData.Controller.Dispose();
    }

    private struct WeaponData
    {
        public string Name;
        public RangeWeaponController Controller;
    }
}