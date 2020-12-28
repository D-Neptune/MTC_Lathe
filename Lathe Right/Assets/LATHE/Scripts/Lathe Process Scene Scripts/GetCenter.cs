using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCenter : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 trans = new Vector3(0, 0, 0);
    void Start()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach (Transform tr in transforms)
        {
            trans += tr.localPosition;
        }
        Vector3 center = trans / transforms.Length;
        Debug.Log("Center of cylinder " + center);
    }

    // Update is called once per fram
}
