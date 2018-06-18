using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Setting : MonoBehaviour {

    [Serializable]
    public class Set
    {
        public string name;
        public bool check;
        [Range(0,100)] public int value;
        public bool OnSlider;
    }

    public enum SETKIND { BACKGROUNDSOUND, EFFECTSOUND, VIBRATION, SENSITIVIRY }
    
    [Header("효과")]
    public Set backgroundSound;
    public Set effectSound;
    public Set vibration;

    [Header("조작")]
    public Set sensitiviry;

    public Set GetSetting(SETKIND kind)
    {
        Set temp = null;
        switch (kind)
        {
            case SETKIND.BACKGROUNDSOUND:
                temp = backgroundSound;
                break;
            case SETKIND.EFFECTSOUND:
                temp = effectSound;
                break;
            case SETKIND.VIBRATION:
                temp = vibration;
                break;
            case SETKIND.SENSITIVIRY:
                temp = sensitiviry;
                break;
        }
        return temp;
    }

    public void ChangeValue(SETKIND kind, int value)
    {
        GetSetting(kind).value = value;
    }

    public void OnOffSet(SETKIND kind, bool value)
    {
        GetSetting(kind).check = value;
    }
}
