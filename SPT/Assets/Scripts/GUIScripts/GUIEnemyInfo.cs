using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GUIEnemyInfo : MonoBehaviour {
    public Image HpBar;


    private void Update()
    {
        Vector3 point = Camera.main.WorldToScreenPoint(this.transform.position);
        this.transform.LookAt(point);
    }

}
