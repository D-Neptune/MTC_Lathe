using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private int counter;
    public GameObject DeactivateAtEndDialogue;
    public DialogueTrigger trigger;
    public Text nameText;
    public Text dialogueText;
    public Text dialogIndex;
    [SerializeField] private Image dialogImage;
    public Boolean language = true;

    private Sprite[] images;
    public int tracker, count;
    public int index, sentenceIndex;
    private string[] sentences, sentencesFR, currentLangSentence;
    private string lastSentence;
    private string titleLang;
    private Boolean controllerPresent;
    [SerializeField] private GameObject open, close, holder;

    void Awake() {
        counter = 0;

        index = 0;
        sentenceIndex = -1;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        titleLang = dialogue.frenchName;
        sentenceIndex = -1;
        DeactivateAtEndDialogue.SetActive(true);
        backButton.interactable = false;

        if (language)
        {
            nameText.text = dialogue.name;
            titleLang = dialogue.frenchName;
        }
        else
        {
            nameText.text = dialogue.frenchName;
            titleLang = dialogue.name;
        }

        sentences = dialogue.sentences;
        sentencesFR = dialogue.sentencesFR;

        tracker = count = sentences.Length;
       
        images = dialogue.images;

        if (language)
        {
            currentLangSentence = sentences;
        }
        else
        {
            currentLangSentence = sentencesFR;
        }

        DisplayNextSentence();
    }

    public void DisplayPreviousSentece()
    {
        sentenceIndex-- ;
        tracker ++;

        dialogIndex.text = (sentenceIndex + 1) + "/" + count;
        dialogImage.sprite = images[sentenceIndex];

        string sentence = currentLangSentence[sentenceIndex];
        if (language)
        {
            lastSentence = sentencesFR[sentenceIndex];
        }
        else
        {
            lastSentence = sentences[sentenceIndex];
        }

        dialogueText.text = sentence;

        if (tracker+1 >= count)
        {
            backButton.interactable = false;
        }
        else
        {
            backButton.interactable = true;
        }
    }

    public void DisplayNextSentence()
    {
        if(open != null)
        {
            open.SetActive(false);
        }
        if(close != null)
        {
            close.SetActive(true);
        }
        holder.SetActive(true);
        sentenceIndex++;
        if (tracker < count)
        {
            backButton.interactable = true;
        }
        if (tracker == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = currentLangSentence[sentenceIndex];
        if (language)
        {
            lastSentence = sentencesFR[sentenceIndex];
        } else
        {
            lastSentence = sentences[sentenceIndex];
        }

        dialogueText.text = sentence;
        dialogIndex.text = (sentenceIndex + 1) + "/" + count;
        dialogImage.sprite = images[sentenceIndex];
        tracker--;


    }

    void EndDialogue()
    {
        DeactivateAtEndDialogue.SetActive(false);
    }

    public void switchLang() {
        if (language)
        {
            currentLangSentence = sentencesFR;
        } else
        {
            currentLangSentence = sentences;
        }

        string tmp = nameText.text;
        nameText.text = titleLang;
        titleLang = tmp;

        string tmp2 = dialogueText.text;
        dialogueText.text = lastSentence;
        lastSentence = tmp2;

        language = !language;
    }

    public int SentenceIndex
    {
        get => sentenceIndex;
    }

}
