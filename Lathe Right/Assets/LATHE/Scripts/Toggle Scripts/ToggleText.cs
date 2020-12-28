using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleText : MonoBehaviour
{
    // Start is called before the first frame update
    private string initialText;
    public string toggleOptionText;
    public string toggleOptionText2;
    public Text currentText;
    void Start()
    {
        initialText = currentText.text;
    }

    // Update is called once per frame
    public void toggle()
    {
        if ((initialText == null) || (toggleOptionText2 != null && !initialText.Equals(toggleOptionText2) && initialText.Equals(toggleOptionText)))
        {
            initialText = toggleOptionText2;
        }
        
        if (currentText.text.Equals(initialText))
        {
            currentText.text = toggleOptionText;
        } 
        else
        {
            currentText.text = initialText;
        }
    }
}
