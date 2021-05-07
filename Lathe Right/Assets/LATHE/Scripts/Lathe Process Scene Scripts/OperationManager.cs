using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OperationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int animIndex;
    [SerializeField] private string OnAnimationName, OffAnimationName, drillTag, stopTag, initialClip;
    [SerializeField] private GameObject cutter, dummyCutter, holder;
    [SerializeField] private HintTrigger triggerFlash;
    [SerializeField] private string[] clipName;
    [SerializeField] private string resetParam, restartParam;
    [SerializeField] private List<ButtonTrigger> buttonTriggers;
    [SerializeField] private List<RendererTrigger> rendererTriggers;
    public List<AnimatorControllerParameter> parameters;
    public bool transitionDone, animDone;
    public int m_counter, m_index = 0;
    public bool isActive = false;
    private AnimationEvent eventSys1, eventSys2, eventSys3;
    [SerializeField] public bool addCutters;
    [HideInInspector] [SerializeField] public GameObject cutter2, dummyCutter2;
     [SerializeField] public string OnAnimationName2, OffAnimationName2, ContinueAnimatioName;

    private int addCutterCounter = 0;

    void Awake()
    {
        eventSys1 = new AnimationEvent();
        eventSys1.functionName = "TurnOn";
        eventSys2 = new AnimationEvent();
        eventSys2.functionName = "TurnOFF";        
        eventSys3 = new AnimationEvent();
        eventSys3.functionName = "ContinueAnim";
        parameters = new List<AnimatorControllerParameter>();
        
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                parameters.Add(param);
            }
        }

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == OnAnimationName)
            {
                clip.AddEvent(eventSys1);
            }
            else if (clip.name == OffAnimationName)
            {
                clip.AddEvent(eventSys2);
            }
            if (addCutters)
            {
                if(clip.name == OnAnimationName2)
                {
                    clip.AddEvent(eventSys1);
                }
                else if (clip.name == OffAnimationName2)
                {
                    clip.AddEvent(eventSys2);
                }
                else if (clip.name == ContinueAnimatioName)
                {
                    clip.AddEvent(eventSys3);
                }

            }
        }
        addCutterCounter = 0;
    }

    public void ResetAnim(bool prevState, OperationManager prevOperationManager)
    {
        SetAnimState(false);
        ResetParams();

        if (prevState)
        {
            prevOperationManager.ResetAnim();
        }
        if (m_counter > 0)
        {
            foreach (ButtonTrigger clipTriggers in buttonTriggers)
            {
                clipTriggers.ResetTriggers(animIndex);
            }            
            foreach (RendererTrigger clipTriggers in rendererTriggers)
            {
                if (!clipTriggers.gameObject.activeSelf)
                {
                    clipTriggers.gameObject.SetActive(true);
                }
                clipTriggers.ResetTriggers(animIndex);
            }
            RestartAnim();
        }
        ActivateAnimator(true);
        m_counter++;
    }

    public void ActivateAnimator(bool state)
    {
        animator.Rebind();
        animator.enabled = state;
        holder.SetActive(state);
        cutter.SetActive(state);
        dummyCutter.SetActive(state);
        isActive = state;
        if (addCutters)
        {
            cutter2.SetActive(state);
            dummyCutter2.SetActive(state);
        }
    }

    public void StartAnimation()
    {

        animator.SetBool(parameters[0].name, true);
    }

    public bool PlayAnimation(string transition, string animationName)
    {

        if (m_index == 0 && isActive && transition == parameters[m_index].name)
        {
            ResetLoopParams();
            StartAnimation();
            m_index++;
            triggerFlash.StopRoutine();
            triggerFlash.AnimPlayed();
            return true;
        }


        if (m_index < parameters.Count && isActive)
        {
            bool inRightTransition = animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
            if (transition == parameters[m_index].name && inRightTransition && animationName == clipName[m_index])
            {
                animator.SetBool(transition, true);

                m_index++;
                CheckTransitionEnd();
                triggerFlash.StopRoutine();
                triggerFlash.AnimPlayed();
                return true;
            }
        }
        return false;
    }

    public void ResetParams()
    {
        for (int i = 0; i < parameters.Count; i++)
        {
            animator.SetBool(parameters[i].name, false);
        }
        m_index = 0;

    }

    public void CheckTransitionEnd()
    {
        if ((m_index == parameters.Count) && isActive)
        {
            transitionDone = true;
        }
    }

    public void TurnOn()
    {
        dummyCutter.tag = drillTag;
        if (addCutters)
        {
            dummyCutter2.tag = drillTag;

        }
        animator.SetFloat("TurnOFF", 1);
    }

    public void TurnOFF()
    {
        dummyCutter.tag = stopTag;
        if (addCutters)
        {
            dummyCutter2.tag = stopTag;

            if (addCutterCounter > 0)
            {
                isActive = false;
                SetAnimState(true);

            }

            addCutterCounter++;
            Debug.Log("Add Cutter Counter: " + addCutterCounter);

        }
        else
        {
            isActive = false;
            SetAnimState(true);


        }
        animator.SetFloat("TurnOFF", 0);

    }

    public void ContinueAnim()
    {
        TurnOn();
        animator.SetFloat("Rotational Speed", 0.000000000000000000001f);
    }

    public void SetAnimSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void SetAnimState(bool state)
    {
        animDone = state;
        transitionDone = state;
    }

    public Animator Animation
    {
        get => animator;
    }

    private void ResetLoopParams()
    {
        animator.SetFloat(resetParam, 0f);
        animator.SetFloat(restartParam, 0f);
    }

    public void ResetAnim()
    {
        animator.SetFloat(resetParam, 1f);

    }

    public void RestartAnim()
    {
        addCutterCounter = 0;
        animator.SetFloat(restartParam, 1f);

    }

    public int Index
    {
        get => m_index;
    }

    public bool Active
    {
        get => isActive;
    }

    public bool Done
    {
        get => animDone;
    }

    public string ResetParam
    {
        get => resetParam;
    }

    public string RestartParam
    {
        get => restartParam;
    }

    public bool CurrentAnimationStatus
    {
        get => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
    }


}


#if UNITY_EDITOR
[CustomEditor(typeof(OperationManager))]
public class OperationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        OperationManager script = (OperationManager)target;

        // draw checkbox for the bool
        if (script.addCutters) // if bool is true, show other fields
        {
            script.cutter2 = EditorGUILayout.ObjectField("Cutter 2", script.cutter2, typeof(GameObject), true) as GameObject;
            script.dummyCutter2 = EditorGUILayout.ObjectField("Dummy Cutter 2", script.dummyCutter2, typeof(GameObject), true) as GameObject;

        }
    }
}
#endif

