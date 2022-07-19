using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using System.Collections;
using LightShaft.Scripts;

public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IBeginDragHandler, IPointerUpHandler
{
    public bool SeekingEnabled;
    public VideoManager2 player;
    public void OnDrag(PointerEventData eventData)
    {
        if (SeekingEnabled)
        {
            player.VideoSkipDrag = true;
            player.TrySkip(Input.mousePosition);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (SeekingEnabled)
        {
            player.VideoSkipDrag = true;
            player.TrySkip(Input.mousePosition);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (SeekingEnabled)
        {
            player.VideoSkipDrag = false;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (SeekingEnabled)
        {
            player.PlayPause();
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (SeekingEnabled)
        {
            player.PlayPause();
            player.VideoSkipDrag = false;
        }
    }
}