using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CertificateToggle : MonoBehaviour
{
    [SerializeField] private SceneDisplayToggle sceneDisplay;
    [SerializeField] private LoadSceneScript loadSceneScript;
    [SerializeField] private string certificate, mainMenu;
    // Start is called before the first frame update
    public void OnClick()
    {
        if (sceneDisplay.getTutorial())
        {
            loadSceneScript.loadlevel(certificate);
        }
        else
        {
            loadSceneScript.loadlevel(mainMenu);
        }
    }
}
