using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

//This class control the youtube video player, you can make custom buttons and bind to that functions;
namespace LightShaft.Scripts
{
    public class YoutubeVideoController : MonoBehaviour
    {
        //private YoutubePlayer _player;
        private VideoPlayer _player;
        public VideoManager2 youtubeController;
        public bool showPlayerControl;
        public Slider playbackSlider;
        public Image progressRectangle;
        public Slider speedSlider;
        public Slider volumeSlider;
        public Text currentTime;
        public Text totalTime;
        public GameObject loading;
        public Button nextVideoButton;
        public Button previousVideoButton;
        
        private float totalVideoDuration;
        private float currentVideoDuration;
        private bool videoSeekDone = false;
        private bool videoAudioSeekDone = false;
        private float hideScreenTime = 0;
        private float audioDuration;

        private bool showingVolume = false;
        private bool showingSpeed = false;

        [SerializeField]
        public GameObject controllerMainUI;

        [Tooltip("Time to auto hide the controller. 0 will not hide the controller.")]
        [SerializeField] public int hideScreenControlTime = 0;

        [Header("If you want to use a sprite rectangle instead of slider disable this")]
        public bool useSliderToProgressVideo = true;
        private void Awake()
        {
            _player = GetComponent<VideoPlayer>();

            if (!showPlayerControl) return;
            // if (!_player.customPlaylist)
            // {
            //     if (previousVideoButton != null && nextVideoButton != null)
            //     {
            //         previousVideoButton.gameObject.SetActive(false);
            //         nextVideoButton.gameObject.SetActive(false);
            //     }
            // }
            else
            {
                if (previousVideoButton != null && nextVideoButton != null)
                {
                    previousVideoButton.gameObject.SetActive(true);
                    nextVideoButton.gameObject.SetActive(true);
                }
            }
            
            if (showPlayerControl)
            {
                if(speedSlider == null)
                    Debug.LogWarning("Drag the playback speed slider to the speedSlider field.");
                if(volumeSlider == null)
                    Debug.LogWarning("Drag the volume eslider to the volumeSlider field.");
                if(playbackSlider == null)
                    Debug.LogWarning("Drag the playback slider to the playbackSlider field, this is necessary to change the video progress.");
            }
            //speedSlider.maxValue = 3;   //max playback speed is 3;

            if (useSliderToProgressVideo)
            {
                progressRectangle.gameObject.SetActive(false);
                if(playbackSlider != null)
                    playbackSlider.gameObject.SetActive(true);
            }
            else
            {
                if (playbackSlider != null)
                    playbackSlider.gameObject.SetActive(false);
                progressRectangle.gameObject.SetActive(true);
            }
        }

        private void FixedUpdate()
        {
             
                if (showPlayerControl)
                {
                    if (_player.isPlaying)
                    {
                        totalVideoDuration = Mathf.RoundToInt(_player.frameCount / _player.frameRate);
                        currentVideoDuration = Mathf.RoundToInt(_player.frame / _player.frameRate);
                    }
                }

                if (_player.frameCount > 0)
                {
                    if (youtubeController != null && showPlayerControl)
                    {
                        if (useSliderToProgressVideo) //use slider
                        {
                            playbackSlider.value = (float)_player.time;
                        }
                        else //use rectangle sprite.
                        {
                            //Debug.Log("Youtube Video Controller: Progress Rectangle Sprite");
                            if (progressRectangle != null)
                            {
                                //Debug.Log("Youtube Video Controller: Progress Rectangle");
                                progressRectangle.fillAmount = (float)_player.frame / (float)_player.frameCount;
                            }
                        }
                    }
                }

                if (currentTime != null && totalTime != null)
                {
                    currentTime.text = FormatTime(Mathf.RoundToInt(currentVideoDuration));
                    totalTime.text = FormatTime(Mathf.RoundToInt(totalVideoDuration));
                }
        }
        
        private string FormatTime(int time)
        {
            int hours = time / 3600;
            int minutes = (time % 3600) / 60;
            int seconds = (time % 3600) % 60;
            if (hours == 0 && minutes != 0)
            {
                return minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            else if (hours == 0 && minutes == 0)
            {
                return "00:" + seconds.ToString("00");
            }
            else
            {
                return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }
        public void Play()
        {
            _player.Play();
        }

        public void Pause()
        {
            _player.Pause();
        }

        public void PlayToggle()
        {
            youtubeController.PlayPause();
        }

        public void ChangeVolume(float volume)
        {
            Debug.Log("WHEN WE START");
     
            switch (_player.audioOutputMode)
            {
                case VideoAudioOutputMode.Direct:
                    // _player.audioPlayer.SetDirectAudioVolume(0, volume);
                    _player.SetDirectAudioVolume(0, volume);
                    break;
                case VideoAudioOutputMode.AudioSource:
                    _player.GetComponent<AudioSource>().volume = volume;
                    _player.SetDirectAudioVolume(0, volume);
                    break;
                default:
                    _player.GetComponent<AudioSource>().volume = volume;
                    _player.SetDirectAudioVolume(0, volume);
                    break;
            }
        }

        public void ChangeVolume()
        {
            if (volumeSlider != null)
            {
                ChangeVolume(volumeSlider.value/10);
            }
        }

        public void ChangePlaybackSpeed(float speed)
        {
            if(!_player.canSetPlaybackSpeed) return;
            if (speed <= 0)
            {
                _player.playbackSpeed = .5f;
                // _player.audioPlayer.playbackSpeed = .5f;
            }
            else
            {
                _player.playbackSpeed = speed;
                // _player.audioPlayer.playbackSpeed = speed;
            }
           
        }
        public void ChangePlaybackSpeed()
        {
            if (speedSlider != null)
            {
                ChangePlaybackSpeed(speedSlider.value/10);
            }
        }

        // public void PlayNextVideo()
        // {
        //     if (!NextVideo())
        //         Debug.Log("Cannot play the next video.");
        // }
        //
        // public void PlayPreviousVideo()
        // {
        //     if(!PreviousVideo())
        //         Debug.Log("Cannot play the previous video.");
        // }

        // public bool NextVideo()
        // {
        //     // if (_player.customPlaylist)
        //     // {
        //     //     _player.CallNextUrl();  return true;
        //     // }else 
        //     //     return false;
        //     youtubeController.PlayNext();  return true;
        // }

        // public bool PreviousVideo()
        // {
        //     // if (_player.customPlaylist)
        //     // {
        //     //     _player.CallPreviousUrl();
        //     //     return true;
        //     // }else return false;
        //     youtubeController.PlayPrevious();
        //     return true;
        // }

        public void ChangeVideoTime(float value)
        {
            float pctg = (Mathf.RoundToInt(value) * 100) / playbackSlider.maxValue;
            pctg = pctg * 0.01f;
            SkipToPercent(pctg);
            //_player.progressStartDrag = false;
        }
        
        public void SkipToPercent(float pct)
        {
            var frame = _player.frameCount * pct;
            _player.frame = (long)frame;
            //_player.Pause();
            progressRectangle.fillAmount = pct;

        }
        // public void PlaybackSliderStartDrag()
        // {
        //     _player.progressStartDrag = true;
        // }

        // public void ToggleFullScreen()
        // {
        //     _player.ToogleFullsScreenMode();
        // }
        
        public void HideControllers()
        {
            if (controllerMainUI != null)
            {
                controllerMainUI.SetActive(false);
                showingVolume = false;
                showingSpeed = false;
                volumeSlider.gameObject.SetActive(false);
                speedSlider.gameObject.SetActive(false);
            }
        }

        public void ToggleVolumeSlider()
        {
            if (showingVolume)
            {
                showingVolume = false;
                volumeSlider.gameObject.SetActive(false);
            }
            else
            {
                showingVolume = true;
                volumeSlider.gameObject.SetActive(true);
            }
        }

        public void SetVolumeSlider(bool state)
        {
            showingVolume = state;
            volumeSlider.gameObject.SetActive(state);
        }

        public void SetSpeedSlider(bool state)
        {
            showingSpeed = state;
            speedSlider.gameObject.SetActive(state);
        }

        public void ToggleSpeedSlider()
        {
            if (showingSpeed)
            {
                showingSpeed = false;
                speedSlider.gameObject.SetActive(false);
            }
            else
            {
                showingSpeed = true;
                speedSlider.gameObject.SetActive(true);
            }
        }
        
        public void TrySkip(Vector2 cursorPosition)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                progressRectangle.rectTransform, cursorPosition, null, out localPoint))
            {
                //float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
                float pct = (localPoint.x - progressRectangle.rectTransform.rect.x) / progressRectangle.rectTransform.rect.width;
                Debug.Log("YOU ARE SKIPPING");
                SkipToPercent(pct);
            }
        }
        
        public void TrySkip(float pct)
        {
            SkipToPercent(pct);
        }
        
        private bool videoSkipDrag = false;
        
        public bool VideoSkipDrag
        {
            get => videoSkipDrag;
            set => videoSkipDrag = value;
        }

    }
    
    
}
