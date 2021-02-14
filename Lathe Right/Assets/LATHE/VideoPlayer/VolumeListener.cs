using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeListener : MonoBehaviour
{
    [SerializeField] private Image VolumeImage;
    [SerializeField] private Vector2 LowHigh;
    [SerializeField] private Sprite LowVolume, MediumVolume, HighVolume;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private float AnchorOffsetX, AnchorOffsetX2;
    [SerializeField] private RectTransform CurrentTime, Divider, TotalTime, SoundHandler;
    
    // Start is called before the first frame update


    // Update is called once per frame

    public void SliderVolume()
    {
        if(VolumeSlider.value <= LowHigh.x)
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
        float tmpOffset = AnchorOffsetX;
        float tmpOffset2 = AnchorOffsetX2;
        if (!sign)
        {
            tmpOffset *= -1;
            tmpOffset2 *= -1;
        }

        CurrentTime.anchorMin = new Vector2(CurrentTime.anchorMin.x + tmpOffset, CurrentTime.anchorMin.y);
        Divider.anchorMin = new Vector2(Divider.anchorMin.x + tmpOffset, Divider.anchorMin.y);
        TotalTime.anchorMin = new Vector2(TotalTime.anchorMin.x + tmpOffset, TotalTime.anchorMin.y);

        CurrentTime.anchorMax = new Vector2(CurrentTime.anchorMax.x + tmpOffset, CurrentTime.anchorMax.y);
        Divider.anchorMax = new Vector2(Divider.anchorMax.x + tmpOffset, Divider.anchorMax.y);
        TotalTime.anchorMax = new Vector2(TotalTime.anchorMax.x + tmpOffset, TotalTime.anchorMax.y);

        SoundHandler.anchorMax = new Vector2(SoundHandler.anchorMax.x + tmpOffset2, SoundHandler.anchorMax.y);

        CurrentTime.anchoredPosition = Vector2.zero;
        Divider.anchoredPosition = Vector2.zero;
        TotalTime.anchoredPosition = Vector2.zero;
        SoundHandler.anchoredPosition = Vector2.zero;
    }
}
