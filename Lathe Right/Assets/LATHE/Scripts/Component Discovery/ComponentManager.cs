using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentManager : MonoBehaviour
{
    private List<ComponentDetail> details;
    [SerializeField] private AnchorSwitcher anchorSwitcher;
    [SerializeField] private PartDataScriptable partData;
    [SerializeField] private GameObject continueBTN, menuBTN;
    [SerializeField] private Text namePart, info, partsExamined, totalNumOfParts;
    [SerializeField] private Image infoImage;
    [SerializeField] private string initialTextFR, namePartFR;
    [SerializeField] private Checklist checklist;
    [SerializeField] private VideoOperator videoOperator;
    [SerializeField] private int IndexVideo;
    private string initialText, initialName;

    public Boolean language = true;
    private int currentIndex = -1;
    private int counter;
    private int numOfParts;

    private string partsExam = "Number of parts examined: ";
    private string partsExamFR = "Nombre de pièces examinées: ";
    private string totalParts = "Total number of parts: ";
    private string totalPartsFR = "Nombre totale de pièces: ";
    public SceneDisplayToggle toggle;

    // Start is called before the first frame update
    void Awake()
    {
        initialText = info.text;
        initialName = namePart.text;
        details = partData.components;
        counter = 0;
        numOfParts = details.Count;
        totalNumOfParts.text = totalParts + numOfParts.ToString();
        checklist.SetupToggleList(details);
        updateCounter();
        continueBTN.SetActive(false);
        //Debug.Log(details.Count);
    }


    public void SetDetails(int index)
    {
        if (language)
        {
            namePart.text = details[index].partName;
            info.text = details[index].sentence;

        } else
        {
            namePart.text = details[index].partNameFR;
            info.text = details[index].sentenceFR;
        }

        infoImage.sprite = details[index].image;
        currentIndex = index;
    }

    public void ChangeLanguage()
    {

        if (counter == 0)
        {
            toggleInitialText();
        }

        else if (language && currentIndex > 0)
        {

            namePart.text = details[currentIndex].partNameFR;
            info.text = details[currentIndex].sentenceFR;
            totalNumOfParts.text = totalPartsFR + numOfParts.ToString();
            partsExamined.text = partsExamFR + counter.ToString();

        }
        else if (currentIndex > 0)
        {
            namePart.text = details[currentIndex].partName;
            info.text = details[currentIndex].sentence;
            totalNumOfParts.text = totalParts + numOfParts.ToString();
            partsExamined.text = partsExam + counter.ToString();

        }
        anchorSwitcher.SwitchAnchors(language);
        checklist.LanguageSwitch();
        language = !language;
    }

    private void toggleInitialText()
    {
        if (counter == 0)
        {
            if (language)
            {
                info.text = initialTextFR;
                namePart.text = namePartFR;
                totalNumOfParts.text = totalPartsFR + numOfParts.ToString();
                partsExamined.text = partsExamFR + counter.ToString();
            }
            else
            {
                info.text = initialText;
                namePart.text = initialName;
                totalNumOfParts.text = totalParts + numOfParts.ToString();
                partsExamined.text = partsExam + counter.ToString();
            }
        }

    }

    public void incrementPartCounter()
    {
        counter++;
        updateCounter();
        if(counter == numOfParts)
        {
            if (toggle != null)
            {
                if (toggle.getTutorial())
                {
                    continueBTN.SetActive(true);
                }
            }
            else
            {
                menuBTN.SetActive(true);
            }
            if(videoOperator != null)
            {
                //videoOperator.PlayVideoClip(IndexVideo);
            }
        }
    }

    public void updateCounter()
    {
        if (language)
        {
            partsExamined.text = partsExam + counter.ToString();
        }
        else
        {
            partsExamined.text = partsExamFR + counter.ToString();

        }
    }
}
