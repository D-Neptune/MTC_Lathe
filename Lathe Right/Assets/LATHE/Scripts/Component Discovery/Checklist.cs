using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checklist : MonoBehaviour
{
    // Start is called before the first frame update
    private List<ComponentDetail> details;
    private Toggle[] checklist;
    private Boolean language = true;

    public void SetupToggleList(List<ComponentDetail> det)
    {
        details = det;
        checklist = gameObject.GetComponentsInChildren<Toggle>();
    }

    public void LanguageSwitch()
    {

        for (int i = 0; i < checklist.Length; i++)
        {
            if (language)
            {
                checklist[i].GetComponentInChildren<Text>().text = details[i].partNameFR;
            }
            else
            {
                checklist[i].GetComponentInChildren<Text>().text = details[i].partName;
            }
        }

        language = !language;
    }
}
