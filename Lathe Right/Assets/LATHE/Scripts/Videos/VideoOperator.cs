using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoOperator : MonoBehaviour
{
    [SerializeField] private Camera dummyCamera;
    [SerializeField] private YoutubeExceptionListener LinkDisplayer;
    [SerializeField] private Camera_Toggle camera_Toggle;
    [SerializeField] private LanguageSceneSwitcher languageScene;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private YoutubePlayer youtubePlayer;
    [SerializeField] private VideoPlayer videoPlayer;
    //[SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private Text title;
    [SerializeField] private List<string> videoClips, videoClipsFR;
    [SerializeField] private List<TextAsset> subtitleClips, subtitleClipsFR;
    [SerializeField] private List<int> indexes;
    [SerializeField] private List<string> titles, titlesFR;
    //[SerializeField] private Image exitImage;
    [SerializeField] private Sprite exit;
    [SerializeField] private YoutubeSubtitlesReader subtitlesReader;

    private bool[] playedOnces;
    private int m_index = -1;
    public bool language;
    private bool playing = false;

    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private int VideoIndexOnStart;
    

    // Start is called before the first frame update
    void Start()
    {
        playedOnces = new bool[videoClips.Count];
        for (int i = 0; i < videoClips.Count; i++)
        {
            playedOnces[i] = false;
        }
        //StopVideoButton.interactable = false;
        //stopButton.interactable = false;
        VideoPanel.SetActive(false);
        videoPlayer.loopPointReached += VideoPlayed;
        if (PlayOnAwake)
        {
            PlayVideoClip(VideoIndexOnStart);
        }
    }

    public bool PlayOnAwake
    {
        get => playOnAwake;
    }

    public int Index
    {
        get => m_index;
    }

    public bool Playing
    {
        get => playing;
    }

    public void SentLink()
    {
        if (m_index > -1)
        {
            playedOnces[m_index] = true;
            //StopVideoButton.interactable = true;
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        if (m_index > -1)
        {
            if (!playedOnces[m_index])
            {
                //StopVideoButton.interactable = false;
                //stopButton.interactable = false;
            }
            else
            {
                //StopVideoButton.interactable = true;
                //stopButton.interactable = true;

            }
        }
        playing = true;
    }


    public void VideoPlayed(VideoPlayer video)
    {
        if (m_index > -1)
        {
            if (!playedOnces[m_index])
            {
                playedOnces[m_index] = true;
                //StopVideoButton.interactable = true;
               // stopButton.interactable = true;
            }
            //playButton.gameObject.SetActive(true);
            //pauseButton.gameObject.SetActive(false);
            playing = false;
        }
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void LinkSent() //Method called if error with video playback occurs
    {
        if (m_index > -1)
        {
            playedOnces[m_index] = true;
            //StopVideoButton.interactable = true;
        }
    }

    public void ExitVideo()
    {
        if (m_index > -1)
        {
            camera_Toggle.ChangeCamForVid(false);
            VideoPanel.SetActive(false);
            m_index = -1;
            playing = false;
            if (dummyCamera != null)
            {
                dummyCamera.enabled = false;
            }
        }
    }

    public void StopVideo()
    {
        if (m_index > -1)
        {
            if (playedOnces[m_index])
            {
                videoPlayer.Stop();
                //playButton.gameObject.SetActive(true);
                //pauseButton.gameObject.SetActive(false);
            }
        }
    }

    public void PlayVideoClip(int index)
    {
        if (index >= 0 && index < videoClips.Count)
        {
            camera_Toggle.ChangeCamForVid(true);
            //exitImage.sprite = exit;
            VideoPanel.SetActive(true);

            if (!playedOnces[index])
            {
                //StopVideoButton.interactable = false;
                //stopButton.interactable = false;
            }
            else
            {
                //StopVideoButton.interactable = true;
                //stopButton.interactable = true;

            }
            if (language)
            {
                subtitlesReader.LoadSubtitles(subtitleClips[index]);
                youtubePlayer.Play(videoClips[index]);
                title.text = titles[index];

            }
            else
            {
                subtitlesReader.LoadSubtitles(subtitleClipsFR[index]);
                youtubePlayer.Play(videoClipsFR[index]);
                title.text = titlesFR[index];
            }
            Debug.Log("Play Youtube Video");
            m_index = index;
            playing = true;
            LinkDisplayer.DisplayLink(language);
            if (dummyCamera != null)
            {
                camera_Toggle.getCurrentCam().enabled = false;
                dummyCamera.enabled = true;
            }
        }
    }

    public void SwitchLang()
    {
        language = !language;
    }

}
