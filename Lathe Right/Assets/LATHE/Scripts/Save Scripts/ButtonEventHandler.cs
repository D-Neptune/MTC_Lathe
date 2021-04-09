using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventHandler : MonoBehaviour
{
    [SerializeField] private Button SaveBTN;
    private void OnDisable()
    {
        SaveBTN.interactable = true;
    }

    private void OnEnable()
    {
        SaveBTN.interactable = false;

    }
}
