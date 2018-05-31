using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public Color hoverColor;
    public Vector3 posiotionOffset;

    public GameObject turret;

    Renderer rend;
    Color startColor;
    Color notEnoughColor;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        notEnoughColor = hoverColor;
    }

    private void OnMouseDown()
    {
        if (turret != null || GameManager.stGameManager.GetPlayerState() != GameManager.ePlayerState.NOMAL)
        {
            //Debug.Log("Can not build here");
            return;
        }
        GameObject turreyToBuild = BuildManager.instance.GetTurretTobuild();
        turret = (GameObject)Instantiate(turreyToBuild, transform.position + posiotionOffset, transform.rotation);
        GameManager.stGameManager.AddTowerList(turret.GetComponent<Tower>());
    }

    private void OnMouseEnter()
    {
        rend.material.color = notEnoughColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
