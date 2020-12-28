using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private GameObject Tab1;
    [SerializeField] private GameObject Tab2;
    [SerializeField] private Text Tab1Text;
    [SerializeField] private Text Tab2Text;
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    void Start()
    {
        TabActive(true);
    }

    // Update is called once per frame
    public void TabActive(bool tabChosen) 
    {
        Tab1.SetActive(tabChosen);
        Tab2.SetActive(!tabChosen);
        ChangeTextColor(tabChosen);
    }

    private void ChangeTextColor(bool tabChosen)
    {
        if (tabChosen)
        {
            Tab1Text.color = onColor;
            Tab2Text.color = offColor;
        }
        else
        {
            Tab1Text.color = offColor;
            Tab2Text.color = onColor;
        }
    }
}
