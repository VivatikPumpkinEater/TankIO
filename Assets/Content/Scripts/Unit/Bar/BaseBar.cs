using TMPro;
using UnityEngine;

public abstract class BaseBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueTxt;

    public float MaxValue { get; private set; }
    public float Value { get; private set; }
    
    protected abstract string Prefix { get; }
    protected abstract string Postfix { get; }

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

    protected virtual void UpdateUI(float value)
    {
        _valueTxt.text = $"{Prefix}{value}{Postfix}";
    }
}