using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoManager : MonoBehaviour {

    [SerializeField] List<GameObject> mapPrefabs;

    public GameObject GetMap(int stageNumber)
    {
        return mapPrefabs[stageNumber];
    }
}
