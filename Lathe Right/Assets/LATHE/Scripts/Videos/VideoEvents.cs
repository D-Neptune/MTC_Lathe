using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class VideoEvents : MonoBehaviour
{
    public static VideoEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action youtubePlayerException;

    public void YoutubePlayerException()
    {
        if(youtubePlayerException != null)
        {
            youtubePlayerException();
        }
    }
}
