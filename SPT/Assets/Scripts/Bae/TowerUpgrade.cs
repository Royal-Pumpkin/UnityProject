using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class TowerUpgrade
{
    [SerializeField] string name;
    [SerializeField] TowerManager.TOWERNAME eName;
    [SerializeField] private int rangePlus;
    [SerializeField] private int rangeProduct;
    [SerializeField] private int damagePlus;
    [SerializeField] private int damageProduct;
    [SerializeField] private int attackSpeedPlus;
    [SerializeField] private int attackSpeedProduct;
    public TowerUpgrade(TowerManager.TOWERNAME eName)
    {
        this.eName = eName;
    }
#if UNITY_EDITOR
    public void SetName()
    {
        name = eName.ToString();
    }
#endif

    public int GetStat(TowerUpradeManager.UPGRADESTAT stat)
    {
        int temp = 0;
        switch (stat)
        {
            case TowerUpradeManager.UPGRADESTAT.RANGE_PLUS:
                temp = rangePlus;
                break;
            case TowerUpradeManager.UPGRADESTAT.RANGE_PRODUCT:
                temp = rangeProduct;
                break;
            case TowerUpradeManager.UPGRADESTAT.DAMAGE_PLUS:
                temp = damagePlus;
                break;
            case TowerUpradeManager.UPGRADESTAT.DAMAGE_PRODUCT:
                temp = damageProduct;
                break;
            case TowerUpradeManager.UPGRADESTAT.ATTACK_SPEED_PLUS:
                temp = attackSpeedPlus;
                break;
            case TowerUpradeManager.UPGRADESTAT.ATTACK_SPEED_PRODUCT:
                temp = attackSpeedProduct;
                break;
        }
        return temp;
    }
    public void ChangeStat(TowerUpradeManager.UPGRADESTAT stat,int value)
    {
        switch (stat)
        {
            case TowerUpradeManager.UPGRADESTAT.RANGE_PLUS:
                rangePlus+= value;
                break;
            case TowerUpradeManager.UPGRADESTAT.RANGE_PRODUCT:
                rangeProduct += value;
                break;
            case TowerUpradeManager.UPGRADESTAT.DAMAGE_PLUS:
                damagePlus += value;
                break;
            case TowerUpradeManager.UPGRADESTAT.DAMAGE_PRODUCT:
                damageProduct += value;
                break;
            case TowerUpradeManager.UPGRADESTAT.ATTACK_SPEED_PLUS:
                attackSpeedPlus += value;
                break;
            case TowerUpradeManager.UPGRADESTAT.ATTACK_SPEED_PRODUCT:
                attackSpeedProduct += value;
                break;
        }
    }
}

    

