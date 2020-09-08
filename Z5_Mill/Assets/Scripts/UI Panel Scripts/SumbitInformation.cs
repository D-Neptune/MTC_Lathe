﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SumbitInformation : MonoBehaviour
{
    [SerializeField] private SceneDisplayToggle displayToggle;
    [SerializeField] private LanguageSceneToggle InputInformation;
    [SerializeField] private Text fullNameText;
    [SerializeField] private Text studentNumberText;
    [SerializeField] private Text infoWarning;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button begin;
    private string fullName, num;

    private void Start()
    {
        fullName = fullNameText.text;
        num = studentNumberText.text;
    }

    public void SubmitInformation()
    {
        if(fullName == fullNameText.text || num == studentNumberText.text)
        {
            infoWarning.gameObject.SetActive(true);
        }
        else
        {
            infoWarning.gameObject.SetActive(false);
            begin.interactable = true;
            InputInformation.setName(fullNameText.text);
            InputInformation.setNumber(studentNumberText.text);
            panel.SetActive(false);
            displayToggle.SubmitDone = true;
}

    }
}
