using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerToggle : MonoBehaviour
{
    [SerializeField] private Text PowerButtonText;
    [SerializeField] private Image powerIMG;
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    [SerializeField] private string onText, offText;
    [SerializeField] private RampDownSpeed ramp;
    private bool running;
    // Start is called before the first frame update
    private void Start()
    {
        running = false;
        TogglePower(false);
    }

    public void TogglePower(bool powerStatus)
    {
        if (powerStatus)
        {
            powerIMG.color = offColor;
            PowerButtonText.text = offText;
            running = true;
        }
        else
        {
            powerIMG.color = onColor;
            PowerButtonText.text = onText;
            if (running)
            {
                ramp.RampDown();
            }
            running = false;
            
        }
    }

    public bool Running
    {
        get => Running;
    }
}
