using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private Camera_Toggle uiCamera;
    private bool lang = true;
    private ToolTipToggle toggle;
    private Text tipText;
    private RectTransform background;
    private void Awake()
    {
        background = transform.GetChild(0).GetComponent<RectTransform>();
        tipText = transform.GetChild(1).GetComponent<Text>();
        HideTip();
        
    }



    public void ShowTip()
    {
        gameObject.SetActive(true);
        gameObject.transform.localPosition = toggle.GetComponentInParent<Transform>().localPosition;
        tipText.text = toggle.getName(lang);
        float text_padding = 4f; 
        Vector2 backgroundSize = new Vector2(tipText.preferredWidth + text_padding * 2f, tipText.preferredHeight + text_padding * 2f);
        background.sizeDelta = backgroundSize;

    }

    public void HideTip()
    {
        gameObject.SetActive(false);
    }

    public void setToolTipToggle(ToolTipToggle newToggle)
    {
        toggle = newToggle;
    }

    public void toggleLang()
    {
        lang = !lang;
    }

}
