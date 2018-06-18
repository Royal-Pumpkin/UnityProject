using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerPrefsUtil
{
    public void GetPlayerData()
    {
        
    }
    public static bool IsPlay
    {
        get { return PlayerPrefs.HasKey("IsPlay"); }
        
    }
    public static void SetIsPlay()
    {
        PlayerPrefs.SetInt("IsPlay", 1);
    }
    public static List<Player.StageClearInfo[]> InitStageInfoList()
    {
        List<Player.StageClearInfo[]> stageClearInfoList = new List<Player.StageClearInfo[]>();

        for (int i = 0; i < 20; i++)
        {
            Player.StageClearInfo[] stageClearInfos = new Player.StageClearInfo[3];
            for (int j = 0; j < 3; j++)
            {
                stageClearInfos[j].star = 0;
                stageClearInfos[j].clear = false;

                PlayerPrefs.SetInt("StageClearInfo.star" + string.Format("{0:D2}{1:D2}", i+1, j+1), 0);
                PlayerPrefs.SetInt("StageClearInfo.clear" + string.Format("{0:D2}{1:D2}", i+1, j+1), 0);
            }
            stageClearInfoList.Add(stageClearInfos);
        }
        PlayerPrefs.Save();
        return stageClearInfoList;
    } 
    public static List<Player.StageClearInfo[]> GetStageInfoList()
    {
        List<Player.StageClearInfo[]> stageClearInfoList = new List<Player.StageClearInfo[]>();

        for (int i = 0; i < 20; i++)
        {
            Player.StageClearInfo[] stageClearInfos = new Player.StageClearInfo[3];
            for (int j = 0; j < 3; j++)
            {
                stageClearInfos[j].star = PlayerPrefs.GetInt("StageClearInfo.star" + string.Format("{0:D2}{1:D2}", i, j));
                stageClearInfos[j].clear = PlayerPrefs.GetInt("StageClearInfo.clear" + string.Format("{0:D2}{1:D2}", i, j))==0?false:true;                
            }
            stageClearInfoList.Add(stageClearInfos);
        }
        return stageClearInfoList;
    }

    public static void SaveStageClearInfoData(int stage, int difficulty, int star)
    {
        PlayerPrefs.SetInt("StageClearInfo.star" + string.Format("{0:D2}{1:D2}", stage, difficulty), star);
        PlayerPrefs.SetInt("StageClearInfo.clear" + string.Format("{0:D2}{1:D2}", stage, difficulty), 1);
        PlayerPrefs.Save();
    }

    public static void SettilngTowerTreeUsable(List<Player.TowerTreeNode[]> towerTree)
    {
        for (int i = 0; i < towerTree.Count; i++)
        {
            for (int j = 0; j < towerTree[i].Length; j++)
            {
                towerTree[i][j].usable = PlayerPrefs.GetInt("TowerTreeNode.usable" + string.Format("{0:D2}{1:D2}", i + 1, j + 1));
            }
        }
    }
    public static void AllSaveTowerTreeUsable(List<Player.TowerTreeNode[]> towerTree)
    {
        for (int i = 0; i < towerTree.Count; i++)
        {
            for (int j = 0; j < towerTree[i].Length; j++)
            {
                SaveTowerTreeNodeUsable(i + 1, j + 1, towerTree[i][j].usable, false);
                 ;
            }
        }
        PlayerPrefs.Save();
    }
    
    public static void SaveTowerTreeNodeUsable (int tree, int nodeNum, int usable, bool isSave=true)
    {
        PlayerPrefs.SetInt("TowerTreeNode.usable" + string.Format("{0:D2}{1:D2}", tree, nodeNum), usable);
        if (isSave)
        {
            PlayerPrefs.Save();
        }        
    }

    public static int PlayerGold
    {
        set
        { 
            PlayerPrefs.SetInt("Player.gold",value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt("Player.gold");
        }
    }
    public static int PlayerGem
    {
        set
        {
            PlayerPrefs.SetInt("Player.gem", value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt("Player.gem");
        }
    }
    public static int PlayerKey
    {
        set
        {
            PlayerPrefs.SetInt("Player.key", value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt("Player.key");
        }
    }
    public static string PlayerKeyRecovryTime
    {
        set
        {
            PlayerPrefs.SetString("Player.keyRecoveryTime", value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetString("Player.keyRecoveryTime");
        }
    }
    public static int PlayerMaxKey
    {
        set
        {
            PlayerPrefs.SetInt("Player.maxkey", value);
            PlayerPrefs.Save();
        }
        get
        {
            return PlayerPrefs.GetInt("Player.maxkey");
        }
    }
    
}
