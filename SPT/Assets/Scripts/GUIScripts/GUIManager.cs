using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {
    public List<GameObject> mListGUIScene = new List<GameObject>();
    public enum eGUISceneName {NULL=-1,NOMALSCENE,CONTROLSCENE,GAMEOVER,CLEAR}
    eGUISceneName mGUISceneState;
    public GUIControlMode mGUIControlMode;
    public GUINomalMode mGUINomalMode;

    public UnityEngine.UI.Text Gear;
    public UnityEngine.UI.Text Life;
    public UnityEngine.UI.Text LeftTime;
    public UnityEngine.UI.Button PauseBtn;
    public UnityEngine.UI.Button tempBtn;

    // Use this for initialization
    void Start () {
        mGUISceneState = eGUISceneName.NOMALSCENE;
        PauseBtn.onClick.AddListener(() => GameManager.stGameManager.SetGameState(GameManager.eGameState.PAUSE));
    }
	
	// Update is called once per frame
	void Update () {

        //나중에 스택으로 바꾸던지 해서 업데이트에서 관리안하고 바뀔때만 부르는 함수로 만들기
		for(int i=0; i<mListGUIScene.Count;i++)
        {
            if ((int)mGUISceneState == i)
            {
                mListGUIScene[i].SetActive(true);
            }
            else
                mListGUIScene[i].SetActive(false);
        }


        Gear.text = GameManager.stGameManager.mStageManager.nInGameGold.ToString();
        Life.text = GameManager.stGameManager.mStageManager.nPlayerLife.ToString();
    }

    public void SetInfoText(float _RestTime)
    {
        LeftTime.text =_RestTime.ToString("N1");
    }

    public void SetBtninfo(IEnumerator _corutine, StageManager.eStageState _statgeState)
    {
        tempBtn.onClick.RemoveAllListeners();
        tempBtn.onClick.AddListener(() => StopTimer(_corutine, _statgeState));
        tempBtn.onClick.AddListener(() => tempBtn.onClick.RemoveAllListeners());
    }

    void StopTimer(IEnumerator _corutine , StageManager.eStageState _statgeState)
    {
        if (_statgeState == StageManager.eStageState.REST)
        {
            StopCoroutine(_corutine);
            GameManager.stGameManager.mStageManager.mStagestate = StageManager.eStageState.WAVE;
            GameManager.stGameManager.mStageManager.bDelay = false;
        }
    }

    public void SetGUIScene(eGUISceneName _GUISceneName)
    {
        mGUISceneState = _GUISceneName;
    }

    public eGUISceneName GetGUIScene()
    {
        return mGUISceneState;
    }
}
