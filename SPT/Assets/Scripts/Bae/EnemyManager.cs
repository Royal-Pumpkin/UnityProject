using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> EnemyModelingList;
    [SerializeField] List<EnemyInfo> EnemyInfoList;

    public EnemyInfo GetEnemyInfo(int num)
    {
        return EnemyInfoList[num];
    }
    public GameObject GetEnemyModeling(int num)
    {
        return EnemyModelingList[num];
    }
}
