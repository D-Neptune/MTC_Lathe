using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDragger : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.position = Input.mousePosition;
    }


    public void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.control && e.alt && e.keyCode == KeyCode.R)
        {
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}
