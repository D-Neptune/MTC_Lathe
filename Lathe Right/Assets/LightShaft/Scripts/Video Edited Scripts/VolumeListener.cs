using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeListener : MonoBehaviour
{
    [SerializeField] private Image VolumeImage;
    [SerializeField] private Vector2 LowHigh;
    [SerializeField] private Sprite LowVolume, MediumVolume, HighVolume, Mute;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private AnchorToggle CurrentTime, Divider, TotalTime, SoundHandler;
    private float VolumeHolder;
    // Start is called before the first frame update


    // Update is called once per frame

    public void SliderVolume()
    {
        if(VolumeSlider.value == 0)
        {
            VolumeImage.sprite = Mute;
        }
        else if(VolumeSlider.value <= LowHigh.x)
        {
            VolumeImage.sprite = LowVolume;
        }
        else if (VolumeSlider.value > LowHigh.x && VolumeSlider.value <= LowHigh.y)
        {
            VolumeImage.sprite = MediumVolume;
        }
        else
        {
            VolumeImage.sprite = HighVolume;
        }
    }



    public void VolumeAnchorSwitch(bool sign)
    {
        CurrentTime.SwitchAnchors(sign);
        Divider.SwitchAnchors(sign);
        SoundHandler.SwitchAnchors(sign);
        TotalTime.SwitchAnchors(sign);
    }
}
