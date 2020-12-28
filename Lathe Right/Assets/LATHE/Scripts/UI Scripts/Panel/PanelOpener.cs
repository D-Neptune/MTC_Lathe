using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive); //Checks for active state, set to oppositve, i.e. if inactive, make active

            
        }
        UpdateUIPanel();
    }

    private void UpdateUIPanel()
    {
        FindObjectOfType<PanelManager>().UpdateUIPanel();
    }

}
