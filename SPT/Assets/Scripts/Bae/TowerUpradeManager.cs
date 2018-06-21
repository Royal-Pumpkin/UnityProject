using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
[Serializable]
public class TowerUpradeManager
{
    public enum UPGRADESTAT { RANGE_PLUS, RANGE_PRODUCT, DAMAGE_PLUS, DAMAGE_PRODUCT, ATTACK_SPEED_PLUS, ATTACK_SPEED_PRODUCT }
    [SerializeField] List<TowerUpgrade> towerUpgrades = new List<TowerUpgrade>();
    public TowerUpradeManager()
    {
        for(int i=0;i< System.Enum.GetValues(typeof(TowerManager.TOWERID)).Length; i++)
        {
            towerUpgrades.Add(new TowerUpgrade((TowerManager.TOWERNAME)i));
        }

    }
#if UNITY_EDITOR
    public void SetName()
    {
        for (int i = 0; i < towerUpgrades.Count; i++)
        {
            towerUpgrades[i].SetName();
        }
    }
#endif
    public int GetUpgradeStat(TowerManager.TOWERID id, UPGRADESTAT stat)
    {
        return towerUpgrades[(int)id].GetStat(stat);
    }
    public void Upgrade(int number)
    {
        switch (number)
        {
            case 1:
                UpgradeTower(UPGRADESTAT.DAMAGE_PLUS, 30, TowerManager.TOWERNAME.A, TowerManager.TOWERNAME.AA);
                break;
            case 2:
                UpgradeTower(UPGRADESTAT.RANGE_PLUS, 15, TowerManager.TOWERNAME.A, TowerManager.TOWERNAME.AA);
                break;
            case 3:
                UpgradeTower(UPGRADESTAT.DAMAGE_PLUS, 10, TowerManager.TOWERNAME.AB);
                break;
            case 4:
                UpgradeTower(UPGRADESTAT.ATTACK_SPEED_PLUS, 250, TowerManager.TOWERNAME.AB);
                break;
            case 5:
                UpgradeTower(UPGRADESTAT.DAMAGE_PLUS, 100, TowerManager.TOWERNAME.AC);
                break;
        }
    }
    void UpgradeTower(UPGRADESTAT stat,int value,params TowerManager.TOWERID[] id)
    {
        for(int i = 0; i < id.Length; i++)
        {
            towerUpgrades[(int)id[i]].ChangeStat(stat, value);
        }
    }
    void UpgradeTower(UPGRADESTAT stat, int value, params TowerManager.TOWERNAME[] name)
    {
        for (int i = 0; i < name.Length; i++)
        {
            towerUpgrades[(int)name[i]].ChangeStat(stat, value);
        }
    }
}
