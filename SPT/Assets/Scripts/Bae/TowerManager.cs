using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public enum TOWERNAME { A, B, C, AA, AB, AC }
    public enum TOWERID { T1001, T1002, T1003, T1011, T1012, T1013 }
    [SerializeField] List<GameObject> towerModelingPrefabs;
    List<List<GameObject>> towerModelingPool = new List<List<GameObject>>();

    [SerializeField] List<TowerInfo> towerInfoList;
    public void Init()
    {
        Transform tr = GetComponent<Transform>();
        for(int i = 0; i < towerModelingPrefabs.Count; i++)
        {
            List<GameObject> temp = new List<GameObject>();
            //
            for(int j = 0; j < 10; j++)
            {
                GameObject tempObj = Instantiate<GameObject>(towerModelingPrefabs[i],tr);
                temp.Add(tempObj);
            }
            towerModelingPool.Add(temp);
        }
    }
	public TowerInfo GetTowerInfo(TOWERNAME name)
    {
        return towerInfoList[(int)name];
    }
    public TowerInfo GetTowerInfo(TOWERID id)
    {
        return towerInfoList[(int)id];
    }
    public GameObject GetTowerModeling(int modelingNumber)
    {
        List<GameObject> temp = towerModelingPool[modelingNumber];
        GameObject obj = null;
        for(int i = 0; i<temp.Count; i++)
        {
            obj = temp[i];
        }
        if(obj == null)
        {
            obj = Instantiate<GameObject>(towerModelingPrefabs[modelingNumber]);
            temp.Add(obj);
        }
        return obj;
    }



}
