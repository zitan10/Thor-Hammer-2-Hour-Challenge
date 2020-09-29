using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class Hammer : MonoBehaviour {

    new Collider collider;

    [SerializeField]
    private GameObject _hand;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _handHammer;

    private Quaternion _originalHammerRotation;
    private Vector3 _throwPosition;

    private States _presentState;
    public States presentState => _presentState;
   
    public enum States
    {
        playersHand,
        hammerThrow,
        hammerThrown,
        hammerRecall
    }

	// Use this for initialization
	void Start () {
        collider = GetComponent<BoxCollider>();
        _originalHammerRotation = transform.rotation;
        _presentState = States.playersHand;
        collider.enabled = false;
    }

	// Update is called once per frame
	void Update () {

        //Throw Hammer State
        if (CrossPlatformInputManager.GetButtonDown("Range") && presentState == States.playersHand)
        {
            this.transform.position = player.transform.position + new Vector3(0f,7f,0f);
            _presentState = States.hammerThrow;
            _throwPosition = (player.transform.forward * 30f) + new Vector3(0f, player.transform.position.y + 3f ,0f);
        }
        //Recall Hammer State
        if (CrossPlatformInputManager.GetButtonDown("Range") && (presentState == States.hammerThrown))
        {
            _presentState = States.hammerRecall;
        }

        switch (_presentState)
        {
            default:
            case States.playersHand:
                _handHammer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                transform.localScale = new Vector3(0f, 0f, 0f);
                break;

            case States.hammerThrow:
                collider.enabled = true;
                _handHammer.transform.localScale = new Vector3(0f, 0f, 0f);
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                HammerThrow();
                break;

            case States.hammerThrown:
                _handHammer.transform.localScale = new Vector3(0f, 0f, 0f);
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                break;

            case States.hammerRecall:
                _handHammer.transform.localScale = new Vector3(0f, 0f, 0f);
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                HammerRecall();
                break;
        }
	}

    private void HammerThrow()
    {
        if(Vector3.Distance(transform.position, _throwPosition) < 1)
        {
            _presentState = States.hammerThrown;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _throwPosition, 100f * Time.deltaTime);
            transform.Rotate(0f, 0f, 45f, Space.Self);
        }
    }

    private void HammerRecall()
    {
        transform.position = Vector3.MoveTowards(transform.position, _hand.transform.position, 100f*Time.deltaTime);
        if (transform.position == _hand.transform.position)
        {
            collider.enabled = false;
            _presentState = States.playersHand;
        }
    }
}
