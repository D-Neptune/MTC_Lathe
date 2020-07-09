﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class QuillFeedControl : MonoBehaviour
{

    float MAX_HEIGHT;
    float MIN_HEIGHT;

    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField] DRO_ButtonState QuillLockButton; // TEMPORARY --> THIS SHOULD BE FOR QUILL FEED ONLY

    [SerializeField]
    GameObject wheel, lockHandle;

    [SerializeField]
    Boolean enable = true;

    public Boolean collided;

    public float movementInterval = 0.005f;

    

    Boolean animated = true;
    Boolean handle_enabled, wheel_spin;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Start()
    {
        collided = false; 
        object_anim = animObject.GetComponent<Animator>();
        lock_anim = lockAnimObject.GetComponent<Animator>();

        MIN_HEIGHT = wheel.transform.localPosition.y - 0.3f;
        MAX_HEIGHT = wheel.transform.localPosition.y;


        setSpeed(0.2f);
        setLockSpeed(0.5f);
        pause();
        pauseLock();
        //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

        handle_enabled = true;
        wheel_spin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(QuillLockButton.checkIfEnabled == true)
        {

            if (Input.mouseScrollDelta.y > 0f && !collided)
            {
                Debug.LogWarning("Scroll Up");

                Vector3 tmp_pos = wheel.transform.localPosition;
                float y_pos = tmp_pos.y - movementInterval;

                if (y_pos < MAX_HEIGHT && y_pos > MIN_HEIGHT)
                {

                    Vector3 new_pos = new Vector3(tmp_pos.x, y_pos, tmp_pos.z);
                    wheel.transform.localPosition = new_pos;
                    object_anim.SetFloat("Reverse", 1);
                    setSpeed(2f);
                }
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {

                Debug.LogWarning("Scroll Down");


                Vector3 tmp_pos = wheel.transform.localPosition;
                float y_pos = tmp_pos.y + movementInterval;

                if (y_pos < MAX_HEIGHT && y_pos > MIN_HEIGHT)
                {
                    Vector3 new_pos = new Vector3(tmp_pos.x, y_pos, tmp_pos.z);

                    wheel.transform.localPosition = new_pos;
                    object_anim.SetFloat("Reverse", -1);
                    setSpeed(2f);
                }
            } else
            {
                Debug.LogWarning("Nothing");

                pause();
            }

        }
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

    private void setSpeed(float mph)
    {
        Debug.Log(mph);
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
}