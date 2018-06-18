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

    [Header("초기값")]
    [SerializeField] private int gold;//아웃게임 재화
    [SerializeField] private int gem;
    [SerializeField] private int lastStage;//플레이어가 최대로 진행한 스테이지
    [SerializeField] private int maxKey=12;
    private int key;

    public int difficultyCount = 3;

    List<StageClearInfo[]> stageClearInfo = new List<StageClearInfo[]>();
    List<TowerTreeNode[]> towerTree = new List<TowerTreeNode[]>();
    public int Gold
    {
        get { return gold; }
    }

    public int Gem
    {
        get
        {
            return gem;
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

    //sqlite 쓸때
    public void Init(int gold, int gem,int key,string keyRecoveryTime,int maxKey, List<Dictionary<string, object>> stageClearInfo, List<Dictionary<string, object>> towerUpgradeInfo)
    {
        this.gold = gold;
        this.gem = gem;
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
            this.stageClearInfo.Add(stageClearInfos);
        }
    }

    public void Init(int gold, int gem, int key, string keyRecoveryTime, int maxKey, List<StageClearInfo[]> stageClearInfo, List<TowerTreeNode[]> towerTree)
    {
        this.gold = gold;
        this.gem = gem;
        this.key = key;
        this.maxKey = maxKey;

        this.stageClearInfo = stageClearInfo;
        this.towerTree = towerTree;
    }
    public void Init(List<StageClearInfo[]> stageClearInfo, List<TowerTreeNode[]> towerTree)
    {
        key = MaxKey;
        this.stageClearInfo = stageClearInfo;
        this.towerTree = towerTree;
    }
    public int ChangeGold(int value)        
    {
        gold += value;
        return gold;
    }
    public int ChangeGem(int value)
    {
        gem += value;
        return gem;
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
        return stageClearInfo[num - 1][difficulty - 1];
    }

    public TowerTreeNode GetTowerTreeNode(int tree, int node)
    {
        return towerTree[tree - 1][node - 1];
    }

    public struct TowerTreeNode
    {
        public string treeName;
        public int[] needNum;
        public int[] nextNum;
        public int usable;
        public int getTowerId;

        public TowerTreeNode(string treeName, int[] needNum, int[] nextNum, int usable, int getTowerId)
        {
            this.treeName = treeName;
            this.needNum = needNum;
            this.nextNum = nextNum;
            this.usable = usable;
            this.getTowerId = getTowerId;
        }
    }

    public void ClearStage(int stage, int difficulty, int star)
    {
        stageClearInfo[stage - 1][difficulty - 1].star = star;
        stageClearInfo[stage - 1][difficulty - 1].clear = true;
        PlayerPrefsUtil.SaveStageClearInfoData(stage, difficulty, star);
    }

    //서버 없을 때 재생 방법 고민해봐야 함
    //IEnumerator GenKey()
    //{

    //}
}
