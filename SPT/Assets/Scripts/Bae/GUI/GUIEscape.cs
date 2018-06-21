using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GUIEscape : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(object.ReferenceEquals(eventData.pointerEnter,gameObject))
            gameObject.SetActive(false);
    }
    public void OnClickOk()
    {
        Application.Quit();
    }
    public void OnClickCancel()
    {
        gameObject.SetActive(false);
    }
}
