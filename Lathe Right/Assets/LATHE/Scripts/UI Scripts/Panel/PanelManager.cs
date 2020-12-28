using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject DROPanel;
    [SerializeField] private GameObject toolsPanel;
   

    // Start is called before the first frame update
    void Start()
    {
        DROPanel.SetActive(true);
        toolsPanel.SetActive(false);
    }

    public void UpdateUIPanel (){

        // if(diagramPanel.activeSelf) // Close other panels
        // {
        //     DROPanel.SetActive(false);
        //     toolsPanel.SetActive(false);
        // }
    }
}
