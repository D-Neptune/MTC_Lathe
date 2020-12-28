using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationBeginTrainingHandler : MonoBehaviour
{
    [SerializeField] private SceneDisplayToggle sceneDisplay;
    [SerializeField] private LoadSceneScript loadSceneScript;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private Button begin;
    private Boolean firstClick;
    // Start is called before the first frame update
    public void OnClick(string millScene)
    {
        if (!firstClick && !sceneDisplay.SubmitDone)
        {
            namePanel.SetActive(true);
            begin.interactable = false;
            firstClick = true;
        }
        else
        {
            sceneDisplay.setTutorial(true);
            loadSceneScript.loadlevel(millScene);
        }
    }
}
