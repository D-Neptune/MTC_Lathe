using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueInterface : MonoBehaviour
{
    public DialogBox dialogues;
    public InteractionManager manager;
    public DialogueOperator dialogueManager;
    private List<Dialogue> gameDialogues;
    private int currentIndex;
    private bool managerPresent;
    public bool SentenceTrigger;
    private void Awake()
    {
        gameDialogues = dialogues.dialogues;
    }

    void Start()
    {
        managerPresent = manager != null;
        if (!SentenceTrigger)
        {
            TriggerDialogue(0);
        }

    }

    public void TriggerDialogue(int i)
    {
        if (managerPresent)
        {
            manager.SetInteractionLevel(gameDialogues[i].name);
        }
        dialogueManager.StartDialogue(gameDialogues[i]);
        currentIndex = i;
    }

    public void TransitionDialogue()
    {
        if (managerPresent)
        {
            manager.Transition(gameDialogues[currentIndex].name);
        }
    }

    public void InteractButton()
    {
        if (managerPresent)
        {
            manager.InteractButton(gameDialogues[currentIndex].name);
        }
    }

}
