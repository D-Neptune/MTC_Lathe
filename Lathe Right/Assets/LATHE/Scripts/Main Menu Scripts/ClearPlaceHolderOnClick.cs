using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClearPlaceHolderOnClick : MonoBehaviour, ISelectHandler
{
    public Text placeholderText;
    public void OnSelect(BaseEventData data)
    {
        //placeholderText.text = "";
    }

}
