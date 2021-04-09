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
    public Boolean sentenceTrigger;
    private void Awake()
    {
        gameDialogues = dialogues.dialogues;
    }

    void Start(){
        if (!sentenceTrigger)
        {
            dialogHolder.SetActive(true);
            TriggerDialogue(0);
        }
        dialogHolder.SetActive(false);


    }

    public void TriggerDialogue (int i)
    {
        dialogHolder.SetActive(true);
        dialogueManager.StartDialogue(gameDialogues[i]);
        currentIndex = i;
    }

}
