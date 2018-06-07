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
    bool isEnable;

    private void Start()
    {
        Init(0,false);
    }
    void Init(int num,bool isEnable)
    {
        SetInfo(nameStr, texture, gold);
        this.num = num;

        if (isEnable)
        {
            Activate();
        }
        
    }

    public void SetInfo(string name, Texture texture, int gold)
    {
        nameText.text = name;
        image.texture = texture;
        goldText.text = gold.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isEnable)
        {
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
        }        
    }

    public void Activate()
    {
        isEnable = true;
        goldText.text = "구매";
    }
}
