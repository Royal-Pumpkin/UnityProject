using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainGUIManager : MonoBehaviour {
    public GameObject MainGUI;
    public GameObject[] scenes;
    public enum MainGUISceneName { NULL = -1, MAIN, ARMORY, STAGE }
    public MainGUISceneName sceneState;
    public Notice notice;
    public GameObject guiSetting;
    public Text goldText;
    public Text diaText;
    public Text keyText;
    public GameObject backBtn;

    private void Start()
    {
        SetGUIScene(MainGUISceneName.MAIN);
    }
    public void Init(Player player)
    {
        SetGoldText(player.Gold);
        SetDiaText(player.Diamond);
        SetKeyText(player.Key, player.MaxKey);
        guiSetting.GetComponent<GUISetting>().Init(MainManager.Instance.setting);
    }

    public void SetGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }
    public void SetDiaText(int dia)
    {
        diaText.text = dia.ToString();
    }
    public void SetKeyText(int key, int maxKey)
    {
        keyText.text = key+" / "+maxKey;
    }

    public void SetGUIScene(int sceneNumber)
    {
        SetGUIScene((MainGUISceneName)sceneNumber);
    }

    public void SetGUIScene(MainGUISceneName _GUISceneName)
    {
        sceneState = _GUISceneName;

        backBtn.SetActive(_GUISceneName != MainGUISceneName.MAIN);

        for (int i = 0; i < scenes.Length; i++)
        {
            if ((int)sceneState == i)
            {
                scenes[i].SetActive(true);
            }
            else
                scenes[i].SetActive(false);
        }
    }
    public void OnOffSettingGUI(bool value)
    {
        guiSetting.SetActive(value);
    }

    //현재는 메인화면으로 가면 되지만 화면깊이가 생기면 변경필요
    public void OnClickBackBtn()
    {
        SetGUIScene(0);
    }
}
