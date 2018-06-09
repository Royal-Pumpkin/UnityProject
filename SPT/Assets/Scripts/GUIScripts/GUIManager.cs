using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {
    public List<GameObject> mListGUIScene = new List<GameObject>();
    public enum eGUISceneName {NULL=-1,NOMALSCENE,CONTROLSCENE,GAMEOVER,CLEAR}
    eGUISceneName mGUISceneState;
    public GUIControlMode mGUIControlMode;
    public GUINomalMode mGUINomalMode;

	// Use this for initialization
	void Start () {
        mGUISceneState = eGUISceneName.NOMALSCENE;

    }
	
	// Update is called once per frame
	void Update () {

        //나중에 스택으로 바꾸던지 해서 업데이트에서 관리안하고 바뀔때만 부르는 함수로 만들기
		for(int i=0; i<mListGUIScene.Count;i++)
        {
            if ((int)mGUISceneState == i)
            {
                mListGUIScene[i].SetActive(true);
            }
            else
                mListGUIScene[i].SetActive(false);
        }
	}

    public void SetGUIScene(eGUISceneName _GUISceneName)
    {
        mGUISceneState = _GUISceneName;
    }
}
