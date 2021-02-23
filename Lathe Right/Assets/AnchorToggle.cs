using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorToggle : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 anchorOffsetMin, anchorOffsetMax;
    public void SwitchAnchors(bool state)
    {
        Vector2 offsetMin = new Vector2(anchorOffsetMin.x, anchorOffsetMin.y);
        Vector2 offsetMax = new Vector2(anchorOffsetMax.x, anchorOffsetMax.y);
        if (!state)
        {
            offsetMin *= -1;
            offsetMax *= -1;
        }
        rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x + offsetMin.x, rectTransform.anchorMin.y + offsetMin.y);
        rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x + offsetMax.x, rectTransform.anchorMax.y + offsetMax.y);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
