using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StageInfo : MonoBehaviour {

    //[SerializeField] EnemyInfo[] enemyInfos;
    public EnemyCountSet[] enemyInfos;

    [SerializeField] Diffilculty[] diffilculties;

    public int gear;
    public int life;

    [Serializable]
    public struct EnemyCountSet
    {
        public int enemyId;
        public int count;
    }

    [Serializable]
    public struct Diffilculty
    {
        public Spawner[] spawners;
    }

    [Serializable]
    public struct Spawner
    {
        public WaveInfo[] waveInfos;
    }
    [Serializable]
    public struct WaveInfo
    {
        public WaveSet[] waveSets;
        
    }
    [Serializable]
    public struct WaveSet
    {
        public int enemyId;
        public int count;
        public float spawnTime;
    }

    public Spawner[] GetWave(int difficulty)
    {
        return diffilculties[difficulty - 1].spawners;
    }
}
