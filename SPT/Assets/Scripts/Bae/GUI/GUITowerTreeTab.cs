using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITowerTreeTab : MonoBehaviour {

    public GameObject treeUIObject;
    public GUIUpgarde[] upgardes;
    public Color color;

    //구매되지 않은 노드들을 들고 있다가 그걸 통해서 검사 하는게 for문 반복수는 줄어들 수 있음
    public void CheckGold()
    {
        for(int i = 0; i < upgardes.Length; i++)
        {
            upgardes[i].CheckGold();
        }
    }
    
}
