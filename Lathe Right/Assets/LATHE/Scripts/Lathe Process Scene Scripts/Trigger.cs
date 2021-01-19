using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trigger
{
    public string TriggerControllerName;
    public int anim;
    public string[] transitionParams;
    public string[] animationName;
    public int[] index;
    public int[] sentenceIndex;

    private int animIndex = 0;

    public Boolean PlaySequence(OperationManager controller)
    {
        Boolean val = false;
        if (animIndex < transitionParams.Length)
        {

            if (index[animIndex] == controller.Index)
            {
                val = controller.PlayAnimation(transitionParams[animIndex], animationName[animIndex]);

                if (val)
                {
                    animIndex++;
                }
            }
        }

        return val;
    }

    public Boolean PlaySequence(AnimController controller)
    {
        Boolean val = false;
        if (animIndex < transitionParams.Length)
        {

            if (index[animIndex] == controller.Index)
            {
                val = controller.PlayAnimation(transitionParams[animIndex], animationName[animIndex]);

                if (val)
                {
                    animIndex++;
                }
            }
        }

        return val;
    }

    public int Anim
    {
        get => anim;
    }

    public string Name
    {
        get => TriggerControllerName;
    }

    public int Index
    {
        get => animIndex;
        set => animIndex = value;
    }

    public int CurrentSentenceIndex()
    {
        int val = sentenceIndex[Index - 1];
        return val;
    }


    public int SentenceIndex()
    {
        try
        {
            int val = sentenceIndex[Index];
            return val;
        } catch(IndexOutOfRangeException e)
        {
            return -1;
        }
       
    }
}
