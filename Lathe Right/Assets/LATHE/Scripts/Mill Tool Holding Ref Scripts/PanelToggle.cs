using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggle : MonoBehaviour
{

    [SerializeField] GameObject descriptionPanel, toolSetupExplanation;

    private GameObject currentPanel;
    private Boolean visible = true;

    // Start is called before the first frame update
    void Start()
    {
        descriptionPanel.SetActive(false);
        toolSetupExplanation.SetActive(false);
        currentPanel = toolSetupExplanation;
    }

    // Update is called once per frame
    public void showDescription()
    {
        if (visible)
        {
            if (toolSetupExplanation.activeSelf)
            {
                toolSetupExplanation.SetActive(false);
            }
            descriptionPanel.SetActive(true);
        }
        currentPanel = descriptionPanel;
    }
    public void showExplanation()
    {
        if (visible)
        {
            if (descriptionPanel.activeSelf)
            {
                descriptionPanel.SetActive(false);
            }
            toolSetupExplanation.SetActive(true);
        }
        currentPanel = toolSetupExplanation;

    }

    public void toggle()
    {
        if (!visible)
        {
            currentPanel.SetActive(true);
        }
        else
        {
            toolSetupExplanation.SetActive(false);
            descriptionPanel.SetActive(false);
        }
        visible = !visible;
    }


}
