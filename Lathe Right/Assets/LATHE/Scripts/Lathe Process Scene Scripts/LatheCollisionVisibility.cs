using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheCollisionVisibility : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    string collisionTag;

    [SerializeField]
    string gameObjectParentName;

    public bool TrainingWheels = false;

    [SerializeField] private GameObject sparks;
    [SerializeField] private float timerCountDown = 10f;
    [SerializeField] private bool reset = true;
    private bool isColliding = false;
    private float initialCountdown;
    private Renderer renderer;
    private bool staticBodyPresent, bitCollisionPresent, revertPresent;


    void Start()
    {
        renderer = GetComponent<Renderer>();
        initialCountdown = timerCountDown;

    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding == true)
        {
            sparks.SetActive(true);
            timerCountDown -= Time.deltaTime;
            if (timerCountDown < 0)
            {
                timerCountDown = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collison)
    {

        if (collison.collider.tag == collisionTag)
        {
            isColliding = true;

            sparks.transform.localPosition = collison.transform.position;
            if (gameObject.GetComponent<Renderer>().enabled)
            {
                CollisionEvent.current.StockMaterialCollision();
            }
        }
    }

    private void OnCollisionStay(Collision collison)
    {
        if (collison.collider.tag == collisionTag)
        {
            sparks.transform.position = collison.transform.position;
            if (timerCountDown <= 0)
            {
                if (TrainingWheels)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    renderer.enabled = false;
                    Destroy(gameObject);
                    Destroy(this);
                }
            }



        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == collisionTag)
        {
            if (reset)
            {
                timerCountDown = initialCountdown;
            }
            isColliding = false;
            TestCylinderStock testCylinderStock = collision.transform.gameObject.GetComponent<TestCylinderStock>();
            if (testCylinderStock != null)
            {
                testCylinderStock.EndCollision();
            }
        }
    }

    public string CollisionTag
    {
        get => collisionTag;
        set => collisionTag = value;
    }

    public float Countdown
    {
        get => initialCountdown;
        set => initialCountdown = value;
    }

    public float Timer
    {
        set => timerCountDown = value;
    }

    public string ParentName
    {
        get => gameObjectParentName;
        set => gameObjectParentName = value;
    }
}
