using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public enum eDebuffName {NULL=-1,SLOW }
    public enum eEnemyType {NULL=-1,GOBLIN}

    public eEnemyType mEnemyType;
    public Transform goal;
    public NavMeshAgent nvAgent;
    public int MonsterIdx;
    bool[] mDebuff = new bool[1];



    public struct strStat
    {
        public EventManger.eEnemyName mEnemyName;
        public int hp;
        public int def;
        strStat(int _hp)
        {
            hp = 30;
            def = 5;
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

    public bool DeBuffSet(eDebuffName _debuff, float _val)
    {
        switch (_debuff)
        {
            case eDebuffName.NULL:
                return false;
            case eDebuffName.SLOW:
                if (mDebuff[(int)eDebuffName.SLOW] == false)
                {
                    mDebuff[(int)eDebuffName.SLOW] = true;
                    StartCoroutine(SlowDebuff(_val));
                }

                break;
            default:
                break;
        }
        return true;
    }

    public virtual bool CheckDead()
    {
        if (mStat.hp <= 0)
        {
            return true;
        }
        else
            return false;
    }

    IEnumerator SlowDebuff(float _val)
    {
        nvAgent.speed -= _val;
        yield return new WaitForSeconds(3f);
        mDebuff[(int)eDebuffName.SLOW] = false;
        nvAgent.speed += _val;
    }
}
