using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorSpeedSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    [SerializeField] private Vector2 X_MaxAnchors;
    [SerializeField] private Vector2 X_MinAnchors;
   
    
    public void ChangeSpeedText()
    {
        RectTransform textRect = text.rectTransform;
        Vector2 offset = AnchorPos();
        textRect.anchorMin = new Vector2(X_MinAnchors.x + offset.x, textRect.anchorMin.y);
        textRect.anchorMax = new Vector2(X_MaxAnchors.x + offset.y, textRect.anchorMax.y);
        textRect.anchoredPosition = Vector2.zero;
        float scaledVal = (float) slider.value /10;

        text.text = scaledVal.ToString();
    }

    private Vector2 AnchorPos()
    {
        float tmpMinStep = ((float) (X_MinAnchors.y - X_MinAnchors.x) / (float) (slider.maxValue - slider.minValue)) * (slider.value-5);
        float tmpMaxStep = ((float) (X_MaxAnchors.y - X_MaxAnchors.x) / (float) (slider.maxValue - slider.minValue)) * (slider.value-5);
        return new Vector2(tmpMinStep, tmpMaxStep);
    }
}
