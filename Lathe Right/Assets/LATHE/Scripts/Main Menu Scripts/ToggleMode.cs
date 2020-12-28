using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMode : MonoBehaviour
{
    [SerializeField] private SceneDisplayToggle displayToggle;
    [SerializeField] private Button library, quiz;

    private void Start()
    {
        if (displayToggle.Done || displayToggle.TestMode)
        {
            library.gameObject.SetActive(true);
            quiz.gameObject.SetActive(true);
        }
        else
        {
            library.gameObject.SetActive(false);
            quiz.gameObject.SetActive(false);
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.D && e.control && e.alt)
        {
            displayToggle.TestMode = !displayToggle.TestMode;
            if (displayToggle.TestMode)
            {
                library.gameObject.SetActive(true);
                quiz.gameObject.SetActive(true);
            }
            else
            {
                if (!displayToggle.Done)
                {
                    library.gameObject.SetActive(false);
                    quiz.gameObject.SetActive(false);
                }
            }
        }
    }

}
