using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class TowerInfo
{
    public string name;
    public int tier;
    public int range;

    public SkillInfo[] skillInfos;

    [Serializable]
    public struct SkillInfo
    {
        public int skillNumber;
        public float value;
    }
}
