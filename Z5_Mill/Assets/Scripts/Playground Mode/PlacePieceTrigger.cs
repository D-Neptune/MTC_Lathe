﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePieceTrigger : MonoBehaviour
{
    [SerializeField] private PlacePiece placePiece;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private GameObject activateOnClick;
    private Color basicColor;
    private Boolean Clicked;


    // Start is called before the first frame update
    public void Start()
    {
        basicColor = GetComponent<Renderer>().material.color;
        Reset();
    }

    // Update is called once per frame
    private void PlaySequence()
    {
        placePiece.PlaceStock();
    }

    private void OnMouseDown()
    {
        if (!Clicked)
        {
            GetComponent<Renderer>().material.color = clickedColor;
            activateOnClick.SetActive(true);
            gameObject.SetActive(false);
            PlaySequence();
        }

    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = basicColor;
    }

    private void OnMouseOver()
    {
        if (!Clicked)
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
    }

    public void Reset()
    {
        GetComponent<Renderer>().material.color = basicColor;
        gameObject.SetActive(true);
        activateOnClick.SetActive(false);
        Clicked = false;
    }
}
