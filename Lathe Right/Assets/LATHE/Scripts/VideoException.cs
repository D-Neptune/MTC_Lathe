using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoException : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("On Enable");
        VideoEvents.current.YoutubePlayerException();
    }
}
