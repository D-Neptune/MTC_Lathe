using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingTutorialToggle : MonoBehaviour
{
    [SerializeField] private SceneDisplayToggle tutorialToggle;
    [SerializeField] private List<VideoProgressBar> seekingProgess;
    // Start is called before the first frame update
    void Start()
    {
        if (!tutorialToggle.getTutorial() || tutorialToggle.AdminMode)
        {
            foreach(VideoProgressBar seeks in seekingProgess)
            {
                seeks.SeekingEnabled = true;
            }
        }
        else
        {
            foreach (VideoProgressBar seeks in seekingProgess)
            {
                seeks.SeekingEnabled = false;
            }
        }
    }

}
