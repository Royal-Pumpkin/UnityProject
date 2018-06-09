using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    private int gold;//아웃게임 재화
    private int diamond;
    private int lastStage;//플레이어가 최대로 진행한 스테이지
    private int maxKey;
    private int key;

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

    public void Init(int gold, int diamond,int key,int maxKey,int lastStage)
    {
        this.gold = gold;
        this.diamond = diamond;
        this.key = key;
        this.maxKey = maxKey;
        this.lastStage = lastStage;

        
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

    //서버 없을 때 재생 방법 고민해봐야 함
    //IEnumerator GenKey()
    //{

    //}
}
