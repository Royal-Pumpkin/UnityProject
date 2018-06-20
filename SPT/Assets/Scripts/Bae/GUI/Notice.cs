using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Notice : MonoBehaviour {
    public string[] messages;
    public string[] messages2;
    public Text messageText;
    public Text message2Text;
    public GameObject normalNotice;
    public GameObject upgradeNotice;
    public Text buyBtnText;
    public GameObject noticeInNotice;

    [Header("업그레이드 gui용")]
    public Image upgradeNoticeImage;
    public Text upgradeNoticeName;
    public Color[] colors;

    string[] buyBtnStr =
    {
        "\n완료","\n업그레이드 하기", "\n구매 불가능"
    };

    int upgradeNum;
    int state;
    int gold;
    /// <summary>
    /// 0: 구매 성공, 1: 구매 실패
    /// </summary>
    /// <param name="messageNum"></param>
    public void OnNotice(int messageNum)
    {
        messageText.text = messages[messageNum];
        normalNotice.SetActive(true);
        gameObject.SetActive(true);
    
    }
    public void OffNotice()
    {
        normalNotice.SetActive(false);
        gameObject.SetActive(false);
    }
    public void OnNoticeUpgrade(int upgradeNum, int state,int gold,Sprite sprite,string name)
    {
        this.upgradeNum = upgradeNum;
        this.state = state;
        this.gold = gold;

        upgradeNoticeImage.sprite = sprite;
        upgradeNoticeName.text = name;
        SettingUpgradeNotice(state);
        upgradeNotice.SetActive(true);
        gameObject.SetActive(true);
    }
    public void UpdateNoticeUpgrade(int state)
    {
        this.state = state;
        SettingUpgradeNotice(state);
    }
    public void OnNoticeInNotice(int messageNum, object arg=null)
    {
        if (arg != null)
        {
            
            message2Text.text = messages2[messageNum].Replace("/n", "\n");
            if (messages2[messageNum].Contains("**"))
            {

                message2Text.text = messages2[messageNum].Replace("**", arg.ToString()).Replace("/n", "\n");
            }
        }
        else
        {
            message2Text.text = messages2[messageNum];
        }
        noticeInNotice.SetActive(true);
    }
    public void OffNoticeInNotice()
    {
        noticeInNotice.SetActive(false);
    }
    public void OffNoticeUpgrade()
    {
        upgradeNotice.SetActive(false);
        gameObject.SetActive(false);
    }
    void SettingUpgradeNotice(int state)
    {
        buyBtnText.text = gold+buyBtnStr[state];
        if (state == 2)
        {
            upgradeNoticeImage.color = colors[1];
        }
        else
        {
            upgradeNoticeImage.color = colors[0];
        }

    }
    public void OnClickBuy()
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                if (MainManager.Instance.GetGold() >= gold)
                {
                    OnNoticeInNotice(0, gold);
                    MainManager.Instance.ChangeGold(-gold);
                    MainManager.Instance.BuyTowerNode(upgradeNum);
                }
                else
                {
                    OnNoticeInNotice(1, gold-MainManager.Instance.GetGold());
                }
                
                break;
            case 2:
                OnNoticeInNotice(2);
                break;
        }
    }
}
