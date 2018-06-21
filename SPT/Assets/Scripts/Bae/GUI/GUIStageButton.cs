using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIStageButton : MonoBehaviour {
    public int stageNumber;
    public GameObject[] starObj;
    public Image btnImage;
    public Text nameText;
    public void ClickStage()
    {
        MainManager.Instance.StageStart(stageNumber);
    }
    public void Init(int star,bool clear,int stageNum)
    {
        SetInfo(star, clear);
        nameText.text = "stage " + stageNum;
    }
    public void SetInfo(int star,bool clear)
    {
        GetStar(star);
        ChangeButtonImage(clear);
    }
    public void GetStar(int starCount)
    {
        for(int i = 0; i < 3; i++)
        {
            starObj[i].SetActive(i<starCount);
        }
    }
    public void ChangeButtonImage(bool clear)
    {
        int i = 0;
        if (clear)
        {
            i = 1;
        }
        btnImage.sprite = MainManager.Instance.mainGUI.stageButtonTexture[i];
    }
}
