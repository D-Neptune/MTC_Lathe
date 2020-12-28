using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ContinueManager : MonoBehaviour
{
    [SerializeField] private List<int> senteceIndexes;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private Button continueBTN;

    private void LateUpdate()
    {

        Boolean found = false;
        foreach (int clipTrigger in senteceIndexes)
        {
            if (manager.SentenceIndex == clipTrigger)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            continueBTN.interactable = true;
        }
    }
}
