using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed;
    private bool isGrounded;
    private PlayerMotor motor;

    public GameObject weopon;
    public Joystick joystick;

	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
    }
	
	// Update is called once per frame
	void Update () {
        playerMovement();
    }

    private void playerMovement()
    {
        speed = 10f;

        float horizontalMovement = joystick.Horizontal * speed;

        float verticalMovement = joystick.Vertical * speed;

        Vector3 velocity = new Vector3(horizontalMovement, 0f, verticalMovement).normalized * speed;

        motor.Move(velocity);

        Vector3 lookDirection = new Vector3 (horizontalMovement, 0f, verticalMovement);

        motor.Rotate(lookDirection);

        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
            motor.PerformJump();
        }
    }
    //Check if player can jump
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
