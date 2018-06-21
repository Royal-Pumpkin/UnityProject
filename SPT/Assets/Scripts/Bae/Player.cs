using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class Player : MonoBehaviour {
    [Serializable]
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
    [SerializeField] private string keyRecoveryTime = "full";
    private int key;

    public int difficultyCount = 3;

    [SerializeField] List<StageClearInfo[]> stageClearInfo = new List<StageClearInfo[]>();
    [SerializeField] List<TowerTreeNode[]> towerTree = new List<TowerTreeNode[]>();

    bool checkGenKeyCoroutine = false;
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
        this.keyRecoveryTime = keyRecoveryTime;
        this.stageClearInfo = stageClearInfo;
        this.towerTree = towerTree;

        if (keyRecoveryTime.CompareTo("full") != 0)
        {
            if (!TimeUtil.ValidTime(keyRecoveryTime))
            {
                if (key == maxKey)
                {
                    this.keyRecoveryTime = "full";
                    PlayerPrefsUtil.PlayerKeyRecovryTime = this.keyRecoveryTime;
                }
                else
                {
                    this.keyRecoveryTime = TimeUtil.GetTime();
                    PlayerPrefsUtil.PlayerKeyRecovryTime = this.keyRecoveryTime;
                    GenKey();
                }                
            }
            else
            {
                TimeSpan timeDiff = DateTime.Now - DateTime.ParseExact(keyRecoveryTime, TimeUtil.pattern, null);
                key+=((int)timeDiff.TotalHours);
                if (key >= maxKey)
                {
                    key = maxKey;
                    this.keyRecoveryTime = "full";
                    PlayerPrefsUtil.PlayerKey = key;
                    PlayerPrefsUtil.PlayerKeyRecovryTime = this.keyRecoveryTime;
                }
                else
                {
                    DateTime time = DateTime.ParseExact(this.keyRecoveryTime, TimeUtil.pattern, null).AddHours((int)timeDiff.TotalHours);
                    this.keyRecoveryTime = time.ToString(TimeUtil.pattern);
                    PlayerPrefsUtil.PlayerKeyRecovryTime = this.keyRecoveryTime;
                    GenKey();
                }
            }
        }
    }
    public void Init(List<StageClearInfo[]> stageClearInfo, List<TowerTreeNode[]> towerTree)
    {
        key = MaxKey;
        Debug.Log(stageClearInfo.Count);
        Debug.Log(towerTree.Count);
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
        if (key < MaxKey)
        {
            if (keyRecoveryTime.CompareTo("full")==0)
            {
                keyRecoveryTime = TimeUtil.GetTime();
                PlayerPrefsUtil.PlayerKeyRecovryTime = keyRecoveryTime;
                GenKey();
            }
            else
            {
                if (!checkGenKeyCoroutine)
                {
                    keyRecoveryTime = TimeUtil.GetTime();
                    GenKey();
                }
            }
        }
        else
        {
            keyRecoveryTime = "full";
        }
        PlayerPrefsUtil.PlayerKeyRecovryTime = keyRecoveryTime;
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
    public List<TowerManager.TOWERID> availableTowerList;
    void SetTowerList()
    {
        for(int i = 0; i < towerTree.Count; i++)
        {
            for(int j = 0; j < towerTree[i].Length; j++)
            {
                if (towerTree[i][j].usable == 0)
                {
                    if (towerTree[i][j].getTowerId == 0)
                    {
                        //타워 강화
                    }
                    else
                    {
                        //availableTowerList.Add((TowerManager.TOWERID)("T"+towerTree[i][j].getTowerId));
                    }
                }
            }
        }
    }
    [Serializable]
    public struct TowerTreeNode
    {
        public int num;
        public string treeName;
        public int[] needNum;
        public int[] nextNum;
        public int usable;
        public int getTowerId;
        public string comment;

        public TowerTreeNode(int num,string treeName, int[] needNum, int[] nextNum, int usable, int getTowerId, string comment)
        {
            this.num = num;
            this.treeName = treeName;
            this.needNum = needNum;
            this.nextNum = nextNum;
            this.usable = usable;
            this.getTowerId = getTowerId;
            this.comment = comment;
        }

        public override string ToString()
        {
            return "treeName "+treeName;
        }
    }

    public void ClearStage(int stage, int difficulty, int star)
    {
        stageClearInfo[stage - 1][difficulty - 1].star = star;
        stageClearInfo[stage - 1][difficulty - 1].clear = true;
        PlayerPrefsUtil.SaveStageClearInfoData(stage, difficulty, star);
    }

    public int TowerTreeCount { get { return towerTree.Count; } }
    public int NodesNumberPerTree(int treeNumber){return towerTree[treeNumber].Length; }

    public void BuyTowerNode(int treeNum,int nodeNum)
    {
        Debug.Log("구매 번호 "+ nodeNum);
        PlayerPrefsUtil.SaveTowerTreeNodeUsable(treeNum, nodeNum, 0);
        MainManager.Instance.GUIUpgradeStateChange(treeNum, nodeNum, 0);
        MainManager.Instance.mainGUI.notice.UpdateNoticeUpgrade(0);
        int trn = treeNum - 1;
        int ndn = nodeNum - 1;
        towerTree[trn][ndn].usable = 0;
        int[] nextNums = towerTree[trn][ndn].nextNum;

        for(int i = 0; i < nextNums.Length; i++)
        {
            int nextNum = nextNums[i] - 1;
            if (nextNum == -1)
            {
                break;
            }
            int[] needNums = towerTree[trn][nextNum].needNum;
            bool checkUsable = true;
            for(int j = 0; j < needNums.Length; j++)
            {
                if (towerTree[trn][needNums[j] - 1].usable != 0)
                {
                    checkUsable=false;
                }
            }
            if (checkUsable)
            {
                towerTree[trn][nextNum].usable = 1;
                PlayerPrefsUtil.SaveTowerTreeNodeUsable(treeNum, nextNums[i], 1);
                MainManager.Instance.GUIUpgradeStateChange(treeNum, nextNums[i], 1);
            }
        }
    }

    void GenKey()
    {
        Debug.Log("재생 시작");
        StartCoroutine(CheckTime());
    }
    
    IEnumerator CheckTime()
    {
        checkGenKeyCoroutine = true;
        DateTime date = DateTime.ParseExact(keyRecoveryTime, TimeUtil.pattern, null);
        date=date.AddHours(1);
        //date = date.AddSeconds(30);
        while (DateTime.Now.CompareTo(date)<0)
        {
            //TimeSpan span = date-DateTime.Now ;
            //checkTime = (int)span.TotalSeconds;
            yield return new WaitForSecondsRealtime(1f);
        }
        checkGenKeyCoroutine = false;
        MainManager.Instance.ChangeKey(1);        
    }
    //int checkTime;
    //private void OnGUI()
    //{
    //    if (keyRecoveryTime.CompareTo("full") != 0)
    //    {
    //        DateTime date = DateTime.ParseExact(keyRecoveryTime, TimeUtil.pattern, null);
    //        date = date.AddHours(1);
    //        GUI.Box(new Rect(0, 0, 200, 50), keyRecoveryTime + "\n" + date.ToString(TimeUtil.pattern) + "\n" + checkTime.ToString());
    //    }
        
    //}
}
