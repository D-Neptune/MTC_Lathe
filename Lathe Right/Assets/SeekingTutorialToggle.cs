using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingTutorialToggle : MonoBehaviour
{
    [SerializeField] private SceneDisplayToggle tutorialToggle;
    [SerializeField] private List<YoutubePlayer.VideoPlayerProgress> seekingProgess;
    // Start is called before the first frame update
    void Start()
    {
        if (!tutorialToggle.getTutorial())
        {
            foreach(YoutubePlayer.VideoPlayerProgress seeks in seekingProgess)
            {
                seeks.SeekingEnabled = true;
            }
        }
    }

}
