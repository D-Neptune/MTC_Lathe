using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetupOnHover : MonoBehaviour
{
    [SerializeField] private Color onHoverColor;
    [SerializeField] private int detailIndex;
    [SerializeField] GameObject ObjectManager;
    [SerializeField] private Color onClickedColor;
    [SerializeField] private float FlashDelay = 0.25f;
    [SerializeField] private int NumOfFlash = 5;
    [SerializeField] private float timer = 0.1f;
    public Boolean clicked = false;
    public Boolean flashing = false;
    public UnityEvent clickEvent;

    void Start()
    {
        foreach(Transform children in transform)
        {
            if (children.gameObject.GetComponent<MeshFilter>() != null)
            {
                setupMesh(children);
            }
        }
    }

    private void setupMesh(Transform child)
    {
        MeshCollider collider = child.GetComponent<MeshCollider>();

        if (collider==null)
        {
            collider = child.gameObject.AddComponent<MeshCollider>();
        }
        collider.convex = true;
        collider.isTrigger = true;

        Renderer renderer = child.GetComponent<Renderer>();

        if (renderer == null)
        {
            renderer = child.gameObject.AddComponent<Renderer>();
        }

        OnHover hover = child.GetComponent<OnHover>();

        if (hover == null)
        {
            hover = child.gameObject.AddComponent<OnHover>();
        }

        hover.hoverColor = onHoverColor;
        hover.setDetailIndex(detailIndex);
        hover.ObjectManager = ObjectManager;
        hover.onClickedColor = onClickedColor;
        hover.SetTimer(timer);

    }

    public void hasBeenClicked()
    {
        StopAllCoroutines();
        if (!clicked)
        {
            clickEvent.Invoke();
            clicked = true;
            foreach(Transform children in transform)
            {
                OnHover tmpHover = children.GetComponent<OnHover>();
                if(tmpHover != null)
                {
                    tmpHover.SetClickedColor();
                }
            }
        }
    }

    public void Hovering(Boolean res)
    {
        foreach (Transform children in transform)
        {
            OnHover tmpHover = children.GetComponent<OnHover>();
            if (tmpHover != null) {
                if (res)
                {
                    tmpHover.SiblingHover();
                }
                else
                {
                    tmpHover.SiblingHoverExit();
                }
            }
        }
    }

    public void HintFlash()
    {
        flashing = true;
        foreach (Transform children in transform)
        {
            OnHover tmpHover = children.GetComponent<OnHover>();
            if (tmpHover != null)
            {
                StartCoroutine(tmpHover.FlashMesh(FlashDelay, NumOfFlash));
            }
        }
        flashing = false;
    }
}
