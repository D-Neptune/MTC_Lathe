using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class YoutubeExceptionListener : MonoBehaviour
{
    [SerializeField] private VideoOperator controller;
    [SerializeField] private VideoManager controller2;
    [SerializeField] private List<string> Links, LinksFR;
    [SerializeField] private string linkMessage, linkMessageFR;
    [SerializeField] private InputField linkArea;


    // Update is called once per frame
    public void DisplayLink(bool lang)
    {
        if (lang)
        {
            linkArea.text = linkMessage + Links[GetVidIndex()];
        }
        else
        {
            linkArea.text = linkMessageFR + LinksFR[GetVidIndex()];
        }
    }


    private int GetVidIndex()
    {
        if (controller != null)
        {
            return controller.Index;
        }
        else
        {
            return controller2.Index;
        }
    }

 

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.V && e.alt)
        {
            if (controller != null)
            {
                if (controller.Playing)
                {
                    controller.LinkSent();
                }
            }
            else
            {
                if (controller2.Playing)
                {
                    controller2.LinkSent();
                }
            }
        }
    }
}
