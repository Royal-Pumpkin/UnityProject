using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class TowerInfo
{
    
    public TowerManager.TOWERID id;
    public string name;
    [SerializeField] private int tier;
    [SerializeField] private int range;
    [SerializeField] private int damage;
    [SerializeField] private int autoAttackSpeed;
    [SerializeField] private int manualAttackSpeed;
    [SerializeField] private int turretTurnover;
    [SerializeField] private int price;

    public int modelingNumber;

    public SkillInfo[] skillInfos;

    public int Tier
    {
        get
        {
            return tier;
        }
    }

    public int Range
    {
        get
        {
            return ApplyUpgrade(range,0);
        }
    }

    public int Damage
    {
        get
        {
            return ApplyUpgrade(damage,1);
        }
    }

    public int AutoAttackSpeed
    {
        get
        {
            return ApplyUpgrade(autoAttackSpeed,2);
        }
    }

    public int ManualAttackSpeed
    {
        get
        {
            return ApplyUpgrade(manualAttackSpeed,2);
        }
    }

    public int TurretTurnover
    {
        get
        {
            return turretTurnover;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    [Serializable]
    public struct SkillInfo
    {
        public int skillNumber;
        public float value;
    }

    /// <summary>
    /// stat : 0 = range, 1 = damage, 2 = attack speed
    /// </summary>
    /// <param name="value"></param>
    /// <param name="stat"></param>
    /// <returns></returns>
    int ApplyUpgrade(int value,int stat)
    {
        value += MainManager.Instance.GetTowerUpgradeStat(id, (TowerUpradeManager.UPGRADESTAT)stat);
        value *= 1 + MainManager.Instance.GetTowerUpgradeStat(id, (TowerUpradeManager.UPGRADESTAT)(stat+1));
        return value;
    }
}
