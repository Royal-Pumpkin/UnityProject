using UnityEngine;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
public class JsonDataConversion
{
    public static int[] ArrayIntToJsonData(JsonData data)
    {
        int[] temp = new int[data.Count];
        for(int i = 0; i < data.Count; i++)
        {
            temp[i] = (int)data[i];
        }
        return temp;
    }

    public static List<Player.TowerTreeNode[]> GetTowerTree(JsonData data)
    {
        List<Player.TowerTreeNode[]> towerTree = new List<Player.TowerTreeNode[]>();

        for(int i = 0; i < data.Count; i++)
        {
            JsonData tree = data[i];
            Player.TowerTreeNode[] towerTreeNodes = new Player.TowerTreeNode[tree.Count];
            for(int j = 0; j < tree.Count; j++)
            {
                towerTreeNodes[j].num = (int)tree[j]["num"];
                towerTreeNodes[j].treeName = (string)tree[j]["name"];
                towerTreeNodes[j].needNum = ArrayIntToJsonData(tree[j]["need_num"]);
                towerTreeNodes[j].nextNum = ArrayIntToJsonData(tree[j]["next_num"]);
                towerTreeNodes[j].usable = (int)tree[j]["usable"];
                towerTreeNodes[j].getTowerId = (int)tree[j]["get_tower"];
                Debug.Log(towerTreeNodes[j]);
            }
            towerTree.Add(towerTreeNodes);
        }
        
        return towerTree;
    }
}