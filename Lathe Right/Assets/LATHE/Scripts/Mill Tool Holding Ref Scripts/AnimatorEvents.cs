using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public static AnimatorEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    public event Action animationDone;
    public void AnimationDone()
    {
        if (animationDone != null)
        {
            animationDone();
        }
    }
}
