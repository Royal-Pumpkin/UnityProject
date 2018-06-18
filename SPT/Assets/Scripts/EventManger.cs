using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManger : MonoBehaviour {

	public enum eEventName{NULL=-1,TOWERPICK,TOWEROUT,ENEMYSPWAN}
    


    //임시로 등록 나중에 resource불러오는걸로 변경?
    public GameObject preGoblin;
    public GameObject preBombgoblin;
    public GameObject preSlime;
    public GameObject preOrc;
    public GameObject preHighOrc;

    

    public bool Event(eEventName _Event)
    {
        switch (_Event)
        {
            case eEventName.NULL:
                return false;
            case eEventName.TOWERPICK:
                TowerPickEvent();
                break;
            case eEventName.TOWEROUT:
                TowerOutUIPick();
                break;
            default:
                return false;
        }
        return true;
    }

    bool TowerPickEvent()
    {
        if (GameManager.stGameManager.GetPlayerState() != GameManager.ePlayerState.NOMAL)
            return false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        RaycastHit hitobj;
        Physics.Raycast(ray, out hitobj);
        //Physics.BoxCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(3, 3, 3), Camera.main.transform.forward, out hitobj, Camera.main.transform.rotation, Mathf.Infinity);
        //Debug.Log("" + hitobj.transform.name);
        //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward*10f, Color.red, 5f);
        //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
        
        if (hitobj.transform == null)
            return false;

        Tower PickedTower = hitobj.transform.GetComponent<Tower>();
        
        if (hitobj.transform.tag == "Tower")
        {
            GameManager.stGameManager.mGUIManager.mGUINomalMode.ModeSelectOn(PickedTower, Input.mousePosition);
            return true;
        }

        return false;
    }

    void TowerOutUIPick()
    {
        if (GameManager.stGameManager.mStageManager.mControlTower)
        {
            GameManager.stGameManager.mStageManager.mControlTower.TowerGetout(Camera.main);
            GameManager.stGameManager.mGUIManager.SetGUIScene(GUIManager.eGUISceneName.NOMALSCENE);
        }
        else
            return;
    }

    Enemy EnemyStatSet(Enemy.eEnemyType _EnemyName,Enemy _CreateEnemy)
    {
        switch (_EnemyName)
        {
            case Enemy.eEnemyType.NULL:
                break;
            case Enemy.eEnemyType.GOBLIN:
                _CreateEnemy.mStat.mEnemyName = Enemy.eEnemyType.GOBLIN;
                _CreateEnemy.transform.name = "Goblin";
                _CreateEnemy.mStat.hp = 50;
                _CreateEnemy.nvAgent.speed = 10;
                _CreateEnemy.gear = 10;
                break;
            case Enemy.eEnemyType.BOMBGOBLIN:
                _CreateEnemy.mStat.mEnemyName = Enemy.eEnemyType.BOMBGOBLIN;
                _CreateEnemy.transform.name = "BombGoblin";
                _CreateEnemy.mStat.hp = 50;
                _CreateEnemy.nvAgent.speed = 10;
                _CreateEnemy.gear = 10;
                break;
            case Enemy.eEnemyType.SLIME:
                _CreateEnemy.mStat.mEnemyName = Enemy.eEnemyType.SLIME;
                _CreateEnemy.transform.name = "Slime";
                _CreateEnemy.mStat.hp = 100;
                _CreateEnemy.nvAgent.speed = 5;
                _CreateEnemy.gear = 5;
                break;
            case Enemy.eEnemyType.ORC:
                _CreateEnemy.mStat.mEnemyName = Enemy.eEnemyType.ORC;
                _CreateEnemy.transform.name = "Orc";
                _CreateEnemy.mStat.hp = 200;
                _CreateEnemy.nvAgent.speed = 2.5f;
                _CreateEnemy.gear = 20;
                break;
            case Enemy.eEnemyType.HIGHORC:
                _CreateEnemy.mStat.mEnemyName = Enemy.eEnemyType.HIGHORC;
                _CreateEnemy.transform.name = "Highorc";
                _CreateEnemy.mStat.hp = 1000;
                _CreateEnemy.nvAgent.speed = 2.5f;
                _CreateEnemy.gear = 777;
                break;
            default:
                break;
        }
        
        return _CreateEnemy;
    }

    public bool EnemyCreate(Enemy.eEnemyType _EnemyName,List<GameObject> _listEnemyobj,List<Enemy> _listEnemy,Transform _parentenemy)
    {
        GameObject MakeEnemy;
        
        switch (_EnemyName)
        {
            case Enemy.eEnemyType.NULL:
                return false;
            case Enemy.eEnemyType.GOBLIN:
                MakeEnemy = Instantiate(preGoblin, _parentenemy);
                break;
            case Enemy.eEnemyType.BOMBGOBLIN:
                MakeEnemy = Instantiate(preBombgoblin, _parentenemy);
                break;
            case Enemy.eEnemyType.SLIME:
                MakeEnemy = Instantiate(preSlime, _parentenemy);
                break;
            case Enemy.eEnemyType.ORC:
                MakeEnemy = Instantiate(preOrc, _parentenemy);
                break;
            case Enemy.eEnemyType.HIGHORC:
                MakeEnemy = Instantiate(preHighOrc, _parentenemy);
                break;
            default:
                return false;
        }

        MakeEnemy.SetActive(false);
        Enemy MakeEnemyScript = MakeEnemy.GetComponent<Enemy>();


        

        EnemyStatSet(_EnemyName, MakeEnemyScript);

        //하이오크에 최상위 부모가 렌더러가 없어서 문제가 발생 임시로 빼둠
        if (_EnemyName != Enemy.eEnemyType.HIGHORC)
            MakeEnemyScript.initEnemy();
        MakeEnemy.transform.position = GameManager.stGameManager.mStageManager.trSpawner.position;
        _listEnemyobj.Add(MakeEnemy);
        MakeEnemyScript.MonsterIdx = GameManager.stGameManager.mStageManager.nListFieldidx;
        _listEnemy.Add(MakeEnemyScript);
        GameManager.stGameManager.mStageManager.nListFieldidx++;

        return true;
    }
}
