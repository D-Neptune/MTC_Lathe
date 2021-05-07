using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CertificateInput : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SceneDisplayToggle tutorialDone;
    [SerializeField] private LanguageSceneToggle InputInformation;
    [SerializeField] private List<ToggleText> toggleList;
    [SerializeField] private Text fullNameText;
    [SerializeField] private Text studentNumberText;


    void Start()
    {
        if (InputInformation.getLanguage())
        {
            toggleOnStart();
        }
        tutorialDone.TutorialDone();
        fullNameText.text = InputInformation.Name;
        studentNumberText.text = InputInformation.Number;
        SaveSystem.DeleteFileExists();
    }

    private void toggleOnStart()
    {
        foreach (ToggleText toggleText in toggleList)
        {
            toggleText.toggle();
        }
    }
}
