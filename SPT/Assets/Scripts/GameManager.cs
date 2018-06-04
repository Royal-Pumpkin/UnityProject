using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //관리매니저
    static public GameManager stGameManager;
    public EventManger mEventManager;
    public GUIManager mGUIManager;
    public Player player;
    //관리 오브젝트 리스트
    List<GameObject> listFieldEnemy = new List<GameObject>();
    public int nListFieldidx = 0;
    List<Tower> listTower = new List<Tower>();

    //현재 게임상태
    public enum eGameState { NULL=-1,PLAY,PAUSE,GAMEOVER}

    eGameState mGameState = eGameState.NULL;

    //현재 게임 플레이상태
    public enum ePlayerState {NULL=-1,NOMAL,TOWER}

    ePlayerState mPlayerState = ePlayerState.NOMAL;

    Vector3 vecDefaultPos;
    Quaternion quaDefaultPos;

    //사용할 오브젝트
    public Tower mControlTower;
    public Transform mGoal;
    public FixedJoystick mStick;
    public Transform trSpawner;

    //테스트용
    public GameObject preone;

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

        for (int i=0;i<100;i++)
        {
            GameObject tempobj = Instantiate(preone, trSpawner);
            listFieldEnemy.Add(tempobj);
            
            tempobj.SetActive(false);
        }


        StartCoroutine("SpawnCorutine");
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

    public void SetPlayerState(ePlayerState _PlayerState)
    {
        mPlayerState = _PlayerState;
    }

    public ePlayerState GetPlayerState()
    {
        return mPlayerState;
    }

    

    

    public void CameraReset()
    {
        Camera.main.transform.position = vecDefaultPos;
        Camera.main.transform.rotation = quaDefaultPos;
    }

    public List<GameObject> GetEnemyList()
    {
        return listFieldEnemy;
    }

    public void AddEnemy(EventManger.eEnemyName _EnemyName)
    {
        mEventManager.EnemyCreate(_EnemyName, listFieldEnemy);
    }

    public List<Tower> GetTowerList()
    {
        return listTower;
    }

    public void AddTowerList(Tower _tower)
    {
        listTower.Add(_tower);
    }

    IEnumerator SpawnCorutine()
    {
        while (mGameState == eGameState.PLAY && nListFieldidx <99)
        {
            int i = (int)Random.Range(1f, 3f);
            if (i == 1)
                AddEnemy(EventManger.eEnemyName.ONE);
            else if (i == 2)
                AddEnemy(EventManger.eEnemyName.TWO);

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator InGameCorutine()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mEventManager.Event(EventManger.eEventName.TOWERPICK);
            }

            for (int i = 0; i < listTower.Count; i++)
            {
                listTower[i].DefaultTowerAct();
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void ChangeGold(int value)
    {
        int gold = player.ChangeGold(value);

    }

}
