using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    int nWave;
    int nFieldIdx = 0;

    public Camera IntowerCamera;
    public GameObject IntowerDesk;

    public enum eStageState {NULL=-1,START,WAVE,REST,FINISH,CLEAR,GAMEOVER}

    public eStageState mStagestate;
    List<GameObject> mListFieldEnemysobj = new List<GameObject>();
    List<Enemy> mListFieldEnemys = new List<Enemy>();
    public List<GameObject> mListTowers = new List<GameObject>(); //임시 퍼블릭
    public List<int> nListWaveNum = new List<int>();

    public int nPlayerLife = 105;
    public int nInGameGold;

    public int nListFieldidx;
    //관리 오브젝트들 맵이 로딩될 때 할당해준다.
    public Transform mGoal;
    public Transform trSpawner;

    //애니메이션용 각각의 타워마다 필요하지는않음
    float HandleAngleX;
    float DefaultAngleX;
    float HandleAngleY;
    float HandleAngleZ;

    //조종간
    public Transform RotateLeft;
    public Transform RotateRight;

    //현재 조정중의 타워
    public Tower mControlTower;

    //트리거
    bool bStartTrriger = true;
    public bool bDelay = false;

    //시간
    public float fStartTime =0;
    public float fGameTime =0;
    public float fWaitTime = 0;
    public float fRestTime = 10f;

    //정리된 웨이브 테이블을 받아와서 mListFieldEnemy에 추가한다. add remove를 사용할지는 고민
    //노드(비콘)들도 마찬가지로 리스트로 관리할지 생각중
    //
    public List<GameObject> GetFieldEnemysobjList()
    {
        return mListFieldEnemysobj;
    }

    public List<Enemy> GetFieldEnemysList()
    {
        return mListFieldEnemys;
    }

    void SetStartTrriger(bool _trriger)
    {
        bStartTrriger = _trriger;
    }

    public void TowerDeskOn(bool _trriger)
    {
        IntowerDesk.SetActive(_trriger);
        HandleAngleX = RotateLeft.localRotation.x;
        HandleAngleY = RotateLeft.localRotation.y;
        HandleAngleZ = RotateLeft.localRotation.z;
    }

    public void InstansiateStateManager()
    {
        DefaultAngleX = RotateLeft.localRotation.x;
    }

    public void SetList(Enemy.eEnemyType _monstername,int _nummonster)
    {
        switch (_monstername)
        {
            case Enemy.eEnemyType.NULL:
                return;
            case Enemy.eEnemyType.GOBLIN:
                for(int i=0;i<_nummonster;i++)
                {
                    GameManager.stGameManager.mEventManager.EnemyCreate(Enemy.eEnemyType.GOBLIN, mListFieldEnemysobj,mListFieldEnemys);
                }
                break;
            case Enemy.eEnemyType.BOMBGOBLIN:
                for (int i = 0; i < _nummonster; i++)
                {
                    GameManager.stGameManager.mEventManager.EnemyCreate(Enemy.eEnemyType.BOMBGOBLIN, mListFieldEnemysobj, mListFieldEnemys);
                }
                break;
            case Enemy.eEnemyType.SLIME:
                for (int i = 0; i < _nummonster; i++)
                {
                    GameManager.stGameManager.mEventManager.EnemyCreate(Enemy.eEnemyType.SLIME, mListFieldEnemysobj, mListFieldEnemys);
                }
                break;
            case Enemy.eEnemyType.ORC:
                for (int i = 0; i < _nummonster; i++)
                {
                    GameManager.stGameManager.mEventManager.EnemyCreate(Enemy.eEnemyType.ORC, mListFieldEnemysobj, mListFieldEnemys);
                }
                break;
            case Enemy.eEnemyType.HIGHORC:
                for (int i = 0; i < _nummonster; i++)
                {
                    GameManager.stGameManager.mEventManager.EnemyCreate(Enemy.eEnemyType.HIGHORC, mListFieldEnemysobj, mListFieldEnemys);
                }
                break;
            default:
                return;
        }
    }

    public IEnumerator HandAnimation()
    {
        float fLerp = 0.0f ;
        
        while (mControlTower != null)
        {

            FixedJoystick tempstick = GameManager.stGameManager.mStick;

            HandleAngleX += tempstick.Horizontal * 5f;
            if(HandleAngleX >= 20)
            {
                HandleAngleX = 20;
            }
            else if(HandleAngleX <=-20)
            {
                HandleAngleX = -20;
            }

            if (tempstick.Horizontal == 0 && tempstick.Vertical == 0)
            {
                fLerp += Time.deltaTime / 5f;
                HandleAngleX = Mathf.Lerp(HandleAngleX, DefaultAngleX, fLerp);
            }
            else
            {
                fLerp = 0;
            }
            

            Quaternion tempqut = Quaternion.Euler(HandleAngleX, HandleAngleY, HandleAngleZ);
            Quaternion reversequt = Quaternion.Euler(-HandleAngleX, HandleAngleY, HandleAngleZ);

            RotateLeft.localRotation = tempqut;
            RotateRight.localRotation = reversequt;

            yield return new WaitForFixedUpdate();
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
        GameManager.stGameManager.mGUIManager.SetGUIScene(GUIManager.eGUISceneName.GAMEOVER);
    }

    void ClearStage()
    {
        mStagestate = eStageState.CLEAR;
        GameManager.stGameManager.mGUIManager.SetGUIScene(GUIManager.eGUISceneName.CLEAR);
    }

    bool WaitStart()
    {
        if (bStartTrriger)
        {
            fStartTime = Time.deltaTime;
            fGameTime = fStartTime;
            return true;
        }
        else
            return false;
    }


    IEnumerator Timer()
    {
        while (true)
        {
            if (GameManager.stGameManager.GetGameState() == GameManager.eGameState.PLAY && mStagestate == eStageState.WAVE)
            {
                fGameTime += Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator RestTimer(float _temptime)
    {
        while(GameManager.stGameManager.GetGameState() == GameManager.eGameState.PLAY && mStagestate == eStageState.REST)
        {
            _temptime -= Time.fixedDeltaTime;
            GameManager.stGameManager.mGUIManager.SetInfoText(_temptime);
            yield return new WaitForFixedUpdate();

        }
    }

    //웨이브랑 현재 리스트 갯수랑 어떤식으로할지 고민중
    public IEnumerator StageProgress(int _numwave,float _waittime)
    {
        int defaultnum = nFieldIdx;
        fWaitTime = _waittime * nListWaveNum[nWave];

        while (mStagestate != eStageState.CLEAR/*클리어를 하고 스위치문안에서 함수를 부르고 while문을 나가는 법을 생각해본다*/ && mStagestate != eStageState.GAMEOVER)
        {
            if (GameManager.stGameManager.GetGameState() != GameManager.eGameState.PLAY)
                yield return new WaitForSeconds(1f);

            if (bDelay)
                yield return null;
           
            switch (mStagestate)
            {
                case eStageState.NULL:
                    break;
                case eStageState.START:
                    if(WaitStart())
                    {
                        StartCoroutine(Timer());
                        mStagestate = eStageState.WAVE;
                    }
                    break;
                case eStageState.WAVE:
                    //Debug.Log("WAVEING");
                    

                    if (mListFieldEnemys.Count == 0)
                        break;
                    mListFieldEnemysobj[nFieldIdx].SetActive(true);
                    mListFieldEnemys[nFieldIdx].goal = mGoal;
                    ++nFieldIdx;

                  

                    if (nFieldIdx == (defaultnum + nListWaveNum[nWave]))
                    {
                        ++nWave;
                        if(nListWaveNum[nWave] == -1)
                        {
                            mStagestate = eStageState.FINISH;
                            break;
                        }
                        IEnumerator tempcorutine = DelayTime(fRestTime);
                        StartCoroutine(tempcorutine);
                        GameManager.stGameManager.mGUIManager.SetBtninfo(tempcorutine, mStagestate);
                        defaultnum = nFieldIdx;
                        yield return null;
                    }

                    yield return new WaitForSeconds(_waittime);
                    break;
                case eStageState.REST:
                    

                    
                    break;
                case eStageState.FINISH:

                    int Checknum = 0;

                    for(int i=0;i<mListFieldEnemys.Count;i++)
                    {
                        if (mListFieldEnemysobj[i].activeSelf)
                            ++Checknum;
                    }

                    if (Checknum == 0)
                    {
                        ClearStage();
                        break;
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

    public IEnumerator DelayTime(float _delay)
    {
        bDelay = true;
        mStagestate = eStageState.REST;
        StartCoroutine(RestTimer(_delay));
        yield return new WaitForSeconds(_delay);
        //stopcorutine을해도 이 뒤에 것이 위에 시간뒤에 불린다
        if (bDelay == true)
        {
            mStagestate = eStageState.WAVE;
            bDelay = false;
        }
    }

    

}

