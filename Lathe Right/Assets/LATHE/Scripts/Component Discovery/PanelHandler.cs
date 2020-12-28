using System;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Boolean prevState = true;
    private Boolean visible = true;
    void Start()
    {
        activate(true);
    }

    public void TogglePanel()
    {
        activate(!prevState);
    }

    // Update is called once per frame

    public void Visibility()
    {
        if (visible)
        {
            visible = false;
            gameObject.SetActive(false);
        }
        else
        {
            visible = true;
            gameObject.SetActive(prevState);
        }
    }

    public void activate(Boolean val)
    {
        if (visible)
        {
            if (val)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        prevState = val;
    }
}
