using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDragger : MonoBehaviour
{
    private Boolean dragging;
    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void enableDragger(Boolean state)
    {
        dragging = state;
    }

}
