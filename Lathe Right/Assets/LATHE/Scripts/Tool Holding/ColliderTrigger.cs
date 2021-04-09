using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField] private AnimController controller;
    [SerializeField] private DialogueOperator dialogueManager;
    [SerializeField] private Triggers animTriggers;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private bool colorChange;

    private List<Trigger> triggers;
    private Color basicColor;


    // Start is called before the first frame update
    void Awake()
    {
        if (colorChange)
        {
            basicColor = GetComponent<Renderer>().material.color;
        }
        triggers = animTriggers.triggers;
        ResetTriggers(-1);
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        Trigger tmpTrigger = null;
        if (controller != null)
        {
            foreach (Trigger trigger in triggers)
            {
                if (trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
                {
                    tmpTrigger = trigger;
                    break;
                }
            }
            if (tmpTrigger != null)
            {
                if (controller.CurrentAnimationStatus)
                {
                    if (colorChange)
                    {
                        GetComponent<Renderer>().material.color = clickedColor;
                    }
                }
                bool val = tmpTrigger.PlaySequence(controller);

                if (val)
                {
                    if (dialogueManager.SentenceIndex == tmpTrigger.CurrentSentenceIndex())
                    {
                        dialogueManager.DisplayNextSentence();
                    }
                    GetComponent<Renderer>().material.color = basicColor;

                }
            }
        }
    }

    private void OnMouseDown()
    {
        PlaySequence();
    }

    private void OnMouseOver()
    {
        if (controller != null)
        {
            foreach (Trigger trigger in triggers)
            {
                if (trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
                {
                    GetComponent<Renderer>().material.color = hoverColor;
                    break;
                }
            }
        }

    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = basicColor;
    }

    public void ResetTriggers(int anim)
    {
        foreach (Trigger trigger in triggers)
        {
            if (anim < 0)
            {
                trigger.Index = 0;
            }
            else if (trigger.Anim == anim)
            {
                trigger.Index = 0;
            }

        }
    }
}

