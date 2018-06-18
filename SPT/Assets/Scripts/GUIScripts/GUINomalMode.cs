using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GUINomalMode : MonoBehaviour {
    
    public GUIBuildMode mGUIBuildMode;
    public GUIModeSelect mGUiModeSelect;
    public Button ElseView;
    
    public Text Gear2;
    



    public void GUINomalInit()
    {
        mGUIBuildMode.InstansiateList();
        mGUiModeSelect.mGUIIngameUpgrade.InstansiateList();
        
    }

    public void BuildModeOn(Transform _selectnode,Vector3 _offset,Vector3 _panelpos)
    {
        mGUIBuildMode.SetPanelOn(_selectnode, _offset);
        mGUIBuildMode.Panel.transform.position = _panelpos;
    }

    public void ModeSelectOn(Tower _pickedtower, Vector3 _panelpos)
    {
        mGUiModeSelect.SetPanelOn(_pickedtower);
        mGUiModeSelect.Panel.transform.position = _panelpos;
    }

    
}
