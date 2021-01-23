using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldShorcuts : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject InputPanel;
    [SerializeField] private InputField firstInputField;
    [SerializeField] private InputField secondInputField;
    [SerializeField] private Button sumbitBTN;
    private EventSystem system;
    private void Start()
    {
        system = EventSystem.current;
    }
    private void OnGUI()
    {
        if (InputPanel.activeSelf)
        {
            Event e = Event.current;
            if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Tab)
            {
                if (system.currentSelectedGameObject.Equals(firstInputField.gameObject))
                {
                    system.SetSelectedGameObject(secondInputField.gameObject, new BaseEventData(system));
                }
            }
            else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
            {
                sumbitBTN.onClick.Invoke();
            }
        }
    }



}
