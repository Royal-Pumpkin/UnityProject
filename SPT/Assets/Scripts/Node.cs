using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public Color hoverColor;
    public Vector3 posiotionOffset;
    public Collider colNodeCollider;

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
            
            return;
        }

        GameManager.stGameManager.SetPlayerState(GameManager.ePlayerState.BUILD);
        GameManager.stGameManager.mGUIManager.mGUINomalMode.BuildModeOn(transform, posiotionOffset, Input.mousePosition);
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
