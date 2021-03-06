using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorSwitcher : MonoBehaviour
{
    [SerializeField] private Vector2 AnchorMinEN, AnchorMinFR, AnchorMaxEN, AnchorMaxFR;
    private RectTransform rectTransform;
    // Start is called before the first frame update

    public void SwitchAnchors(bool val)
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        if (val)
        {
            rectTransform.anchorMin = AnchorMinFR;
            rectTransform.anchorMax = AnchorMaxFR;
        }
        else
        {
            rectTransform.anchorMin = AnchorMinEN;
            rectTransform.anchorMax = AnchorMaxEN;
        }
        rectTransform.anchoredPosition = Vector2.zero;

    }
}
