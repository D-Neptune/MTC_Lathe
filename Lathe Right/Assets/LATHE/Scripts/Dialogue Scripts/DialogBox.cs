using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogsIntro", menuName = "DialogsIntro")]

public class DialogBox : ScriptableObject
{
    public List<Dialogue> dialogues;
}
