using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Triggers", menuName = "AnimTriggers")]
public class Triggers : ScriptableObject
{
    public List<Trigger> triggers;
}
