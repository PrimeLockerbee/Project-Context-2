using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE,
    RUNNING,
    SPRINTING,
    GROUNDPOUND,
    HOLDINGOBJECT
}

public class ThirdPersonCharacterController : MonoBehaviour
{
    public float startSpeed;
    private float speed;
    public float sprintSpeed;
    public float jumpForce;
    public float groundPoundForce;
    public float pickUpRange;

    
    private Vector3 movement;
    private Rigidbody playerRigidbody;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isGroundPounding;

    //[SerializeField] private PlayerState playerState = PlayerState.IDLE;

    private bool isHoldingObject = false;
    private GameObject pickedUpObject;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        speed = startSpeed;
    }

    void Update()
    {
        //switch (playerState)
        //{
        //    case PlayerState.IDLE:
        //        Move();
        //        Sprint();
        //        Jump();
        //        GroundPound();
        //        PickUpObject();
        //        break;
        //    case PlayerState.RUNNING:
        //        Move();
        //        Sprint();
        //        Jump();
        //        GroundPound();
        //        PickUpObject();
        //        break;
        //    case PlayerState.SPRINTING:
        //        Move();
        //        Jump();
        //        break;
        //    case PlayerState.GROUNDPOUND:
        //        GroundPound();
        //        break;
        //    case PlayerState.HOLDINGOBJECT:
        //        Move();
        //        Jump();
        //        DropObject();
        //        break;
        //}

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Sprint();
        Jump();
        GroundPound();
        PickUpObject();
        DropObject();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) /*&& playerState != PlayerState.GROUNDPOUND*/)
        {
            //playerState = PlayerState.SPRINTING;
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //playerState = PlayerState.RUNNING;
            speed = startSpeed;
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void GroundPound()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) /*&& playerState != PlayerState.HOLDINGOBJECT*/)
        {
            //playerState = PlayerState.GROUNDPOUND;
            playerRigidbody.AddForce(Vector3.down * groundPoundForce, ForceMode.Impulse);
        }
        //else if (playerState == PlayerState.GROUNDPOUND && !Input.GetKey(KeyCode.Space))
        //{
        //    playerState = PlayerState.IDLE;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void PickUpObject()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHoldingObject)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange))
            {
                pickedUpObject = hit.transform.gameObject;

                pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
                pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
                pickedUpObject.GetComponent<BoxCollider>().enabled = false;
                pickedUpObject.transform.position = transform.position + transform.forward;
                pickedUpObject.transform.parent = transform;
                isHoldingObject = true;
                //playerState = PlayerState.HOLDINGOBJECT;
            }

            if (Physics.Raycast(transform.position, -transform.forward, out hit, pickUpRange))
            {
                pickedUpObject = hit.transform.gameObject;

                pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
                pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
                pickedUpObject.GetComponent<BoxCollider>().enabled = false;
                pickedUpObject.transform.position = transform.position + transform.forward;
                pickedUpObject.transform.parent = transform;
                isHoldingObject = true;
                //playerState = PlayerState.HOLDINGOBJECT;
            }

            if (Physics.Raycast(transform.position, transform.right, out hit, pickUpRange))
            {
                pickedUpObject = hit.transform.gameObject;

                pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
                pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
                pickedUpObject.GetComponent<BoxCollider>().enabled = false;
                pickedUpObject.transform.position = transform.position + transform.forward;
                pickedUpObject.transform.parent = transform;
                isHoldingObject = true;
                //playerState = PlayerState.HOLDINGOBJECT;
            }

            if (Physics.Raycast(transform.position, -transform.right, out hit, pickUpRange))
            {
                pickedUpObject = hit.transform.gameObject;

                pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
                pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
                pickedUpObject.GetComponent<BoxCollider>().enabled = false;
                pickedUpObject.transform.position = transform.position + transform.forward;
                pickedUpObject.transform.parent = transform;
                isHoldingObject = true;
                //playerState = PlayerState.HOLDINGOBJECT;
            }
        }
    }

    void DropObject()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isHoldingObject)
        {
            pickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
            pickedUpObject.GetComponent<BoxCollider>().enabled = true;
            pickedUpObject.transform.parent = null;
            isHoldingObject = false;
            //playerState = PlayerState.IDLE;
        }
    }
}
