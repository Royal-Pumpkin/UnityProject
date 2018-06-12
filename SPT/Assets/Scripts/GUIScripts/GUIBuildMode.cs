using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIBuildMode : MonoBehaviour {
    public GameObject Panel;
    public UnityEngine.UI.Button ElseView; //이걸 하나하나마다 만들것인지 노말뷰에서 기능만 달리할건지 고민
    public GameObject TowerImagePrefab;
    List<GameObject> BuilderableTowerImage = new List<GameObject>();

    

    //GUI용 타워 리스트 public은 나중에 없애봄
    public List<BuildManager.eTowerType> GUIPlayerTower;

    public void SetPanelOn(Transform _selectnode,Vector3 _offset)
    {
        gameObject.SetActive(true);

        for(int i = 0; i < BuilderableTowerImage.Count; i++)
        {
            GUITowerImage tempGUIImage = BuilderableTowerImage[i].GetComponent<GUITowerImage>();
            tempGUIImage.Btn.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < BuilderableTowerImage.Count; i++)
        {
            GUITowerImage tempGUIImage = BuilderableTowerImage[i].GetComponent<GUITowerImage>();
            tempGUIImage.mTowerType = GUIPlayerTower[i];
            tempGUIImage.Btn.onClick.AddListener(() => GameManager.stGameManager.mBuildManager.BuildTower(tempGUIImage.mTowerType,_selectnode, _offset));
            tempGUIImage.Btn.onClick.AddListener(() => gameObject.SetActive(false));
        }
    }

    public bool InstansiateList()
    {
        if (!TowerImagePrefab)
            return false;

        ElseView.onClick.AddListener(() => gameObject.SetActive(false));
        ElseView.onClick.AddListener(() => GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.NOMAL));

        for (int i = 0; i < 3/*tower종류 리스트 다불러서 그 횟수만큼(지금은 3개밖에없으니 임시로)*/; i++)
        {
            GameObject tempobj = Instantiate(TowerImagePrefab, Panel.transform);
            
            BuilderableTowerImage.Add(tempobj);
        }

        return true;
    }

}
