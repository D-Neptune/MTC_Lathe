﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZWheelControl : MonoBehaviour
{
    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField] DRO_Button ZLockButton; // TEMPORARY --> THIS SHOULD BE FOR QUILL FEED ONLY

    [SerializeField]
    GameObject wheel, lockHandle;

    [SerializeField]
    Boolean enable = true;

    [SerializeField] private float speedMultiplier, arrowSpeed;

    private PlacePiece placePiece;
    private ClampPiece clampPiece;

    [SerializeField] private string lockBool, unlockBool;


    Boolean animated = true;
    Boolean handle_enabled, wheel_spin, locked, scrollActive, keyActive;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Awake()
    {
        object_anim = animObject.GetComponent<Animator>();
        lock_anim = lockAnimObject.GetComponent<Animator>();
        locked = false;
        setSpeed(0.2f);
        pause();
        pauseLock();

        handle_enabled = true;
        wheel_spin = true;
    }


    public Boolean Locked
    {
        get => locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (enable && !Input.GetKey(KeyCode.LeftShift) && !keyActive) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if ((ZLockButton.Activated == true) && !locked)
            {

                if (Input.mouseScrollDelta.y > 0f && animTime < 1f)
                {
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        object_anim.SetFloat("Reverse", 1);
                        setSpeed(arrowSpeed);
                    }
                }
                else if (Input.mouseScrollDelta.y < 0f && animTime > 0)
                {
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f )|| (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        object_anim.SetFloat("Reverse", -1);
                        setSpeed(arrowSpeed);
                    }
                } else
                {
                    pause();
                    scrollActive = false;
                }
            }
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (enable && !scrollActive) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if ((ZLockButton.Activated == true) && !locked)
            {

                if (e.type == EventType.KeyDown && e.keyCode == KeyCode.DownArrow && animTime < 1f)
                {
                    keyActive = true;
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        object_anim.SetFloat("Reverse", 1);
                        setSpeed(arrowSpeed);
                    }
                }
                else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.UpArrow && animTime > 0)
                {
                    keyActive = true;
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        object_anim.SetFloat("Reverse", -1);
                        setSpeed(arrowSpeed);
                    }
                }
                else
                {
                    pause();
                    keyActive = false;
                }
            }
        }
    }

    private void StopMovement()
    {
        pause();
        WarningEvents.current.CutterNear();
    }

    private void pause()
    {
        object_anim.speed = 0;
        animated = false;
    }

    private void pauseLock()
    {
        lock_anim.speed = 0;
    }

    public void ToggleLock(float speed)
    {
        if (locked)
        {
            lock_anim.SetBool(lockBool, false);
            lock_anim.SetBool(unlockBool, true);
        }
        else
        {
            lock_anim.SetBool(lockBool, true);
            lock_anim.SetBool(unlockBool, false);
        }
        locked = !locked;
        setLockSpeed(speed);
    }

    private void setSpeed(float mph)
    {
        object_anim.speed = mph;
        if (mph > 0)
        {
            animated = true;
        }
    }

    private void setLockSpeed(float mph)
    {
        lock_anim.speed = mph;
    }

    public void resetAnim(float time)
    {
        object_anim.Play(object_anim.runtimeAnimatorController.animationClips[0].name, 0, time);
        lock_anim.Play(lock_anim.runtimeAnimatorController.animationClips[0].name, 0, time);
        object_anim.speed = 0;
        lock_anim.speed = 0;
        lock_anim.SetBool(lockBool, true);
        lock_anim.SetBool(unlockBool, false);
        locked = false;
    }

    public float animTime
    {
        get => object_anim.GetCurrentAnimatorStateInfo(0).normalizedTime * object_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public PlacePiece Place
    {
        get => placePiece;
        set => placePiece = value;
    }

    public ClampPiece Clamp
    {
        get => clampPiece;
        set => clampPiece = value;
    }
}