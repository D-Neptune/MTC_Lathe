using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;
    public TextMeshProUGUI progressTxt;


    public void UpdateProgressBar(int current, int maximum)
    {
        progressTxt.text = "Questions Completed: " + current.ToString() + " / " + maximum.ToString();

        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
