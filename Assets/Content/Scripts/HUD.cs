using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ProtectionBar _protectionBar;

    [SerializeField] private TMP_Text _weaponName;

    public (HealthBar healthBar, ProtectionBar protectionBar) Bars => (_healthBar, _protectionBar);

    public void UpdateWeaponName(string value)
    {
        _weaponName.text = value;
    }
}