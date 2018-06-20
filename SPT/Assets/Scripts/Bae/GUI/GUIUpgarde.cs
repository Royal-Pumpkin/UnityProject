using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GUIUpgarde : MonoBehaviour,IPointerClickHandler {
    public Text nameText;
    public RawImage image;
    public Text goldText;

    //public string nameStr;
    public Texture[] texture;
    public Sprite sprite;
    public int gold;

    public int num;
    //구매 완료, 구매 가능, 구매 불가능
    bool isEnable;

    public enum STATE { COM,AVAIL,UNAVAIL}
    /// <summary>
    /// 0 : 구매완료, 1 : 구매 가능, 2 : 구매 불가능
    /// </summary>
    STATE state;


    public void Init(int num, string name,int usable)
    {
        STATE state = (STATE)usable;
        SetInfo(name, texture[usable], gold);
        this.num = num;
        switch (state)
        {
            case STATE.COM:
                Activate();
                break;
            case STATE.AVAIL:
                break;
            case STATE.UNAVAIL:
                break;
        }
        this.state = state;
    }

    public void SetInfo(string name, Texture texture, int gold)
    {
        nameText.text = name;
        image.texture = texture;
        goldText.text = gold.ToString();
    }

    public void CheckGold()
    {
        if (MainManager.Instance.GetGold() < gold)
        {
            goldText.color = Color.red;
        }
        else
        {
            goldText.color = Color.black;
        }
    }

    //구매가능이 되었을 때 사용 
    public void ChangeState(int usable)
    {
        //image.color = Color.white;
        image.texture = texture[usable];
        state = (STATE)usable;
        switch (state)
        {
            case STATE.COM:
                Activate();
                break;
            case STATE.AVAIL:
                break;
            case STATE.UNAVAIL:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MainManager.Instance.mainGUI.notice.OnNoticeUpgrade(num,(int)state,gold, sprite, nameText.text);
    }

    public void Activate()
    {
        state = STATE.COM;
        goldText.gameObject.SetActive(false);        
    }
}
