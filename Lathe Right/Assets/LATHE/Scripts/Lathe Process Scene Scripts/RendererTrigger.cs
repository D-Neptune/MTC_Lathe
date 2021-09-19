using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;


public class RendererTrigger : MonoBehaviour
{
    [SerializeField] private DialogueOperator dialogueManager;
    [SerializeField] private Triggers animTriggers;
    [SerializeField] private ProcessController manager;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private bool colorChange;
    [SerializeField] private GameObject activateOnClick;
    [SerializeField] public bool powerTrigger;
    [HideInInspector] [SerializeField] public PowerToggle powerToggle;
    [HideInInspector] [SerializeField] public bool power;
    private List<Trigger> triggers;
    private Color basicColor;


    // Start is called before the first frame update
    void Awake()
    {
        if (colorChange)
        {
            basicColor = GetComponent<Renderer>().material.color;
            if (activateOnClick != null)
            {
                activateOnClick.SetActive(false);
            }
        }
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
                if (controller.CurrentAnimationStatus)
                {
                    if (colorChange)
                    {
                        GetComponent<Renderer>().material.color = clickedColor;
                        if (activateOnClick != null)
                        {
                            //activateOnClick.SetActive(true);
                            gameObject.SetActive(false);
                        }
                    }
                }
                bool val = tmpTrigger.PlaySequence(controller);

                if (val)
                {
                    if (powerTrigger)
                    {
                        powerToggle.TogglePower(power);
                    }

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
        if (EventSystem.current.IsPointerOverGameObject()) return;
        PlaySequence();
    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        OperationManager controller = manager.Operation;
        if (controller != null)
        {
            foreach (Trigger trigger in triggers)
            {
                if (trigger.Anim == manager.Index && trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
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
        ResetStock();
    }

    public void ResetStock()
    {
        if (activateOnClick != null)
        {
            activateOnClick.SetActive(false);
            GetComponent<Renderer>().material.color = basicColor;

        }
    }

    public GameObject ActivateOnClick
    {
        get => activateOnClick;
        set => activateOnClick = value;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RendererTrigger))]
public class RendererTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        RendererTrigger script = (RendererTrigger) target;

        // draw checkbox for the bool
        if (script.powerTrigger) // if bool is true, show other fields
        {
            script.powerToggle = EditorGUILayout.ObjectField("Power Toggle", script.powerToggle, typeof(PowerToggle), true) as PowerToggle;
            script.power = EditorGUILayout.Toggle("ON/OFF", script.power);
        }
    }
}
#endif

