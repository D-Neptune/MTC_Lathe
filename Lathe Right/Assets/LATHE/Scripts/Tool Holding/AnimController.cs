using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimController : MonoBehaviour
{
    [SerializeField] private List<ColliderTrigger> colliderTriggers;
    [SerializeField] private HintController hintController;
    [SerializeField] private TriggerDialogueInterface trigger;
    [SerializeField] private Button restartButton;
    [SerializeField] private string[] clipName;
    [SerializeField] private string resetParam;
    [SerializeField] private int dialogIndex;

    private int index = 0;
    private Animator animator;
    private List<AnimatorControllerParameter> parameters;
    private int counter = 0;
    private bool transitionDone, animDone;
    // Start is called before the first frame update
    void Awake()
    {
        restartButton.interactable = false;
        parameters = new List<AnimatorControllerParameter>();
        animator = GetComponent<Animator>();
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                parameters.Add(param);
            }
        }
        animator.enabled = false;
    }

    public void ResetHint()
    {
        hintController.AnimIndex = 0;
        hintController.Reset();
    }

    public bool PlayAnimation(string transition, string animationName)
    {
        if (index < parameters.Count)
        {
            if (index == 0)
            {
                ResetLoopParams();
            }
            bool inRightTransition = animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
            Debug.Log("Transition Resources: " + transition + " vs Transition Parameters: " + parameters[index].name);
            Debug.Log("Animation Name Resources: " + animationName + " vs Clip Name: " + clipName[index]);
            if (transition == parameters[index].name && inRightTransition && animationName == clipName[index])
            {
                animator.SetBool(transition, true);
                hintController.AnimPlayed();
                index++;
                CheckTransitionEnd();
                return true;
            }
        }
        return false;
    }

    public void CheckTransitionEnd()
    {
        if (index == parameters.Count)
        {
            transitionDone = true;
            counter++;
        }

    }


    public void ResetParams()
    {
        for (int i = 0; i < parameters.Count; i++)
        {
            animator.SetBool(parameters[i].name, false);
        }

    }

    private void ResetLoopParams()
    {
        animator.SetFloat(resetParam, 0f);
    }

    public void ResetAnim()
    {
        if (transitionDone && CurrentAnimationStatus)
        {
            animator.SetFloat(resetParam, 1f);
            ResetParams();
            index = 0;
            transitionDone = false;
            trigger.TriggerDialogue(dialogIndex);
            AnimatorEvents.current.AnimationDone();
            foreach (ColliderTrigger clipTriggers in colliderTriggers)
            {
                clipTriggers.ResetTriggers(0);
            }
            ResetHint();
        }

    }

    public int Index
    {
        get => index;
    }

    public bool Done
    {
        get => animDone;
    }

    public string ResetParam
    {
        get => resetParam;
    }

    public int Counter
    {
        get => counter;
    }

    public bool CurrentAnimationStatus
    {
        get => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
    }
}
