using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControlMode : MonoBehaviour {
    public Button NomalModeBtn;
    public Button FireBtn;
	// Use this for initialization

    public void FuncctionSet()
    {
        NomalModeBtn.onClick.AddListener(() => GameManager.stGameManager.CameraReset());
        NomalModeBtn.onClick.AddListener(() => GameManager.stGameManager.mEventManager.Event(EventManger.eEventName.TOWEROUT));
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
