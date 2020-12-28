using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Panel1;
    [SerializeField] private GameObject Panel2;
    public bool isActive1 = false;
    public bool isActive2 = false;

    public void togglePanel1()
    {
        if (isActive1)
        {
            Panel1.SetActive(false);
        }
        else
        {
            Panel1.SetActive(true);
            if (isActive2)
            {
                Panel2.SetActive(false);
                isActive2 = !isActive2;
            }
        }
        isActive1 = !isActive1;
    }

    public void togglePanel2()
    {
        if (isActive2)
        {
            Panel2.SetActive(false);
        }
        else
        {
            Panel2.SetActive(true);
            if (isActive1)
            {
                Panel1.SetActive(false);
                isActive1 = !isActive1;
            }
        }
        isActive2 = !isActive2;
    }

    public void SetPanel1(bool value)
    {
        if (isActive2 && value)
        {
            Panel2.SetActive(false);
        }
        Panel1.SetActive(value);

        isActive1 = value;
        isActive2 = false;
    }

    public void SetPanel2(bool value)
    {
        if (isActive1 && value)
        {
            Panel1.SetActive(false);
        }
        Panel2.SetActive(value);

        isActive2 = value;
        isActive1 = false;
    }
}
