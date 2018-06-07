using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManager : MonoBehaviour {
    private static MainManager instance = null;
    public static MainManager Instance
    {
        get { return instance; }
    }
    public MainGUIManager mainGUI;
    public Player player;
    public Setting setting;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeValueSet(Setting.SETKIND kind, int value)
    {
        setting.ChangeValue(kind, value);
    }

    private void Start()
    {
        player.Init(100);
        mainGUI.Init();
        mainGUI.SetGoldText(player.Gold);
    }

    public void ChangeGold(int value)
    {
        mainGUI.SetGoldText(player.ChangeGold(value));
    }

    public int GetGold()
    {
        return player.Gold;
    }

    public void StageStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnOffSet(Setting.SETKIND kind, bool value)
    {
        setting.OnOffSet(kind, value);
    }
}
