using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public enum TowerName { A, B, C, D, AA, AB, AC, AD, BA, BB, BC, BD, CA, CB, CC, CD, DA, DB, DC, DD }

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
	public TowerInfo GetTowerInfo(TowerName name)
    {
        return towerInfoList[(int)name];
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
