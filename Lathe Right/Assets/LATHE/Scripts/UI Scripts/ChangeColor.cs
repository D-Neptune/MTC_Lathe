using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Color Darken;
    [SerializeField] private Image image;
    public void OnClicked()
    {
        image.color = Darken;
    }
}
