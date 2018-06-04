using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    public enum eTowerType { NULL = -1, A, B, C }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("no BM");
            return;
        }
        instance = this;
    }

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

    public bool BuildTower(eTowerType _Type)
    {
        switch (_Type)
        {
            case eTowerType.NULL:
                return false;
            case eTowerType.A:
                //GameObject turreyToBuild = GetTurretTobuild();
                //turret = (GameObject)Instantiate(turreyToBuild, transform.position + posiotionOffset, transform.rotation);
                //GameManager.stGameManager.AddTowerList(turret.GetComponent<Tower>());
                //colNodeCollider.enabled = false;

                break;
            case eTowerType.B:
                break;
            case eTowerType.C:
                break;
            default:
                return false;
        }

        return true;
    }
}
