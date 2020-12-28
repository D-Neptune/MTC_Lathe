using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalInfoManager : MonoBehaviour
{
    private List<ComponentDetail> details;
    [SerializeField] private Additional_Info_Scriptable partData;

    [SerializeField] private Text namePart, info;
    [SerializeField] private Image infoImage;

    public Boolean language = true;
    private int currentIndex = -1;

    // Start is called before the first frame update
    void Awake()
    {
        details = partData.components;
    }


    public void SetDetails(int index)
    {
        if (language)
        {
            namePart.text = details[index].partName;
            info.text = details[index].sentence;

        }
        else
        {
            namePart.text = details[index].partNameFR;
            info.text = details[index].sentenceFR;
        }

        infoImage.sprite = details[index].image;
        currentIndex = index;
    }

    public void ChangeLanguage()
    {
        if (currentIndex != -1)
        {
            if (language)
            {
                namePart.text = details[currentIndex].partNameFR;
                info.text = details[currentIndex].sentenceFR;
            }
            else
            {
                namePart.text = details[currentIndex].partName;
                info.text = details[currentIndex].sentence;
            }
        }

        language = !language;
    }
}

