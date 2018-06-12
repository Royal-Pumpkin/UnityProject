using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour {
    private static MainManager instance = null;
    public MainGUIManager mainGUI;
    public Player player;
    public Setting setting;
    public int lastStage;//준비된 마지막 스테이지 넘버
    private int currentStage;
    public static MainManager Instance
    {
        get { return instance; }
    }

    public int CurrentStage
    {
        get
        {
            return currentStage;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
    }

    public void ChangeValueSet(Setting.SETKIND kind, int value)
    {
        setting.ChangeValue(kind, value);
    }

    private void Start()
    {
        //초기화는 데이터 읽어와서 하는걸로
        player.Init(100,100,5,12,0);
        mainGUI.Init(player);
    }

    public void ChangeGold(int value)
    {
        mainGUI.SetGoldText(player.ChangeGold(value));
    }
    public void ChangeDia(int value)
    {
        mainGUI.SetDiaText(player.ChangeKey(value));
    }
    public void ChangeKey(int value)
    {
        mainGUI.SetKeyText(player.ChangeKey(value),player.MaxKey);
    }
    public void ChangeMaxKey(int value)
    {
        mainGUI.SetKeyText(player.Key,player.PlusMaxKey(value));
    }

    public int GetGold()
    {
        return player.Gold;
    }
    public int GetDia()
    {
        return player.Diamond;
    }

    public void StageStart()
    {
        SceneManager.LoadScene("SampleScene");
        ChangeKey(-1);
    }
    
    public void OnOffSet(Setting.SETKIND kind, bool value)
    {
        setting.OnOffSet(kind, value);
    }

    public void ClearStage(int gold)
    {
        if (currentStage == (player.LastStage+1))
        {
            player.ClearLastStage();
        }
        ChangeGold(gold);

        
    }
    public void FailStage()
    {

    }
}
