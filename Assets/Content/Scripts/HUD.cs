using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ProtectionBar _protectionBar;

    public (HealthBar healthBar, ProtectionBar protectionBar) Bars => (_healthBar, _protectionBar);
}