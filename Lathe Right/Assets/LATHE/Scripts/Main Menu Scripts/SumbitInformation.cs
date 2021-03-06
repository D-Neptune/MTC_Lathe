using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SumbitInformation : MonoBehaviour
{
    [SerializeField] private LoadSceneScript loadScene;
    [SerializeField] private string AdminName, AdminPassword;
    [SerializeField] private SceneDisplayToggle displayToggle;
    [SerializeField] private LanguageSceneToggle InputInformation;
    [SerializeField] private InputField StudentNumberInputPanel;
    [SerializeField] private Text fullNameText;
    [SerializeField] private Text studentNumberText;
    [SerializeField] private Text infoWarning, passwordWarning;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button begin;
    [SerializeField] private string SceneName;
    private string fullName, num, warning, wrongPassword;

    private void Start()
    {
        fullName = fullNameText.text;
        num = studentNumberText.text;
        warning = infoWarning.text;
        wrongPassword = passwordWarning.text;
    }

    public void AdminNameSubmitted()
    {
        if (fullNameText.text.Equals(AdminName))
        {
            StudentNumberInputPanel.contentType = InputField.ContentType.Alphanumeric;
        }
        else
        {
            StudentNumberInputPanel.contentType = InputField.ContentType.IntegerNumber;
        }
    }

    public void SubmitInformation()
    {
        if(fullName == fullNameText.text || num == studentNumberText.text)
        {
            infoWarning.gameObject.SetActive(true);
            passwordWarning.gameObject.SetActive(false);
        }
        else if((fullNameText.text.Equals(AdminName) && studentNumberText.text.Equals(AdminPassword)))
        {
            Debug.Log("ADMIN MODE SHOULD BE ACTIVATED");
            displayToggle.AdminMode = true;
            displayToggle.setTutorial(true);
            infoWarning.gameObject.SetActive(false);
            passwordWarning.gameObject.SetActive(false);
            begin.interactable = true;
            InputInformation.setName(fullNameText.text);
            InputInformation.setNumber(studentNumberText.text);
            panel.SetActive(false);
            displayToggle.SubmitDone = true;
            loadScene.loadlevel(SceneName);
        }
        else if ((fullNameText.text.Equals(AdminName) && !studentNumberText.text.Equals(AdminPassword)))
        {
            passwordWarning.gameObject.SetActive(true);
            infoWarning.gameObject.SetActive(false);
        }
        else
        {
            displayToggle.setTutorial(true);
            infoWarning.gameObject.SetActive(false);
            passwordWarning.gameObject.SetActive(false);
            begin.interactable = true;
            InputInformation.setName(fullNameText.text);
            InputInformation.setNumber(studentNumberText.text);
            panel.SetActive(false);
            displayToggle.SubmitDone = true;
            loadScene.loadlevel(SceneName);
        }

    }
}
