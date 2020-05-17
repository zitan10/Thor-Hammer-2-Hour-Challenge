using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Hammer : MonoBehaviour {

    public GameObject hand;
    public Collider collider;
    public GameObject player;

    private Quaternion originalHammerRotation;
    private Rigidbody rb;
    private States presentState;
    private Vector3 throwPosition;
    

    private enum States
    {
        playersHand,
        hammerThrow,
        hammerThrown,
        hammerRecall
    }

	// Use this for initialization
	void Start () {
        originalHammerRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        presentState = States.playersHand;
        rb.detectCollisions = false;
        collider.enabled = false;
    }

	// Update is called once per frame
	void Update () {

        //Throw Hammer State
        if (CrossPlatformInputManager.GetButtonDown("Range") && presentState == States.playersHand)
        {
            presentState = States.hammerThrow;
            throwPosition = (player.transform.forward * 30f) + new Vector3(0f, player.transform.position.y,0f);
        }
        //Recall Hammer State
        if (CrossPlatformInputManager.GetButtonDown("Range") && (presentState == States.hammerThrown))
        {
            presentState = States.hammerRecall;
        }

        switch (presentState)
        {
            default:
            case States.playersHand:
                transform.position = hand.transform.position +
                    new Vector3(0f, 0.45f, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, originalHammerRotation,
                    100f * Time.deltaTime);
                break;

            case States.hammerThrow:
                //collider.enabled = true;
                //rb.detectCollisions = true;
                HammerThrow();
                break;

            case States.hammerThrown:
                //Do nothing
                break;

            case States.hammerRecall:
                HammerRecall();
                break;
        }
	}

    private void HammerThrow()
    {
        //presentState = States.hammerThrown;
        //rb.useGravity = true;
        //transform.position = transform.position + (player.transform.forward)*2; 
        //rb.AddForce(player.transform.forward * 1000f);
        if(Vector3.Distance(transform.position, throwPosition) < 1)
        {
            presentState = States.hammerThrown;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, throwPosition, 100f * Time.deltaTime);
        }
    }

    private void HammerRecall()
    {
        transform.position = Vector3.MoveTowards(transform.position, hand.transform.position, 100f*Time.deltaTime);
        if (transform.position == hand.transform.position)
        {
            rb.detectCollisions = false;
            collider.enabled = false;
            presentState = States.playersHand;
        }
    }
}
