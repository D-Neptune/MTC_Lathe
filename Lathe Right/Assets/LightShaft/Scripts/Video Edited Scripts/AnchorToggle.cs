using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToggle : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 AnchorMin_Minimized, AnchorMax_Minimized, AnchorMin_Maximized, AnchorMax_Maximized;
    public void SwitchAnchors(bool state)
    {
        if (!state)
        {
            rectTransform.anchorMin = AnchorMin_Minimized;
            rectTransform.anchorMax = AnchorMax_Minimized;
        }
        else
        {
            rectTransform.anchorMin = AnchorMin_Maximized;
            rectTransform.anchorMax = AnchorMax_Maximized;
        }

        rectTransform.anchoredPosition = Vector2.zero;
    }

}
