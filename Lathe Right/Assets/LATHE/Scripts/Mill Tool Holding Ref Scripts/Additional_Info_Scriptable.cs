using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddtionnalInformation", menuName = "AddtionnalInformation")]

public class Additional_Info_Scriptable : ScriptableObject
{
    // Start is called before the first frame update
    public List<ComponentDetail> components;
}
