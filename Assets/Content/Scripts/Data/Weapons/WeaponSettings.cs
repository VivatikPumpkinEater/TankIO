using System;

[Serializable]
public class WeaponSettings
{
    public BulletTypes BulletType;
    public float Damage;
    public float Speed;

    public bool FiringInBursts;
    public BurstsSettings BurstsSettings;

    public bool ShootingFractions;
    public ShootingFractionsSettings FractionsSettings;
}