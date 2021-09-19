using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dialogHolder;
    public DialogBox dialogues;
    public DialogueManager dialogueManager;
    private List<Dialogue> gameDialogues;
    private int currentIndex;
    private Boolean managerPresent;
    [SerializeField ]public bool sentencetrigger;
    private void Awake()
    {
        gameDialogues = dialogues.dialogues;
    }

    void Start(){
        if (!sentenceTrigger)
        {
            dialogHolder.SetActive(true);
            TriggerDialogue(0);
            return;
        }
        dialogHolder.SetActive(false);


    }

    public void TriggerDialogue (int i)
    {
        dialogHolder.SetActive(true);
        dialogueManager.StartDialogue(gameDialogues[i]);
        currentIndex = i;
    }

    public bool sentenceTrigger
    {
        get => sentencetrigger;
        set => sentencetrigger = value;
    }

}
