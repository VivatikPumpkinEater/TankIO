using TMPro;
using UnityEngine;

public abstract class BaseBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueTxt;
    
    public float MaxValue { get; private set; }
    public float Value { get; private set; }

    public void Init(float defaultValue)
    {
        MaxValue = defaultValue;
        Value = defaultValue;
        
        UpdateUI(Value);
    }

    public void ApplyDamage(float value)
    {
        Value -= value;
        UpdateUI(Value);
    }

    protected void UpdateUI(float value)
    {
        _valueTxt.text = $"{value}";
    }
}