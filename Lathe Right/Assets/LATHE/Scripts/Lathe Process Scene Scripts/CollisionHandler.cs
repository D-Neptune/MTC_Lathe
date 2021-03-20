using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private int timer = 1;
    [SerializeField] private ParticleSystem particle;
    private float currTime;
    // Start is called before the first frame update
    public void Start()
    {
        currTime = timer;
        CollisionEvent.current.stockMaterialCollision += CollisionOccured;
    }

    public void CollisionOccured()
    {
        particle.gameObject.SetActive(true);
        currTime = timer;
    }

    private void FixedUpdate()
    {
        if(currTime <= 0)
        {
            particle.gameObject.SetActive(false);
        }
        else
        {
            currTime -= Time.deltaTime;
        }
    }

}
