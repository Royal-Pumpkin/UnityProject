using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    public enum TowerName { A, B, C, D, AA, AB, AC, AD, BA, BB, BC, BD, CA, CB, CC, CD, DA, DB, DC, DD }

    [SerializeField] List<GameObject> towerPrefabs;
    List<List<GameObject>> towerPool = new List<List<GameObject>>();

    public void Init()
    {
        Transform tr = GetComponent<Transform>();
        for(int i = 0; i < towerPrefabs.Count; i++)
        {
            List<GameObject> temp = new List<GameObject>();
            //
            for(int j = 0; j < 10; j++)
            {
                GameObject tempObj = Instantiate<GameObject>(towerPrefabs[i],tr);
                temp.Add(tempObj);
            }
            towerPool.Add(temp);
        }
    }
	
    public GameObject GetTower(TowerName name)
    {
        List<GameObject> temp = towerPool[(int)name];
        GameObject obj = null;
        for(int i = 0; i<temp.Count; i++)
        {
            obj = temp[i];
        }
        if(obj == null)
        {
            obj = Instantiate<GameObject>(towerPrefabs[(int)name]);
            temp.Add(obj);
        }
        return obj;
    }
    

}
