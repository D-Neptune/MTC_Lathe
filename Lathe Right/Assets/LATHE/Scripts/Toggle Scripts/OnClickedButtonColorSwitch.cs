using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickedButtonColorSwitch : MonoBehaviour
{
    // Start is called before the first frame update

    private Color normalButtonColor;
    public Color pressedColor;

    void Start()
    {
        normalButtonColor = GetComponent<Image>().color;
    }

    public void onClickColor()
    {
        GetComponent<Image>().color = pressedColor;
    }

    public void onClickEndColor()
    {
        GetComponent<Image>().color = normalButtonColor;
    }
}
