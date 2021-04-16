using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneToggle : MonoBehaviour
{

    public GameObject Button1, Button2, Button3;
    public SceneDisplayToggle tutorial;
    void Start()
    {
        if (tutorial.getTutorial())
        {
            //Button1.SetActive(true);
            Button2.SetActive(false);
            if (Button3 != null)
            {
                Button3.SetActive(true);
            }
        }
        else
        {
            Button1.SetActive(false);
            Button2.SetActive(true);
            if (Button3 != null)
            {
                Button3.SetActive(false);
            }
        }
    }
}

