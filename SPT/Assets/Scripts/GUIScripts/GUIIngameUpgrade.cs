using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIIngameUpgrade : MonoBehaviour {
    public GameObject ImagePrefab;
    public GameObject Panel;
    public List<GameObject> listUpgradeBtn = new List<GameObject>();

    public Button ElseView; //이걸 하나하나마다 만들것인지 노말뷰에서 기능만 달리할건지 고민

    public void InstansiateList( )
    {
        ElseView.onClick.AddListener(() => gameObject.SetActive(false));
        ElseView.onClick.AddListener(() => GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.NOMAL));

        //이 아래 함수는 원래 리스트가 있으면 리스트를 이용해서 for문돌리면 되는데 지금은 임시로 수동으로 하겠습니다.
        for (int i=0;i<2;i++)
        {
            GameObject tempobj = Instantiate(ImagePrefab, Panel.transform);
            listUpgradeBtn.Add(tempobj);

        }
        GUITowerImage temp = listUpgradeBtn[0].GetComponent<GUITowerImage>();
        temp.mTowerType = BuildManager.eTowerType.AA;
        temp = listUpgradeBtn[1].GetComponent<GUITowerImage>();
        temp.mTowerType = BuildManager.eTowerType.AB;

        
    }

    //얘는 빌드 ui만들때처럼 부를때 마다 해줘야한다. 어떤 Tower인지에 대한 정보가 없기때문
    public void SetBtnFunction(Tower _pickedtower)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < listUpgradeBtn.Count; i++)
        {
            GUITowerImage tempGUIImage = listUpgradeBtn[i].GetComponent<GUITowerImage>();
            tempGUIImage.Btn.onClick.RemoveAllListeners();
        }


        Button tempBtn = listUpgradeBtn[0].GetComponent<Button>();
        tempBtn.onClick.AddListener(() => _pickedtower.TowerUpgrade(listUpgradeBtn[0].GetComponent<GUITowerImage>().mTowerType));
        tempBtn.onClick.AddListener(() => gameObject.SetActive(false));
        tempBtn = listUpgradeBtn[1].GetComponent<Button>();
        tempBtn.onClick.AddListener(() => _pickedtower.TowerUpgrade(listUpgradeBtn[0].GetComponent<GUITowerImage>().mTowerType));
        tempBtn.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
