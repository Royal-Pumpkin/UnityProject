using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class TowerInfo
{
    
    public TowerManager.TOWERID id;
    public string name;
    public int tier;
    public int range;
    public int damage;
    public int autoAttackSpeed;
    public int manualAttackSpeed;
    public int turretTurnover;
    public int price;

    public int modelingNumber;

    public SkillInfo[] skillInfos;

    [Serializable]
    public struct SkillInfo
    {
        public int skillNumber;
        public float value;
    }
}
