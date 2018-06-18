using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //관리매니저
    static public GameManager stGameManager;
    public EventManger mEventManager;
    public GUIManager mGUIManager;
    public BuildManager mBuildManager;
    public StageManager mStageManager;

    //관리 오브젝트 리스트
    //stage메니저 만들어서 따로관리
    //프리팹 게임오브젝트로 만든것도 몬스터매니저같은거 만들어서 따로관리
    //관리 방법에 대해선 계속해서 생각해보겠음
    //public enum eEnemyObject {NULL=-1,ONE,TWO }
    //public List<int> listEnemyObjectNum = new List<int>(); //임시로 퍼블릭


    //List<GameObject> Objectpool = new List<GameObject>();
    //public int nListFieldidx = 0;
    //필드에있는 타워리스트
    List<Tower> listTower = new List<Tower>();
    public int nListTowerIdx = 0;


    //현재 게임상태
    public enum eGameState { NULL=-1,PLAY,PAUSE,GAMEOVER}

    eGameState mGameState = eGameState.NULL;

    //현재 게임 플레이상태
    public enum ePlayerState {NULL=-1,NOMAL,BUILD,TOWER}

    ePlayerState mPlayerState = ePlayerState.NOMAL;

    Vector3 vecDefaultPos;
    Quaternion quaDefaultPos;

    //사용할 오브젝트 //일부는 stagemanager로 이동
    
    
    public FixedJoystick mStick;
    
    

    //플레이어 정보 나중에 받아서 변경예정
    List<BuildManager.eTowerType> mlistPlayerUseableTower = new List<BuildManager.eTowerType>();

    //테스트용
    //public GameObject preone;

    private void Awake()
    {
        if(stGameManager != null)
        {
            Debug.Log("GameManager is null");
            return;
        }
        stGameManager = this;
    }

    private void Start()
    {
        mGameState = eGameState.PLAY;

        mlistPlayerUseableTower.Add(BuildManager.eTowerType.A);
        mlistPlayerUseableTower.Add(BuildManager.eTowerType.B);
        mlistPlayerUseableTower.Add(BuildManager.eTowerType.C);
        mGUIManager.mGUINomalMode.mGUIBuildMode.GUIPlayerTower = mlistPlayerUseableTower;

        

        mStageManager.nInGameGold = 10200;
        mStageManager.InstansiateStateManager();
        //타워 생성 임시
        for (int i=0;i<8;i++)
        {
            listTower.Add(mBuildManager.MakeTower().GetComponent<Tower>());
            listTower[i].gameObject.SetActive(false);
        }

        //몬스터 생성 임시
        //for (int i=0;i<100;i++)
        //{
        //    GameObject tempobj = Instantiate(preone, mStageManager.trSpawner);
        //    Objectpool.Add(tempobj);
            
        //    tempobj.SetActive(false);
        //}

        //몬스터 스텟 초기화
        //이런식으로 관리하는 오브젝트가 몇개인지 알 수 있다 listEnemyOb~[(int)eEnemyObject.One] 이나 listEnemyOb~[(int)EventManager.eEnemyName.One] 이런식
        //listEnemyObjectNum.Add(nListFieldidx);

        //for (int x = 0; x < 50; x++)
        //{
        //    AddEnemy(EventManger.eEnemyName.ONE);
        //}

        //listEnemyObjectNum.Add(nListFieldidx);

        //for (int x = 0; x < 50; x++)
        //{
        //    AddEnemy(EventManger.eEnemyName.TWO);
        //}

        
        //stage세팅 테스트를 위해서 임시로 이곳에서 실행 나중에 데이터 시트를 통해서 받아오기
        mStageManager.SetList(Enemy.eEnemyType.GOBLIN, 5);
        mStageManager.SetList(Enemy.eEnemyType.BOMBGOBLIN, 1);
        mStageManager.SetList(Enemy.eEnemyType.GOBLIN, 5);
        mStageManager.SetList(Enemy.eEnemyType.HIGHORC, 1);
        mStageManager.nListWaveNum.Add(12);

        mStageManager.SetList(Enemy.eEnemyType.GOBLIN, 5);
        mStageManager.SetList(Enemy.eEnemyType.BOMBGOBLIN, 1);
        mStageManager.SetList(Enemy.eEnemyType.GOBLIN, 5);
        mStageManager.SetList(Enemy.eEnemyType.HIGHORC, 1);
        mStageManager.nListWaveNum.Add(12);

        mStageManager.SetList(Enemy.eEnemyType.GOBLIN, 5);
        mStageManager.SetList(Enemy.eEnemyType.BOMBGOBLIN, 1);
        mStageManager.SetList(Enemy.eEnemyType.GOBLIN, 5);
        mStageManager.SetList(Enemy.eEnemyType.HIGHORC, 1);
        mStageManager.nListWaveNum.Add(12);

        //mStageManager.SetList(Enemy.eEnemyType.SLIME, 15);
        //mStageManager.nListWaveNum.Add(15);
        //mStageManager.SetList(Enemy.eEnemyType.ORC, 20);
        //mStageManager.nListWaveNum.Add(20);
        //mStageManager.SetList(Enemy.eEnemyType.ORC, 10);
        //mStageManager.SetList(Enemy.eEnemyType.HIGHORC, 1);
        //mStageManager.SetList(Enemy.eEnemyType.ORC, 10);
        //mStageManager.nListWaveNum.Add(21);

        mStageManager.nListWaveNum.Add(-1);

        mGUIManager.mGUINomalMode.GUINomalInit();
        mGUIManager.mGUIAlways.GUIAlwaysOnInit(mStageManager.GetFieldEnemysList().Count);

        StartCoroutine(mStageManager.StageProgress(100, 0.5f));



        //StartCoroutine("SpawnCorutine");
        StartCoroutine("InGameCorutine");

        vecDefaultPos = Camera.main.transform.position;
        quaDefaultPos = Camera.main.transform.rotation;
        mGUIManager.mGUIControlMode.FuncctionSet();
    }

    //private void Update()
    //{
        
    //}

    //private void OnGUI()
    //{
    //    //GUI.Box(new Rect(0, 0, 200, 40), "PlayerState:" + mPlayerState);
    //    //for (int i = 0; i < listFieldEnemy.Count; i++)
    //    //{
    //    //    GUI.Box(new Rect(0, 40 * (i + 1), 200, 40), "" + listFieldEnemy[i].gameObject.name + "/" + listFieldEnemy[i].GetComponent<Enemy>().mStat.mEnemyName + "/" + listFieldEnemy[i].GetComponent<Enemy>().mStat.hp);
    //    //}
    //}

    //각 리스트 , 상태 접근 함수
    public void SetPlayerState(ePlayerState _PlayerState)
    {
        mPlayerState = _PlayerState;
    }

    public ePlayerState GetPlayerState()
    {
        return mPlayerState;
    }

    public void SetGameState(eGameState _GameState)
    {
        switch (_GameState)
        {
            case eGameState.NULL:
                break;
            case eGameState.PLAY:
                Time.timeScale = 1f;
                break;
            case eGameState.PAUSE:
                Time.timeScale = 0f;
                break;
            case eGameState.GAMEOVER:
                break;
            default:
                break;
        }
        mGameState = _GameState;
    }

    public eGameState GetGameState()
    {
        return mGameState;
    }

    //public List<GameObject> GetEnemyList()
    //{
    //    return Objectpool;
    //}

   



    public List<BuildManager.eTowerType> GetPlayerUseableTowerList()
    {
        return mlistPlayerUseableTower;
    }

    public List<Tower> GetFieldTowerList()
    {
        return listTower;
    }

    public void AddFieldTowerList(Tower _tower)
    {
        listTower.Add(_tower);
    }




    //카메라 원 위치로 이동
    public void CameraReset()
    {
        Camera.main.transform.position = vecDefaultPos;
        Camera.main.transform.rotation = quaDefaultPos;
    }

    
   

    IEnumerator InGameCorutine()
    {
        //float curnum = 0;
        while (GameManager.stGameManager.GetGameState() == GameManager.eGameState.PLAY)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mEventManager.Event(EventManger.eEventName.TOWERPICK);
            }

            for (int i = 0; i < listTower.Count; i++)
            {
                listTower[i].DefaultTowerAct();
            }

            for (int i = 0; i < mStageManager.GetFieldEnemysList().Count; i++)
            {
                if (mStageManager.GetFieldEnemysobjList()[i].activeSelf)
                {
                    mStageManager.GetFieldEnemysList()[i].ArriveGoal();
                }
            }

            //float persentnum = ((float)GameManager.stGameManager.nListFieldidx / (float)GameManager.stGameManager.GetEnemyList().Count);



            //if (curnum <= persentnum)
            //    curnum += Time.fixedDeltaTime / (float)GameManager.stGameManager.GetEnemyList().Count;

            //mGUIManager.mGUINomalMode.WaveBar.fillAmount = curnum;

            yield return new WaitForFixedUpdate();
        }
    }

}
