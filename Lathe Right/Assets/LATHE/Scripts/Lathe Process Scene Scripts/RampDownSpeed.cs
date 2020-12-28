using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampDownSpeed : MonoBehaviour
{
    [SerializeField] private ProcessController operation;
    [SerializeField] private List<Animator> animators;
    [SerializeField] private string rotationParamName;
    [SerializeField] private float speedDecremmentRate;
    private float speed;
    float timer = 0;
    float timeToWait = 0.1f;
    bool checkingTime;
    bool timerDone;
    bool Cooldown = false;

    private Animator currentAnimator;

    
    public void RampDown()
    {
        timer = 0;
        timeToWait = 0.1f;
        timerDone = false;
        currentAnimator = animators[operation.Index];
        speed = currentAnimator.GetFloat(rotationParamName);
        Cooldown = true;
    }

    private void LateUpdate()
    {
        if (Cooldown)
        {
            if (timerDone)
            {
                speed -= speedDecremmentRate;
                currentAnimator.SetFloat(rotationParamName, speed);
                timerDone = false;
            }
            else if (timer >= timeToWait)
            {
                timerDone = true;
                timer = 0;
            }

            if (speed < 0)
            {
                Cooldown = false;
            }

            timer += Time.deltaTime;
        }
    }



}
