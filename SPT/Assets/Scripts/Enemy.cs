using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Transform goal;
    public NavMeshAgent nvAgent;
    public int MonsterIdx;

    public struct strStat
    {
        public EventManger.eEnemyName mEnemyName;
        public int hp;
        strStat(int _hp)
        {
            hp = 30;
            mEnemyName = EventManger.eEnemyName.NULL;
        }
    }
    public strStat mStat;

    private void Start()
    {
        goal = GameManager.stGameManager.mGoal;
        nvAgent = GetComponent<NavMeshAgent>();
        nvAgent.destination = goal.position;
        mStat.hp = 30;
    }



    public bool CheckDead()
    {
        if (mStat.hp <= 0)
        {
            return true;
        }
        else
            return false;
    }
}
