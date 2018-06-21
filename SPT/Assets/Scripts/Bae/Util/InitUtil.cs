using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class InitUtil
{
    /*1. 
     */
    public static void Init(MainManager mainManager){
        Player player = mainManager.player;
        JsonData towerTreeData = FileUtil.LoadJsonData("tower_tree");
        List<Player.TowerTreeNode[]> towerTree = JsonDataConversion.GetTowerTree(towerTreeData["trees"]);
        //플레이 데이터가 없을 경우
        if (!PlayerPrefsUtil.IsPlay)
        {
            Debug.Log("시작");
            player.Init(PlayerPrefsUtil.InitStageInfoList(), towerTree);
            PlayerPrefsUtil.PlayerGold = player.Gold;
            PlayerPrefsUtil.PlayerGem = player.Gem;
            PlayerPrefsUtil.PlayerKey = player.Key;
            PlayerPrefsUtil.PlayerKeyRecovryTime = "full";
            PlayerPrefsUtil.PlayerMaxKey = player.MaxKey;
            PlayerPrefsUtil.AllSaveTowerTreeUsable(towerTree);
            PlayerPrefsUtil.SetIsPlay();
        }
        else
        {
            PlayerPrefsUtil.SettilngTowerTreeUsable(towerTree);
            player.Init(PlayerPrefsUtil.PlayerGold, PlayerPrefsUtil.PlayerGem, PlayerPrefsUtil.PlayerKey, PlayerPrefsUtil.PlayerKeyRecovryTime, PlayerPrefsUtil.PlayerMaxKey, PlayerPrefsUtil.GetStageInfoList(), towerTree);
        }
#if UNITY_EDITOR
        mainManager.towerUpradeManager.SetName();
#endif
    }
}
