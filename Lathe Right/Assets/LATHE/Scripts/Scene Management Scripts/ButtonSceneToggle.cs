using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneToggle : MonoBehaviour
{

    public GameObject Button1, Button2;
    public SceneDisplayToggle tutorial;
    void Start()
    {
        if (tutorial.getTutorial())
        {
            Button1.SetActive(true);
            Button2.SetActive(false);
        }
        else
        {
            Button1.SetActive(false);
            Button2.SetActive(true);
        }
    }
}

