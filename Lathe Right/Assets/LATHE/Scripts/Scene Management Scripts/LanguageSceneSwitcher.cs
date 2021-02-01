using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ToggleText> toggleList;
    public ToolTip toggleHint;
    public DialogueOperator dialogueOperator;
    public LanguageSceneToggle languageScene;
    public DialogueManager toggleLangManager;
    public ComponentManager toggleComponentLangManager;
    public ImageToggle imageToggle;
    public AdditionalInfoManager toolHolderManager;
    public ToggleScene toggleScene;
    public VideoManager videoManager;
    public VideoOperator videoOperator;
    void Awake()
    {
        if (languageScene.getLanguage())
        {
            toggleOnStart();
        }
    }

    private void toggleOnStart()
    {
        foreach(ToggleText toggleText in toggleList)
        {
            toggleText.toggle();
        }

        
        if (toggleLangManager != null)
        {
            toggleLangManager.switchLang();
        }
        if (imageToggle != null)
        {
            imageToggle.toggle();
        }
        if(toggleComponentLangManager != null)
        {
            toggleComponentLangManager.ChangeLanguage();
        }
        if(toolHolderManager != null)
        {
            toolHolderManager.ChangeLanguage();
        }
        if(toggleHint != null)
        {
            toggleHint.toggleLang();
        }
        if(toggleScene != null)
        {
            toggleScene.SwitchLang();
        }
        if(videoManager != null)
        {
            videoManager.SwitchLang();
        }
        if(dialogueOperator != null)
        {
            dialogueOperator.switchLang();
        }
        if(videoOperator != null)
        {
            videoOperator.SwitchLang();
        }

    }

    public void updateLanguageState()
    {
        languageScene.setLanguage(!languageScene.getLanguage());
    }
}
