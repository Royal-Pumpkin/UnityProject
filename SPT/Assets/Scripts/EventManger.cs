using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManger : MonoBehaviour {

	public enum eEventName{NULL=-1,TOWERPICK,TOWEROUT,ENEMYSPWAN}
    public enum eEnemyName {NULL=-1,ONE,TWO }

    //임시로 등록 나중에 resource불러오는걸로 변경?
    public GameObject prefabEnemy1;
    public GameObject prefabEnemy2;
    

    

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

    public bool Event(eEventName _Event,eEnemyName _enemy)
    {
        switch (_Event)
        {
            case eEventName.NULL:
                return false;
            case eEventName.ENEMYSPWAN:
                //EnemySpawn();
                return true;
            default:
                return false;
        }
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

            PickedTower.TowerGetin(Camera.main);
            GameManager.stGameManager.mControlTower = PickedTower;
            GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.TOWER);
            GameManager.stGameManager.mGUIManager.SetGUIScene(GUIManager.eGUISceneName.CONTROLSCENE);
            return true;
        }

        return false;
    }

    void TowerOutUIPick()
    {
        if (GameManager.stGameManager.mControlTower)
        {
            GameManager.stGameManager.mControlTower.TowerGetout(Camera.main);
            GameManager.stGameManager.mGUIManager.SetGUIScene(GUIManager.eGUISceneName.PLAYSCENE);
        }
        else
            return;
    }

    Enemy EnemyStatSet(eEnemyName _EnemyName,Enemy _CreateEnemy)
    {
        
        switch (_EnemyName)
        {
            case eEnemyName.NULL:
                _CreateEnemy.mStat.mEnemyName = eEnemyName.NULL;
                break;
            case eEnemyName.ONE:
                _CreateEnemy.mStat.mEnemyName = eEnemyName.ONE;
                _CreateEnemy.mStat.hp = 10;
                break;
            case eEnemyName.TWO:
                _CreateEnemy.mStat.mEnemyName = eEnemyName.TWO;
                _CreateEnemy.mStat.hp = 20;
                break;
            default:
                _CreateEnemy.mStat.mEnemyName = eEnemyName.NULL;
                break;
        }
        return _CreateEnemy;
    }

    public bool EnemyCreate(eEnemyName _EnemyName,List<GameObject> _listEnemy)
    {
        GameObject MakeEnemy = _listEnemy[GameManager.stGameManager.nListFieldidx];
        Enemy MakeEnemyScript = MakeEnemy.GetComponent<Enemy>();
        MeshRenderer EnemyMeshRenderer = MakeEnemy.GetComponent<MeshRenderer>();
        Material[] tempmaterial = EnemyMeshRenderer.materials;

        switch (_EnemyName)
        {
            case eEnemyName.NULL:
                return false;
            case eEnemyName.ONE:

                MakeEnemyScript.mStat.hp = 10;
                MakeEnemy.transform.name = "one";
                MakeEnemy.SetActive(true);
                MakeEnemy.transform.position = GameManager.stGameManager.trSpawner.position;
                GameManager.stGameManager.nListFieldidx++;


                //EnemyStatSet(_EnemyName, tempGameobj.GetComponent<Enemy>());
                //_listEnemy.Add(tempGameobj);
                break;
            case eEnemyName.TWO:

                


                MakeEnemyScript.mStat.hp = 20;
                tempmaterial[0] = Resources.Load<Material>("Materials/One"/*나중에 stat에 Matarial path 넣어서 교체*/);
                EnemyMeshRenderer.materials = tempmaterial;
                MakeEnemy.transform.name = "two";
                MakeEnemy.SetActive(true);
                MakeEnemy.transform.position = GameManager.stGameManager.trSpawner.position;
                GameManager.stGameManager.nListFieldidx++;

                //tempGameobj = Instantiate(prefabEnemy2, trSpawner);
                //EnemyStatSet(_EnemyName, tempGameobj.GetComponent<Enemy>());
                //_listEnemy.Add(tempGameobj);
                break;
            default:
                return false;
        }

       
        
        return true;
    }
}
