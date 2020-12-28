using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Toggle : MonoBehaviour
{
    public Camera cam_main_scene;
    public Camera cam_drill1;
    public Camera cam_drill2;
    public Camera cam_sm1; // sm == side milling perspective
    public Camera cam_fm1; // fm == face milling perspective 
    //public Camera cam_5;
    public Text myText;
    private Camera currentCam;

    private int clickCounter;
    private int numberOfCams = 5; // the number of cams that can be switched between

    public void Awake()
    {
        clickCounter = 1;
        cam_main_scene.enabled = true;
        currentCam = cam_main_scene;

    }

    public void switchcam() {

        deactivateall ();
        countClicks ();

        if(clickCounter == 1)
        {
            cam_main_scene.enabled = true;
            currentCam = cam_main_scene;
            myText.text = "1";

        }
        else if (clickCounter == 2)
        {
            cam_drill1.enabled = true;
            currentCam = cam_drill1;

            myText.text = "2";
        }
        else if (clickCounter==3)
        {
            cam_drill2.enabled = true;
            currentCam = cam_drill2;

            myText.text = "3";
        }
        else if (clickCounter==4) 
        {
            cam_sm1.enabled = true;
            currentCam = cam_sm1;

            myText.text = "4";
        }
        else if (clickCounter==5)
        {
            cam_fm1.enabled = true;
            currentCam = cam_fm1;

            myText.text = "5";
        }
        // else if (clickCounter==6)
        // {
        //     cam_5.enabled = true;
        //     currentCam = cam_5;

        //     myText.text = "6";
        // }
    }

    public void deactivateall() {
        cam_main_scene.enabled = false;
        cam_drill1.enabled = false;
        cam_drill2.enabled = false;
        cam_sm1.enabled = false;
        cam_fm1.enabled = false;
        // cam_5.enabled = false;
    }

    public void ChangeCamForVid(bool vid)
    {
        if (vid)
        {
            clickCounter = 4;
            switchcam();
        }
        else
        {
            switchcam();
        }

    }

    private void countClicks()
    {
        if (clickCounter == numberOfCams)
        {
            clickCounter = 1; //back to first cam if reach the last cam
        }
        else
        {
            clickCounter ++;
        }
    }

    public Camera getCurrentCam()
    {
        return currentCam;
    }

}
