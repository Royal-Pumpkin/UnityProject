using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIModeSelect : MonoBehaviour {
    public GameObject Panel; //타워픽이벤트에서 얘를 활성화 시켜준다
    public GUIIngameUpgrade mGUIIngameUpgrade;

    public Button ElseView; //이걸 하나하나마다 만들것인지 노말뷰에서 기능만 달리할건지 고민

    public Button UpgradeBtn; //타워픽이벤트이후 활성화 된 판낼안에 있음
    public Button GetinBtn; //타워픽이벤트이후 활성화 된 판낼안에 있음

    
    public void SetPanelOn(Tower _pickedTower)
    {
        gameObject.SetActive(true);
        _pickedTower.bSeselect = true;

        UpgradeBtn.onClick.RemoveAllListeners();
        GetinBtn.onClick.RemoveAllListeners();
        ElseView.onClick.RemoveAllListeners(); //이거는 시작할 때 한번만 부르면됨 나중에 옮기도록함 초기화 함수 만들어서

        UpgradeBtn.onClick.AddListener(() => mGUIIngameUpgrade.SetBtnFunction(_pickedTower));
        GetinBtn.onClick.AddListener(() => _pickedTower.TowerGetin(Camera.main));

        UpgradeBtn.onClick.AddListener(() => gameObject.SetActive(false));
        GetinBtn.onClick.AddListener(() => gameObject.SetActive(false));

        UpgradeBtn.onClick.AddListener(() => _pickedTower.bSeselect = false);
        GetinBtn.onClick.AddListener(() => _pickedTower.bSeselect = false);


        ElseView.onClick.AddListener(() => gameObject.SetActive(false));
        ElseView.onClick.AddListener(() => GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.NOMAL));
        ElseView.onClick.AddListener(() => _pickedTower.bSeselect = false);
    }

}
