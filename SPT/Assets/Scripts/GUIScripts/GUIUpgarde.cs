using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIUpgarde : MonoBehaviour {
    public Text nameText;
    public RawImage image;
    public Text goldText;
	
    public void SetInfo(string name, Texture texture, string gold)
    {
        nameText.text = name;
        image.texture = texture;
        goldText.text = gold;
    }
}
