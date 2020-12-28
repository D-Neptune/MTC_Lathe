using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    // Start is called before the first frame update
    public Color hoverColor;
    public Color onClickedColor;
    private Color basicColor;
    private Boolean hovering = false;
    public float timer, counter;
    [SerializeField] private int detailIndex;
    [SerializeField] public GameObject ObjectManager;

    public Boolean hasBeenClicked = false;

    void Awake()
    {
        setBasicColor();
    }

    public void setDetailIndex(int index)
    {
        detailIndex = index;
    }

    public void OnMouseDown()
    {
        ObjectManager.GetComponent<ComponentManager>().SetDetails(detailIndex);
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            GetComponentInParent<SetupOnHover>().hasBeenClicked();
        }
    }
    private void LateUpdate()
    {
        if (hovering)
        {
            counter -= Time.deltaTime;
            if(counter <= 0)
            {
                SiblingHover();
                GetComponentInParent<SetupOnHover>().Hovering(hovering);

            }
        }
        else
        {
            counter = timer; 
        }
    }
    // Update is called once per frame
    public void OnMouseOver()
    {
        hovering = true;
    }

    public void OnMouseExit()
    {
        SiblingHoverExit();
        hovering = false;
        GetComponentInParent<SetupOnHover>().Hovering(hovering);
    }

    public void SiblingHover()
    {
        if (!hasBeenClicked)
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
    }

    public void SiblingHoverExit()
    {
        if (!hasBeenClicked) 
        {
            if (basicColor == null)
            {
                setBasicColor();
            }
            GetComponent<Renderer>().material.color = basicColor;
        }
    }

    private void setBasicColor()
    {
        basicColor = GetComponent<Renderer>().material.color;
    }

    public void SetTimer(float timeToSet)
    {
        timer = counter = timeToSet;
    }

    public void SetClickedColor()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(basicColor, Color.black, 0.7f);
        hasBeenClicked = true;
    }

    public IEnumerator FlashMesh(float interval, int flashes)
    {
        if (!hasBeenClicked)
        {
            for(int i = 0; i < flashes; i++)
            {
                GetComponent<Renderer>().material.color = hoverColor;
                yield return new WaitForSecondsRealtime(interval);
                GetComponent<Renderer>().material.color = basicColor;
                yield return new WaitForSecondsRealtime(interval);
            }

        }
    }
}
