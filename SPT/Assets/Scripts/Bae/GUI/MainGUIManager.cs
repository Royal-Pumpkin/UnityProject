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

    [Header("text")]
    public Text goldText;
    public Text diaText;
    public Text keyText;

    public GameObject backBtn;

    [Header("stage")]
    public GUIStageButton[] stageBtns;
    public Sprite[] stageButtonTexture;

    [Header("tower tree")]
    public GUITowerTreeTab[] tabs;
    public int onTabNumber;

    private void Start()
    {
        SetGUIScene(MainGUISceneName.MAIN);
    }
    public void Init(Player player)
    {
        SetGoldText(player.Gold);
        SetDiaText(player.Gem);
        SetKeyText(player.Key, player.MaxKey);
        guiSetting.GetComponent<GUISetting>().Init(MainManager.Instance.setting);

        StageBtnImageSet(player, 1);

        for(int i=0;i< player.TowerTreeCount; i++)
        {
            for(int j = 0; j < player.NodesNumberPerTree(i); j++)
            {                
                tabs[i].upgardes[j].Init(player.GetTowerTreeNode(i+1,j+1).num, player.GetTowerTreeNode(i + 1, j + 1).treeName, player.GetTowerTreeNode(i + 1, j + 1).usable);
            }
        }
    }
    public void StageBtnImageSet(Player player, int difficulty)
    {
        for (int i = 0; i < stageBtns.Length; i++)
        {
            Player.StageClearInfo info = player.GetStageClearInfo(i + 1, difficulty);
            stageBtns[i].Init(info.star, info.clear);
        }
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
        switch (_GUISceneName)
        {
            case MainGUISceneName.NULL:
                break;
            case MainGUISceneName.MAIN:
                break;
            case MainGUISceneName.ARMORY:
                tabs[onTabNumber].CheckGold();
                break;
            case MainGUISceneName.STAGE:
                break;
        }
    }
    public void OnOffSettingGUI(bool value)
    {
        guiSetting.SetActive(value);
    }

    public void OnOffMainGUI(bool value)
    {
        MainGUI.SetActive(value);
    }

    //현재는 메인화면으로 가면 되지만 화면깊이가 생기면 변경필요
    public void OnClickBackBtn()
    {
        SetGUIScene(0);
    }

    public void OnClickDifficulty(int num)
    {
        MainManager.Instance.Difficulty = num;
        StageBtnImageSet(MainManager.Instance.player, num);
    }

    public void OnClickTowerTreeTab(int num)
    { 
        tabs[onTabNumber].treeUIObject.SetActive(false);
        tabs[num].treeUIObject.SetActive(true);
        onTabNumber = num;
    }
    public void GUIUpgradeStateChange(int treeNumber, int nodeNumber, int usable)
    {
        //아직은 usable 파라미터 사용안됨
        tabs[treeNumber - 1].upgardes[nodeNumber - 1].ChangeState(usable);
        tabs[treeNumber - 1].CheckGold();
    }
}
