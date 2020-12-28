using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSceneToggle : MonoBehaviour
{
    public static Boolean language;
    public static string fullName;
    public static string studentNumber;
    // Start is called before the first frame update

    public void setLanguage(Boolean lang)
    {
        language = lang;
    }

    public Boolean getLanguage()
    {
        return language;
    }

    public void setName(string fullname)
    {
        fullName = fullname;
    }

    public void setNumber(string number)
    {
        studentNumber = number;
    }

    public string Name
    {
        get => fullName;
    }

    public string Number
    {
        get => studentNumber;
    }


}
