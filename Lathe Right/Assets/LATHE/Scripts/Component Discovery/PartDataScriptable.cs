using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComponentDetails", menuName = "ComponentDetails")]

public class PartDataScriptable : ScriptableObject
{
    // Start is called before the first frame update
    public List<ComponentDetail> components;
}
