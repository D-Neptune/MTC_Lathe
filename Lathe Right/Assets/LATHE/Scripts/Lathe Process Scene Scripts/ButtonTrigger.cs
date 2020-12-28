using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private DialogueOperator dialogueManager;
    [SerializeField] private Triggers animTriggers;
    [SerializeField] private ProcessController manager;
    [SerializeField] public bool toggleImage, colorChange;
    [HideInInspector] [SerializeField] public ImageToggle imageToggle;
    private List<Trigger> triggers;
    

    // Start is called before the first frame update
    void Awake()
    {
        triggers = animTriggers.triggers;
        ResetTriggers(-1);
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        Trigger tmpTrigger = null;
        OperationManager controller = manager.Operation;
        if (controller != null)
        {
            foreach (Trigger trigger in triggers)
            {
                if (trigger.Anim == manager.Index && trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
                {
                    tmpTrigger = trigger;
                    break;
                }
            }
            if (tmpTrigger != null)
            {
                bool val = tmpTrigger.PlaySequence(controller);
                if (val)
                {
                    if (toggleImage)
                    {
                        imageToggle.toggle();
                    }
                    if (dialogueManager.SentenceIndex == tmpTrigger.CurrentSentenceIndex())
                    {
                        dialogueManager.DisplayNextSentence();
                    }
                }
            }
        }
    }


    private void OnMouseDown()
    {
        PlaySequence();
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

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonTrigger))]
public class ButtonTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        ButtonTrigger script = (ButtonTrigger) target;

        // draw checkbox for the bool
        if (script.toggleImage) // if bool is true, show other fields
        {
            script.imageToggle = EditorGUILayout.ObjectField("Image Toggle", script.imageToggle, typeof(ImageToggle), true) as ImageToggle;
        }
    }
}
#endif

