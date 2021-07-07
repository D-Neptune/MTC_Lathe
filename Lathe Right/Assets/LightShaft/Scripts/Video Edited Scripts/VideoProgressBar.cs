using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using System.Collections;

public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IBeginDragHandler, IPointerUpHandler
{
    public bool SeekingEnabled;
    public YoutubePlayer player;
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
            player.Pause();
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (SeekingEnabled)
        {
            player.Play();
            player.VideoSkipDrag = false;
        }

    }
}