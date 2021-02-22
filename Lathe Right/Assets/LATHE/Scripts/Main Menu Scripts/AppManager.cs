using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0; // Must be zero for targetFrameRate to work
        Application.targetFrameRate = 30;
        DontDestroyOnLoad(this.gameObject); // Preserves AppManager across scenes
    }
}
