using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public enum eTowerType { NULL = -1, A, B, C }



    public GameObject standardTurretPrefab;

    GameObject turretToBuild;

    // Use this for initialization
    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }
    public GameObject GetTurretTobuild()
    {
        return turretToBuild;
    }

    public GameObject MakeTower()
    {
        //지금은 그냥 기본타워만 생성 후에 미리 여러개의 타워를 설치 혹은 빈공간만 만들어놓고 생성해서 쓰도록함
        GameObject tempobj = Instantiate(standardTurretPrefab,GameManager.stGameManager.mGoal);
        return tempobj;
    }

    public bool BuildTower(eTowerType _Type,Transform _nodeTr,Vector3 _offset)
    {
        GameObject turretToBuild = GameManager.stGameManager.GetFieldTowerList()[GameManager.stGameManager.nListTowerIdx].gameObject;
        Tower turretScript = turretToBuild.GetComponent<Tower>();

        switch (_Type)
        {
            
            case eTowerType.NULL:
                return false;
            case eTowerType.A:
                

                turretToBuild.transform.position = _nodeTr.position + _offset;
                turretToBuild.transform.rotation = _nodeTr.rotation;
                GameManager.stGameManager.AddFieldTowerList(turretToBuild.GetComponent<Tower>());
                turretScript.mTowerType = eTowerType.A;
                
                GameManager.stGameManager.nListTowerIdx++;

                break;
            case eTowerType.B:
                //GameObject turretToBuild = GameManager.stGameManager.GetFieldTowerList()[GameManager.stGameManager.nListTowerIdx].gameObject;
                //Tower turretScript = turretToBuild.GetComponent<Tower>();

                turretToBuild.transform.position = _nodeTr.position + _offset;
                turretToBuild.transform.rotation = _nodeTr.rotation;
                GameManager.stGameManager.AddFieldTowerList(turretToBuild.GetComponent<Tower>());
                turretScript.mTowerType = eTowerType.B;
                GameManager.stGameManager.nListTowerIdx++;
                break;
            case eTowerType.C:
                //GameObject turretToBuild = GameManager.stGameManager.GetFieldTowerList()[GameManager.stGameManager.nListTowerIdx].gameObject;
                //Tower turretScript = turretToBuild.GetComponent<Tower>();

                turretToBuild.transform.position = _nodeTr.position + _offset;
                turretToBuild.transform.rotation = _nodeTr.rotation;
                GameManager.stGameManager.AddFieldTowerList(turretToBuild.GetComponent<Tower>());
                turretScript.mTowerType = eTowerType.C;
                GameManager.stGameManager.nListTowerIdx++;
                break;
            default:
                return false;
        }

        turretToBuild.SetActive(true);
        GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.NOMAL);
        return true;
    }
}
