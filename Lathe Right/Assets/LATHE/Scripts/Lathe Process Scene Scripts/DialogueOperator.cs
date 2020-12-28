using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class DialogueOperator : MonoBehaviour
{
    [SerializeField] private ProcessController animationController;
    [SerializeField] private Button backButton;
    private int counter;
    public GameObject DeactivateAtEndDialogue;
    public TriggerDialogueInterface trigger;
    public Text nameText;
    public Text dialogueText;
    public Text dialogIndex;
    [SerializeField] private Image dialogImage;
    public bool language = true;

    private Sprite[] images;
    public int tracker, count;
    public int index, sentenceIndex;
    private string[] sentences, sentencesFR, currentLangSentence;
    private string lastSentence;
    private string titleLang;
    private bool controllerPresent;
    [SerializeField] private GameObject buttonOC;
    [SerializeField] private GameObject open, close, holder;
    [SerializeField] public bool MachiningWait;
    [SerializeField] public List<int> indexWait;
    [SerializeField] private VideoOperator videoOperator;
    [SerializeField] private int indexVideo, OperationIndexVideo;


    void Awake()
    {
        controllerPresent = animationController != null;
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
        sentenceIndex--;
        tracker++;

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

        if (tracker + 1 >= count)
        {
            backButton.interactable = false;
        }
        else
        {
            backButton.interactable = true;
        }
    }

    public void DialogWait()
    {
        if (open != null) { open.SetActive(false); }
        if (close != null) { close.SetActive(true); }
        if (buttonOC != null) { buttonOC.SetActive(true); }
        holder.SetActive(true);
    }

    public void DisplayNextSentence()
    {
        if (MachiningWait)
        {
            if(indexWait[animationController.Index] == sentenceIndex+1)
            {
                if (open != null) { open.SetActive(false); }
                if (close != null) { close.SetActive(false); }
                if (buttonOC != null) { buttonOC.SetActive(false); }
                holder.SetActive(false);
                
            }
            else
            {
                if (open != null) { open.SetActive(false); }
                if (close != null) { close.SetActive(true); }
                holder.SetActive(true);
            }
        }
        else
        {
            if (open != null)
            {
                open.SetActive(false);
            }
            if (close != null)
            {
                close.SetActive(true);
            }
            holder.SetActive(true);

        }
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
        }
        else
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
        if(videoOperator != null)
        {
            if(animationController.Index == OperationIndexVideo)
            {
                videoOperator.PlayVideoClip(indexVideo);
            }
        }
        if (controllerPresent)
        {
            OperationManager operation = animationController.Operation;
            if (operation != null)
            {
                if (!animationController.Operation.Done && animationController.Operation.Active)
                {
                    trigger.InteractButton();
                }
                else if (animationController.Operation.Done || (counter == 0 && !animationController.Operation.Active))
                {
                    trigger.TransitionDialogue();
                    counter++;
                }
            }
            else
            {
                if(counter == 0)
                {
                    trigger.TransitionDialogue();
                    counter++;
                }
            }

        }
    }

    public void SkipDialogue()
    {
        if (animationController.Operation.Done && animationController.Operation.Active)
        {
            DeactivateAtEndDialogue.SetActive(false);
            trigger.TransitionDialogue();
            counter++;
        }
    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.N.ToString())) && controllerPresent)
        {
            SkipDialogue();
        }
    }

    public void switchLang()
    {
        if (language)
        {
            currentLangSentence = sentencesFR;
        }
        else
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


