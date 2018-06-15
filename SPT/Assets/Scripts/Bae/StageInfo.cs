using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StageInfo : MonoBehaviour {

    //[SerializeField] EnemyInfo[] enemyInfos;
    [SerializeField] EnemyCountSet[] enemyInfos;
    [SerializeField] Spawner[] spawners;

    [Serializable]
    public struct EnemyCountSet
    {
        public int enemyId;
        public int count;
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
    

}
