using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public enum eDebuffName {NULL=-1,SLOW }
    public enum eEnemyType {NULL=-1,GOBLIN,BOMBGOBLIN,SLIME,ORC,HIGHORC}

    Renderer mEnemyRenderer;
    Color mDefaultColor;

    public eEnemyType mEnemyType;
    
    public Transform goal;
    public NavMeshAgent nvAgent;
    public int MonsterIdx;
    public int gear;
    int score;
    bool[] mDebuff = new bool[1];
    public bool bSerchstate;



    public struct strStat
    {
        public eEnemyType mEnemyName;
        public int hp;
        public int def;
        strStat(int _hp)
        {
            hp = 30;
            def = 5;
            mEnemyName = eEnemyType.NULL;
        }
    }
    public strStat mStat;

    private void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        nvAgent.destination = goal.position;
    }

    public void initEnemy()
    {
        mEnemyRenderer = this.GetComponent<Renderer>();
        mDefaultColor = mEnemyRenderer.material.color;
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
            GameManager.stGameManager.mStageManager.nInGameGold += gear;
            return true;
        }
        else
            return false;
    }

    public void SetColor(bool _triiger)
    {
        if (mEnemyRenderer != null)
        {

            if (_triiger)
                mEnemyRenderer.material.color = Color.red;
            else
                mEnemyRenderer.material.color = mDefaultColor;
        }
    }

    public void ArriveGoal()
    {
        if(nvAgent ==null)
        {
            return;
        }

        Vector3 vecposions = transform.position - goal.position;
        float distancetogoal = vecposions.magnitude;
        float framemove = nvAgent.speed * Time.deltaTime;

        if (distancetogoal <= 2)
        {
            gameObject.SetActive(false);
            //life감소
            if (GameManager.stGameManager.mStageManager.lifeDown(1))
                GameManager.stGameManager.mStageManager.GameOver();
        }
    }

    IEnumerator SlowDebuff(float _val)
    {
        nvAgent.speed -= _val;
        yield return new WaitForSeconds(3f);
        mDebuff[(int)eDebuffName.SLOW] = false;
        nvAgent.speed += _val;
    }
}
