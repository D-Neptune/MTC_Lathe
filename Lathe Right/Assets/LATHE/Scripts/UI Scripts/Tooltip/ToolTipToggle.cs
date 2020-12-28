using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipToggle : MonoBehaviour
{
    public string name;
    public string nameFR;

    public string getName(Boolean lang)
    {
        if (lang)
        {
            return name;
        }
        else
        {
            return nameFR;
        }
    }

}
