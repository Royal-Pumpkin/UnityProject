using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIAlwaysOn : MonoBehaviour {
    //항상 켜져있는 GUI로 따로 스크립트 떼기
    int MonsterCount;
    float WaveBarGage;

    public UnityEngine.UI.Text Gear;
    public UnityEngine.UI.Text Life;
    public UnityEngine.UI.Text LeftTime;
    public UnityEngine.UI.Button PauseBtn;
    public UnityEngine.UI.Button TimerBtn;
    public UnityEngine.UI.Image WaveBar;


    public void SetAlwaysUI()
    {
        Gear.text = GameManager.stGameManager.mStageManager.nInGameGold.ToString();
        Life.text = GameManager.stGameManager.mStageManager.nPlayerLife.ToString();
    }

    public void GUIAlwaysOnInit(int _wavecount)
    {
        PauseBtn.onClick.AddListener(() => GameManager.stGameManager.SetGameState(GameManager.eGameState.PAUSE));


        MonsterCount = (_wavecount * 2);
        WaveBarGage = (1 / (float)MonsterCount);
        WaveBar.fillAmount = 0;
    }

    public void WaveBarControl()
    {
        WaveBar.fillAmount += WaveBarGage;
    }

    //만든이유를 모르겠음
    public void SetInfoText(float _RestTime)
    {
        LeftTime.text = _RestTime.ToString("N1");
        
    }

    public void SetBtninfo(IEnumerator _corutine, StageManager.eStageState _statgeState)
    {
        TimerBtn.onClick.RemoveAllListeners();
        TimerBtn.onClick.AddListener(() => StopTimer(_corutine, _statgeState));
        TimerBtn.onClick.AddListener(() => TimerBtn.onClick.RemoveAllListeners());
    }

    void StopTimer(IEnumerator _corutine, StageManager.eStageState _statgeState)
    {
        if (_statgeState == StageManager.eStageState.REST)
        {
            StopCoroutine(_corutine);
            GameManager.stGameManager.mStageManager.mStagestate = StageManager.eStageState.WAVE;
            GameManager.stGameManager.mStageManager.bDelay = false;
        }
    }
}
