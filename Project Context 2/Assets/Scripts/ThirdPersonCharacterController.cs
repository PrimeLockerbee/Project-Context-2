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

    private PlayerState playerState = PlayerState.IDLE;

    private bool isHoldingObject = false;
    private GameObject pickedUpObject;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        speed = startSpeed;
    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerState.IDLE:
                Move();
                Sprint();
                Jump();
                GroundPound();
                PickUpObject();
                break;
            case PlayerState.RUNNING:
                Move();
                Sprint();
                Jump();
                GroundPound();
                PickUpObject();
                break;
            case PlayerState.SPRINTING:
                Move();
                Jump();
                break;
            case PlayerState.GROUNDPOUND:
                GroundPound();
                break;
            case PlayerState.HOLDINGOBJECT:
                Move();
                Jump();
                DropObject();
                break;
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = (horizontal * transform.right + vertical * transform.forward).normalized;
        transform.position += movement * speed * Time.deltaTime;
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerState != PlayerState.GROUNDPOUND)
        {
            playerState = PlayerState.SPRINTING;
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerState = PlayerState.RUNNING;
            speed = 5f;
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
        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.HOLDINGOBJECT)
        {
            playerState = PlayerState.GROUNDPOUND;
            playerRigidbody.AddForce(Vector3.down * groundPoundForce, ForceMode.Impulse);
        }
        else if (playerState == PlayerState.GROUNDPOUND && !Input.GetKey(KeyCode.Space))
        {
            playerState = PlayerState.IDLE;
        }
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
            }
            pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
            pickedUpObject.transform.position = transform.position + transform.forward;
            pickedUpObject.transform.parent = transform;
            isHoldingObject = true;
            playerState = PlayerState.HOLDINGOBJECT;
        }
    }

    void DropObject()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isHoldingObject)
        {
            pickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedUpObject.transform.parent = null;
            isHoldingObject = false;
            playerState = PlayerState.IDLE;
        }
    }
}
