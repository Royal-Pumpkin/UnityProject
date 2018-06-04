using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private int gold;
    public int Gold
    {
        get { return gold; }
    }
    public void Init(int gold)
    {
        this.gold = gold;
    }  

	public int ChangeGold(int value)        
    {
        gold += value;
        return gold;
    }
}
