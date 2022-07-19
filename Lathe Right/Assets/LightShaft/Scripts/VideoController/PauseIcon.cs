using System.Collections;
using System.Collections.Generic;
using LightShaft.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseIcon : MonoBehaviour {

    public VideoPlayer p;
    public Button pausePlayButton;
    public Image playImage;
    public Image pauseImage;


    private void FixedUpdate()
    {
        if (p.isPaused)
        {
            pausePlayButton.image = playImage;
            playImage.gameObject.SetActive(true);
            pauseImage.gameObject.SetActive(false);
        }
        else
        {
            pausePlayButton.image = pauseImage;
            pauseImage.gameObject.SetActive(true);
            playImage.gameObject.SetActive(false);
        }
    }
}