﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour {
    private static MainManager instance = null;
    public MainGUIManager mainGUI;
    public Player player;
    public Setting setting;
    public TowerManager towerManager;
    public int lastStage;//준비된 마지막 스테이지 넘버
    private int currentStage;
    private int difficulty=1;

    
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

    public int Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
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
        SQLiteUtil.VaildCheckDB();
        Dictionary<string, object> playerInfo = SQLiteUtil.PlayerInfo();        
        List<Dictionary<string, object>> stageClearInfo = SQLiteUtil.StageClearInfo();
        List<Dictionary<string, object>> towerUpgradeInfo = SQLiteUtil.TowerUpgradeTreeInfo();


        player.Init((int)playerInfo["gold"], (int)playerInfo["gem"], (int)playerInfo["key"], (string)playerInfo["key_recovery_time"], 12, stageClearInfo, towerUpgradeInfo);
        towerManager.Init();
        mainGUI.Init(player);
    }

    void DebugSelectList(List<Dictionary<string, System.Object>> selectList)
    {
        Debug.Log("Count : " + selectList.Count);
        if (selectList.Count > 0)
        {
            string keyNames = "";
            Dictionary<string, System.Object>.KeyCollection.Enumerator enums= selectList[0].Keys.GetEnumerator();
            while (enums.MoveNext())
            {
                keyNames += enums.Current+ " ";
            }
            Debug.Log(keyNames);
        }
        
        for (int i=0; i<selectList.Count; i++)
        {
            Dictionary<string, System.Object>.ValueCollection.Enumerator enums = selectList[i].Values.GetEnumerator();
            string keyNames = "";
            while (enums.MoveNext())
            {
                keyNames += enums.Current + " ";
            }
            Debug.Log(keyNames);
        }
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

    public void StageStart(int stageNumber)
    {
        Player.StageClearInfo info = player.GetStageClearInfo(stageNumber, difficulty);
        if (!info.clear)
        {
            if (stageNumber > 1)
            {
                Player.StageClearInfo preInfo = player.GetStageClearInfo(stageNumber-1, difficulty);
                if (!preInfo.clear)
                {
                    mainGUI.notice.OnNotice(2);
                    return;
                }
            }
        }

        currentStage = stageNumber;
        mainGUI.OnOffMainGUI(false);
        SceneManager.LoadScene("SampleScene");
        ChangeKey(-1);
    }
    
    public void OnOffSet(Setting.SETKIND kind, bool value)
    {
        setting.OnOffSet(kind, value);
    }

    /// <summary>
    /// 스테이지 클리어하고 씬이 넘어가야 할 시점에서 호출
    /// </summary>
    /// <param name="gold"></param>
    public void ClearStage(int gold,int star)
    {
        if (currentStage == (player.LastStage+1))
        {
            player.ClearLastStage();
        }
        ChangeGold(gold);

        LoadMainScene();
    }

    /// <summary>
    /// 스테이지 실패하고 씬이 넘어가야 할 시점에서 호출
    /// </summary>
    public void FailStage()
    {
        LoadMainScene();
    }

    void LoadMainScene()
    {
        mainGUI.OnOffMainGUI(true);
        SceneManager.LoadScene("Main");
    }
}
