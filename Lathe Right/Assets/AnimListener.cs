using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AnimListener : MonoBehaviour
{
    [SerializeField] private AnimController controller;
    [SerializeField] private Animator animator;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private TriggerDialogueInterface triggerDialogue;
    private int counter = 0;

    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer.loopPointReached += ActivateAnimator;

    }

    public void ActivateAnimator(VideoPlayer videoPlayer)
    {
        Debug.Log("ActovateAnim");
        if (counter == 0)
        {
            controller.ResetHint();
            animator.enabled = true;
        }
    }
}
