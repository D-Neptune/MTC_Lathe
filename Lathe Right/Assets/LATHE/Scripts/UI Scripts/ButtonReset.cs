using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReset : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    public void ResetDRO()
    {
        foreach (Button button in buttons)
        {
            button.image.color = Color.white;
        }
    }
}
