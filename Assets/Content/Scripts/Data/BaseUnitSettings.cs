using System;
using UnityEngine;

[Serializable]
public class BaseUnitSettings
{
    public float Health;
    
    [Range(0f, 0.9f)]
    public float ProtectionFactor;
    public float MovementSpeed;
}