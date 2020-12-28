using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoOperator : MonoBehaviour
{
    [SerializeField] private Camera_Toggle camera_Toggle;
    [SerializeField] private LanguageSceneSwitcher languageScene;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button StopVideoButton, playButton, pauseButton, stopButton;
    [SerializeField] private Text title;
    [SerializeField] private List<VideoClip> videoClips, videoClipsFR;
    [SerializeField] private List<int> indexes;
    [SerializeField] private List<string> titles, titlesFR;
    [SerializeField] private bool onStart = false;
    [SerializeField] private Image exitImage;
    [SerializeField] private Sprite exit;

    private List<bool> playedOnces;
    private int m_index = -1;
    public bool language;


    // Start is called before the first frame update
    void Start()
    {
        playedOnces = new List<bool>();
        for (int i = 0; i < videoClips.Count; i++)
        {
            playedOnces.Add(false);
        }
        StopVideoButton.interactable = false;
        stopButton.interactable = false;
        VideoPanel.SetActive(false);
        videoPlayer.loopPointReached += VideoPlayed;
        VideoEvents.current.youtubePlayerException += SentLink;
        if (languageScene != null)
        {
            if (onStart && !languageScene.languageScene.getLanguage())
            {
                PlayVideoClip(0);
            }
        }
    }

    public void SentLink()
    {
        if (m_index > -1)
        {
            playedOnces[m_index] = true;
            StopVideoButton.interactable = true;
        }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        if (m_index > -1)
        {
            if (!playedOnces[m_index])
            {
                StopVideoButton.interactable = false;
                stopButton.interactable = false;
            }
            else
            {
                StopVideoButton.interactable = true;
                stopButton.interactable = true;

            }
        }
    }

    public void VideoPlayed(VideoPlayer video)
    {
        if (m_index > -1)
        {
            if (!playedOnces[m_index])
            {
                playedOnces[m_index] = true;
                StopVideoButton.interactable = true;
                stopButton.interactable = true;
            }
            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void StartVideo()
    {
        VideoPanel.SetActive(true);
    }

    public void ExitVideo()
    {
        camera_Toggle.ChangeCamForVid(false);
        VideoPanel.SetActive(false);
        m_index = -1;
    }

    public void StopVideo()
    {
        if (m_index > -1)
        {
            if (playedOnces[m_index])
            {
                videoPlayer.Stop();
                playButton.gameObject.SetActive(true);
                pauseButton.gameObject.SetActive(false);
            }
        }
    }

    public void PlayVideoClip(int index)
    {
        if (index >= 0 && index < videoClips.Count)
        {
            camera_Toggle.ChangeCamForVid(true);
            exitImage.sprite = exit;
            VideoPanel.SetActive(true);

            if (!playedOnces[index])
            {
                StopVideoButton.interactable = false;
                stopButton.interactable = false;
            }
            else
            {
                StopVideoButton.interactable = true;
                stopButton.interactable = true;

            }
            if (language)
            {
                m_index = index;
                title.text = titles[index];
                videoPlayer.clip = videoClips[index];
                videoPlayer.Play();

            }
            else
            {
                m_index = index;
                title.text = titlesFR[index];
                videoPlayer.clip = videoClipsFR[index];
                videoPlayer.Play();
            }

        }
    }




    public void SwitchLang()
    {
        language = !language;
    }

}
