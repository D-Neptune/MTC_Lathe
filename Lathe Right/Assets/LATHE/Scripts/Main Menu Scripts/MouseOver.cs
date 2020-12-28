using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseOver : MonoBehaviour
{
    // Start is called before the first frame update
    private Color normalColor;
    private Font normalFont;
    private Color onHoverColor;
    private Color normalButtonColor;
    Text buttonText;
    public Color pressedColor;
    public Color color;
    public Font font;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        onHoverColor = color;
        normalColor = buttonText.color;
        normalFont = buttonText.font;
        normalButtonColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    public void Hover()
    {
        buttonText.color = onHoverColor;
        buttonText.font = font;
    }

    public void NoHover()
    {
        buttonText.color = normalColor;
        buttonText.font = normalFont;
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
