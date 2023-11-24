using UnityEngine;

public abstract class UnitView : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ProtectionBar _protectionBar;
    
    public HealthBar HealthBar => _healthBar;
    public ProtectionBar ProtectionBar => _protectionBar;
}