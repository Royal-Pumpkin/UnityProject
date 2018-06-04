using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIBuildMode : MonoBehaviour {
    public GameObject Panel;
    public GameObject TowerImagePrefab;
    List<GameObject> BuilderableTowerImage = new List<GameObject>();

    //GUI용 타워 리스트 public은 나중에 없애봄
    public List<BuildManager.eTowerType> GUIPlayerTower;

    public void SetPanelOn()
    {
        Panel.SetActive(true);
    }

    public void SetListOnPanel()
    {
        
    }

    public bool InstansiateList()
    {
        if (!TowerImagePrefab)
            return false;

        for (int i = 0; i < 3/*tower종류 리스트 다불러서 그 횟수만큼(지금은 3개밖에없으니 임시로)*/; i++)
        {
            GameObject tempobj = Instantiate(TowerImagePrefab, Panel.transform);
            GUITowerImage tempGUIImage = TowerImagePrefab.GetComponent<GUITowerImage>();
            tempGUIImage.mTowerType = GUIPlayerTower[i];
            //tempGUIImage.Btn.onClick.AddListener(()=> )
            BuilderableTowerImage.Add(tempobj);
        }

        return true;
    }

}
