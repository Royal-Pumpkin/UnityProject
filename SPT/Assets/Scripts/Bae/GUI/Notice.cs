using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Notice : MonoBehaviour {
    public string[] messages;
    public Text messageText;

    /// <summary>
    /// 0: 구매 성공, 1: 구매 실패
    /// </summary>
    /// <param name="messageNum"></param>
    public void OnNotice(int messageNum)
    {
        messageText.text = messages[messageNum];
        gameObject.SetActive(true);
    }
    public void OffNotice()
    {
        gameObject.SetActive(false);
    }
}
