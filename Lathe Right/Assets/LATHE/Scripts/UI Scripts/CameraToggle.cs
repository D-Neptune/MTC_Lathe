using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class CameraToggle : MonoBehaviour
{
    Camera[] cameras;
    [SerializeField] string genericCameraTag;
    [SerializeField] Text camCurrentNum;
    [SerializeField] Camera camController;
    private static string MAIN_CAM_TAG = "MainCamera"; 
    private int index;
    void Awake()
    {
        index = 0;
        cameras = GetComponentsInChildren<Camera>();
        List<Camera> camList = new List<Camera>(cameras);
        camList.Insert(0, camController); // insert camera controller at index 0
        cameras = camList.ToArray();

        camCurrentNum.text = "1";
        
        deactivateAll();
        SwtichView();
    }

    // Update is called once per frame
    public void SwtichView()
    {
        if (index == cameras.Length)
        {
            cameras[cameras.Length - 1].tag = genericCameraTag;
            cameras[cameras.Length - 1].enabled = false;
            index = 0;
        } else if (index != 0)
        {
            cameras[index - 1].tag = genericCameraTag;
            cameras[index - 1].enabled = false;
        }
        camCurrentNum.text = (index + 1).ToString();
        cameras[index].enabled = true;
        cameras[index].tag = MAIN_CAM_TAG;
        index++;
    }

    void deactivateAll()
    {
        for(int i = 0; i<cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
    }
}
