using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyInfo {
    public enum GRADE { NORMAL,BOSS}
    public string name;
    public GRADE grade;
    public float speed;
    public int health;
    public int gear;
    public int score;

    public int modelingNumber;
}
