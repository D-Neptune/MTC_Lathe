using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCylinderStock : MonoBehaviour
{

    private Vector3 init_pos;
    private Quaternion init_rot;
    [SerializeField] private Animator controller;
    [SerializeField] private string StockTag;
    [SerializeField] private float speed;
    [SerializeField] private Transform follow;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

    private void Start()
    {
        init_pos = follow.localPosition;
        init_rot = follow.localRotation;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        updateStaticRigidBody();
    }

    public void updateStaticRigidBody()
    {
        transform.localPosition = init_pos + offset;
        transform.localRotation = init_rot;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == StockTag)
        {
            controller.SetFloat("Speed", 0);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == StockTag)
        {
            controller.SetFloat("Speed", 0);
        }
    }

    public void EndCollision()
    {
        controller.SetFloat("Speed", speed);
    }
}
