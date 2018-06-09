using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUISet : MonoBehaviour {
    public Setting.SETKIND kind;
    public string setName;
    public Toggle toggle;
    public Slider slider;
    public Text sliderValue;
    public void Init(Setting.Set set)
    {
        toggle.isOn = set.check;
        if (set.OnSlider)
        {
            slider.value = set.value;
        }        
    }
    public void ToggleOnValueChanged(bool value)
    {
        MainManager.Instance.OnOffSet(kind, value);
        slider.interactable = value;
    }
    public void SliderOnValueChanged()
    {
        if (!slider)
            return;
        int value = (int)slider.value;
        MainManager.Instance.ChangeValueSet(kind, value);
        sliderValue.text = value.ToString();
    }
}
