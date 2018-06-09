using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    int nWave;
    int nFieldIdx = 0;
    float fTime;

    public enum eStageState {NULL=-1,START,WAVE,REST,FINISH,CLEAR,GAMEOVER}

    public eStageState mStagestate;
    List<GameObject> mListFieldEnemys = new List<GameObject>();
    public List<GameObject> mListTowers = new List<GameObject>(); //임시 퍼블릭
    public List<int> nListWaveNum = new List<int>();

    int nPlayerLife = 5;
    int nInGameGold;

    //관리 오브젝트들 맵이 로딩될 때 할당해준다.
    public Transform mGoal;
    public Transform trSpawner;


    //정리된 웨이브 테이블을 받아와서 mListFieldEnemy에 추가한다. add remove를 사용할지는 고민
    //노드(비콘)들도 마찬가지로 리스트로 관리할지 생각중
    //
    public void SetList(EventManger.eEnemyName _monstername,int _nummonster)
    {
        switch (_monstername)
        {
            case EventManger.eEnemyName.NULL:
                return;
            case EventManger.eEnemyName.ONE:
                for(int i=0;i<_nummonster;i++)
                {
                    mListFieldEnemys.Add(GameManager.stGameManager.GetEnemyList()[i + GameManager.stGameManager.listEnemyObjectNum[(int)GameManager.eEnemyObject.ONE]]);
                }
                break;
            case EventManger.eEnemyName.TWO:
                for (int i = 0; i < _nummonster; i++)
                {
                    mListFieldEnemys.Add(GameManager.stGameManager.GetEnemyList()[i + GameManager.stGameManager.listEnemyObjectNum[(int)GameManager.eEnemyObject.TWO]]);
                }
                break;
            default:
                return;
        }
    }

    public bool lifeDown(int _val)
    {
        nPlayerLife -= _val;

        if (nPlayerLife <= 0)
            return true;
        else
            return false;
    }

    public void GameOver()
    {
        mStagestate = eStageState.GAMEOVER;
    }

    void ClearStage()
    {
        mStagestate = eStageState.CLEAR;
    }


    IEnumerator StateProgress()
    {
        while(GameManager.stGameManager.GetGameState() == GameManager.eGameState.PLAY)
        {
            //WaveProgress();
            yield return new WaitForSeconds(0.5f);
        }
    }

    //웨이브랑 현재 리스트 갯수랑 어떤식으로할지 고민중
    public IEnumerator StageProgress(int _numwave,float _waittime)
    {
        int defaultnum = nFieldIdx;
        
        while (mStagestate != eStageState.CLEAR/*클리어를 하고 스위치문안에서 함수를 부르고 while문을 나가는 법을 생각해본다*/)
        {
            if (GameManager.stGameManager.GetGameState() != GameManager.eGameState.PLAY)
                yield return new WaitForSeconds(1f);


            switch (mStagestate)
            {
                case eStageState.NULL:
                    break;
                case eStageState.START:

                    break;
                case eStageState.WAVE:
                    Debug.Log("WAVEING");


                    if (mListFieldEnemys.Count == 0)
                        break;
                    mListFieldEnemys[nFieldIdx].SetActive(true);
                    Enemy tempEnemy = mListFieldEnemys[nFieldIdx].GetComponent<Enemy>();
                    tempEnemy.goal = mGoal;
                    ++nFieldIdx;

                    if(nFieldIdx == (defaultnum + nListWaveNum[nWave]))
                    {
                        ++nWave;
                        if(nListWaveNum[nWave] == -1)
                        {
                            mStagestate = eStageState.FINISH;
                            break;
                        }
                        mStagestate = eStageState.REST;
                    }

                    for(int i=0;i<mListFieldEnemys.Count;i++)
                    {
                        if (mListFieldEnemys[i].activeSelf)
                        {
                            tempEnemy = mListFieldEnemys[i].GetComponent<Enemy>();
                            tempEnemy.ArriveGoal();
                        }
                    }

                    yield return new WaitForSeconds(_waittime);
                    break;
                case eStageState.REST:
                    defaultnum = nFieldIdx;
                    yield return new WaitForSeconds(3f);
                    mStagestate = eStageState.WAVE;
                    break;
                case eStageState.FINISH:
                    int Checknum = 0;

                    for(int i=0;i<mListFieldEnemys.Count;i++)
                    {
                        if (mListFieldEnemys[i].activeSelf)
                            ++Checknum;
                    }

                    if (Checknum == 0)
                    {
                        mStagestate = eStageState.CLEAR;
                        break;
                    }

                    for (int i = 0; i < mListFieldEnemys.Count; i++)
                    {
                        if (mListFieldEnemys[i].activeSelf)
                        {
                            tempEnemy = mListFieldEnemys[i].GetComponent<Enemy>();
                            tempEnemy.ArriveGoal();
                        }
                    }


                    break;
                case eStageState.CLEAR:

                    break;
                case eStageState.GAMEOVER:

                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    
}

