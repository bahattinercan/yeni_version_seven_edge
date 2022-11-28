using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingTest : MonoBehaviour
{
    public float jumpSpeed = 5f;//or whatever you want it to be
    public Rigidbody rb; //and again, whatever you want to call it

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpSpeed);
        }

    }
}
