using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] public Button ON, MainMenu, Next, Facing, Turning, Drill, ToolPost, Tailstock;
    [SerializeField] public List<Button> controls;
    [SerializeField] public GameObject TailstockPanel, ToolPostPanel;
    [SerializeField] public string intro, facing, turning, drilling;
    private bool SequenceDone;
    private anim currentAnim;
    void Awake()
    {
        //NoInteraction();
    }

    public anim CurrentAnim
    {
        get => currentAnim;
    }

    public void SetupAnims(anim savedAnim)
    {
        if (savedAnim == anim.Facing)
        {
            Transition(intro);
        }
        else if (savedAnim == anim.Turning)
        {
            Transition(facing);
        }
        else if (savedAnim == anim.Drilling)
        {
            Transition(turning);
        }
        else
        {
            Transition(drilling);
        }
    }

    public void Transition(string dialogueName)
    {
        Debug.Log("LOADING");
        NoInteraction();
        if (SequenceDone)
        {
            Drill.interactable = true;
            Turning.interactable = true;
            Facing.interactable = true;
            MainMenu.interactable = true;
            Next.interactable = true;
        }
        else if (dialogueName.Equals(intro))
        {
            Facing.interactable = true;
            currentAnim = anim.Facing;
        }

        else if (dialogueName.Equals(facing))
        {
            Turning.interactable = true;
            currentAnim = anim.Turning;

        }
        else if (dialogueName.Equals(turning))
        {
            Drill.interactable = true;
            currentAnim = anim.Drilling;

        }
        else if (dialogueName.Equals(drilling))
        {
            currentAnim = anim.Complete;
            Drill.interactable = true;
            Turning.interactable = true;
            Facing.interactable = true;
            Next.interactable = true;
            SequenceDone = true;

        }
    }



    // Update is called once per frame
    public void SetInteractionLevel(string dialogueName)
    {
        NoInteraction();

        if (dialogueName.Equals(facing))
        {
            ON.interactable = true;
            ToolPost.interactable = false;
            Tailstock.interactable = false;
            ControlState(true);

        }
        else if (dialogueName.Equals(turning))
        {
            ON.interactable = true;
            ToolPost.interactable = false;
            Tailstock.interactable = false;
            ControlState(true);

        }
        else if (dialogueName.Equals(drilling))
        {
            ON.interactable = true;
            ToolPost.interactable = true;
            Tailstock.interactable = true;
            ControlState(true);
        }

    }
    public void InteractButton(string dialogueName)
    {
        if (dialogueName.Equals(drilling))
        {
            Drill.interactable = true;
        }
        else if (dialogueName.Equals(facing))
        {
            Facing.interactable = true;
        }
        else if (dialogueName.Equals(turning))
        {
            Turning.interactable = true;
        }
    }
    void NoInteraction()
    {
        ToolPost.interactable = false;
        Tailstock.interactable = false;
        ON.interactable = false;
        Next.interactable = false;
        Turning.interactable = false;
        Facing.interactable = false;
        Drill.interactable = false;
        TailstockPanel.SetActive(false);
        ToolPostPanel.SetActive(false);
        ControlState(false);
    }

    private void ControlState(bool val)
    {
        foreach(Button button in controls)
        {
            button.interactable = val;
        }
    }
}

public enum anim { Facing, Turning, Drilling, Complete, NA}


