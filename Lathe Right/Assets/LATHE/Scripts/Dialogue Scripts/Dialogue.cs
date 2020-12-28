using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // To allow class to show up in the inspector to edit it
public class Dialogue { // This class is used as an object that is passed into the dialogue manager whenever we want to start a new dialogue

    // this class hosts all information needed for a dialogue
    
    public string name;
    public string frenchName;

    [TextArea(3, 10)]
    public string[] sentences;    
    
    [TextArea(3, 10)]
    public string[] sentencesFR;

    public Sprite[] images;
}
