using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player : MonoBehaviour {

    public struct StageClearInfo
    {
        public int star;
        public bool clear;
    }

    private int gold;//아웃게임 재화
    private int diamond;
    private int lastStage;//플레이어가 최대로 진행한 스테이지
    private int maxKey=12;
    private int key;

    public int difficultyCount = 3;

    List<StageClearInfo[]> stageClearInfoList = new List<StageClearInfo[]>();

    public int Gold
    {
        get { return gold; }
    }

    public int Diamond
    {
        get
        {
            return diamond;
        }
    }

    public int LastStage
    {
        get
        {
            return lastStage;
        }
    }

    public int MaxKey
    {
        get
        {
            return maxKey;
        }
    }

    public int Key
    {
        get
        {
            return key;
        } 
    }

    public void ClearLastStage()
    {
        lastStage++;
        //
    }

    public void Init(int gold, int diamond,int key,string keyRecoveryTime,int maxKey, List<Dictionary<string, object>> stageClearInfo, List<Dictionary<string, object>> towerUpgradeInfo)
    {
        this.gold = gold;
        this.diamond = diamond;
        this.key = key;
        this.maxKey = maxKey;

        for(int i = 0; i < stageClearInfo.Count/3; i++)
        {
            StageClearInfo[] stageClearInfos = new StageClearInfo[difficultyCount];
            for(int j=0;j< difficultyCount; j++)
            {
                stageClearInfos[j].star = (int)stageClearInfo[3 * i + j]["star"];
                stageClearInfos[j].clear = (bool)(stageClearInfo[3 * i + j]["clear"]);
            }
            stageClearInfoList.Add(stageClearInfos);
        }
         

    }
	public int ChangeGold(int value)        
    {
        gold += value;
        return gold;
    }
    public int ChangeDiamond(int value)
    {
        diamond += value;
        return diamond;
    }
    public int ChangeKey(int value)
    {
        key += value;
        return key;
    }
    public int PlusMaxKey(int value)
    {
        maxKey += value;
        return maxKey;
    }

    public StageClearInfo GetStageClearInfo(int num, int difficulty)
    {
        return stageClearInfoList[num - 1][difficulty - 1];
    }
    class TowerTree
    {
        public TowerTree()
        {

        }
    }
    
    //서버 없을 때 재생 방법 고민해봐야 함
    //IEnumerator GenKey()
    //{

    //}
}
