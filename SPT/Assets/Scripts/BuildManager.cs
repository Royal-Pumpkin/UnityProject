using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

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


}
