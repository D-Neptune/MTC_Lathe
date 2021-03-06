using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneDisplayToggle : MonoBehaviour
{
    public static Boolean tutorial;
    public static Boolean done;
    public static Boolean testMode;
    public static Boolean submitDone;
    public static Boolean adminMode;

    // Start is called before the first frame update

    public void setTutorial(Boolean tut)
    {
        tutorial = tut;
    }

    public Boolean getTutorial()
    {
        return tutorial;
    }

    public void TutorialDone()
    {
        done = true;
    }

    public Boolean Done
    {
        get => done;
    }

    public Boolean TestMode
    {
        get => testMode;
        set => testMode = value;
    }

    public Boolean SubmitDone
    {
        get => submitDone;
        set => submitDone = value;
    }

    public Boolean AdminMode
    {
        get => adminMode;
        set => adminMode = value;
    }
}
