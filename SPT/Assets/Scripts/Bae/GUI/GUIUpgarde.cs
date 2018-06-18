using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GUIUpgarde : MonoBehaviour,IPointerClickHandler {
    public Text nameText;
    public RawImage image;
    public Text goldText;

    public string nameStr;
    public Texture texture;
    public int gold;

    public int num;
    //구매 완료, 구매 가능, 구매 불가능
    bool isEnable;

    public enum STATE { NULL,COM,AVAIL,UNAVAIL}
    /// <summary>
    /// 0 : 구매완료, 1 : 구매 가능, 2 : 구매 불가능
    /// </summary>
    STATE state;


    void Init(int num, int usable)
    {
        STATE state = (STATE)usable;
        SetInfo(nameStr, texture, gold);
        this.num = num;
        switch (state)
        {
            case STATE.NULL:
                break;
            case STATE.COM:
                Activate();
                break;
            case STATE.AVAIL:
                break;
            case STATE.UNAVAIL:
                image.color = Color.gray;
                break;
        }
        this.state = state;
    }

    public void SetInfo(string name, Texture texture, int gold)
    {
        nameText.text = name;
        //image.texture = texture;
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
    void ChangeState()
    {
        image.color = Color.white;
        state = STATE.AVAIL;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (state)
        {
            case STATE.NULL:
                break;
            case STATE.COM:
                break;
            case STATE.AVAIL:
                if (MainManager.Instance.GetGold() >= gold)
                {
                    MainManager.Instance.mainGUI.notice.OnNotice(0);
                    MainManager.Instance.ChangeGold(-gold);
                    Activate();
                }
                else
                {
                    MainManager.Instance.mainGUI.notice.OnNotice(1);
                }
                break;
            case STATE.UNAVAIL:
                MainManager.Instance.mainGUI.notice.OnNotice(3);
                break;
        }
    }

    public void Activate()
    {
        state = STATE.COM;
        goldText.gameObject.SetActive(false);
    }
}
