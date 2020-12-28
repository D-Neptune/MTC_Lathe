using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DummyColliderProxy : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent clickEvent, hoverEvent, exitEvent;

    private void OnMouseDown()
    {
        clickEvent.Invoke();
    }

    private void OnMouseOver()
    {
        hoverEvent.Invoke();

    }

    private void OnMouseExit()
    {
        exitEvent.Invoke();

    }
}
