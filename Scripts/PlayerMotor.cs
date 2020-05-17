using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotate = Vector3.zero;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        PerformMovement();
        PerformRotation();
    }

    //Gets a movement vector
    public void Move(Vector3 velocityIn)
    {
        velocity = velocityIn;
    }

    //Gets a rotation vector
    public void Rotate(Vector3 RotateIn)
    {
        rotate = RotateIn;
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //Perform Rotation based on velocity variable
    void PerformRotation()
    {
        if (rotate != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(rotate, Vector3.up);
            float rotationSpeed = 5f * Time.deltaTime;
            rb.transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, rotationSpeed);
        }
    }

    public void PerformJump()
    {
        rb.AddForce(0, 7f, 0, ForceMode.Impulse);
    }
}
