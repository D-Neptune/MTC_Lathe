using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlAnimMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float startingAnimTime;
    [SerializeField] private float animationSpeed;

    private Boolean btnEnabled, scrollActive, arrowActive, paused;


    // Start is called before the first frame update
    void Start()
    {
        ResetAnimation();
    }

    private void LateUpdate()
    {
        if(btnEnabled && !arrowActive)
        {
            if (Input.mouseScrollDelta.y > 0f && AnimTime < 1f)
            {
                MoveAnimation(1);
                scrollActive = true;
            }
            else if (Input.mouseScrollDelta.y < 0f && AnimTime > 0)
            {
                MoveAnimation(-1);
                scrollActive = true;
            }
            else
            {
                if (!paused)
                {
                    MoveAnimation(0);
                }
                scrollActive = false;
            }

        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if(btnEnabled && !scrollActive)
        {
            if(e.type == EventType.KeyDown && e.keyCode == KeyCode.UpArrow && AnimTime < 1f)
            {
                MoveAnimation(1);
                arrowActive = true;
            }
            else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.DownArrow && AnimTime > 0)
            {
                MoveAnimation(-1);
                arrowActive = true;
            }
            else
            {
                if (!paused)
                {
                    MoveAnimation(0);
                }
                arrowActive = false;
            }
        }
    }

    private void MoveAnimation(int orientation)
    {

        if(orientation > 0)
        {
            animator.SetFloat("Orientation", 1);
            SetSpeed(animationSpeed);
            paused = false;
        }
        else if (orientation < 0)
        {
            animator.SetFloat("Orientation", -1);
            SetSpeed(animationSpeed);
            paused = false;
        }
        else
        {
            PauseAnimation();
        }
    }


    public void ResetAnimation()
    {
        animator.Play(animator.runtimeAnimatorController.animationClips[0].name, 0, startingAnimTime);
        PauseAnimation();
    }


    public float AnimTime
    {
        get => animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    private void PauseAnimation()
    {
        SetSpeed(0);
        paused = true;
    }

    private void SetSpeed(float speed)
    {
        animator.speed = speed;
    }

    public Boolean hasNotStarted
    {
        get => (AnimTime == startingAnimTime);
    }

    public void ToggleEnable()
    {
        if (Enabled)
        {
            Enabled = false;
        }
        else
        {
            Enabled = true;
        }
    }

    public Boolean Enabled
    {
        get => btnEnabled;
        set => btnEnabled = value;
    }
}
