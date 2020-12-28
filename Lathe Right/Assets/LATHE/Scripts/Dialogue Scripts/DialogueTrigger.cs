using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogBox dialogues;
    public DialogueManager dialogueManager;
    private List<Dialogue> gameDialogues;
    private int currentIndex;
    private Boolean managerPresent;
    public Boolean sentenceTrigger;
    private void Awake()
    {
        gameDialogues = dialogues.dialogues;
    }

    void Start(){
        if (!sentenceTrigger)
        {
            TriggerDialogue(0);
        }

    }

    public void TriggerDialogue (int i)
    {
        dialogueManager.StartDialogue(gameDialogues[i]);
        currentIndex = i;
    }

}
