using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUISetting : MonoBehaviour {

    public void Init(Setting setting)
    {
        for(int i = 0; i < guiSets.Length; i++)
        {
            guiSets[i].Init(setting.GetSetting((Setting.SETKIND)i));
        }
    }
    public GUISet[] guiSets;
}
