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
    [SerializeField] private float speed;
    public float sprintSpeed;
    public float jumpForce;
    public float groundPoundForce;
    public float pickUpRange;

    
    private Vector3 movement;
    private Rigidbody playerRigidbody;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isGroundPounding;

    private bool isHoldingObject = false;
    private GameObject pickedUpObject;
    public Transform pickupRaycastStart;
    public float pickupRaycastDistance = 5f;

    private Vector3 direction = Vector3.zero;
    private Quaternion targetRotation;
    private float rotationSpeed = 10.0f;

    private Animator _anim;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        speed = startSpeed;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Sprint();
        Jump();
        GroundPound();
        //PickUpObject();
        //DropObject();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingObject)
            {
                PickUpObject(); 
            }
            else
            {
                DropObject();
            }
        }
    }

    void Move(float h, float v)
    {


        direction = new Vector3(h, 0, v);

        if (direction.magnitude > 0.1f)
        {
            targetRotation = Quaternion.LookRotation(direction);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        ////Rigidbody based movement
        //Vector3 movement = new Vector3(h, 0f, v);
        //playerRigidbody.AddForce(movement * speed);

        //Transform.translate based movement
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = startSpeed;
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            //playerRigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.VelocityChange);
            //playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void GroundPound()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerRigidbody.AddForce(Vector3.down * groundPoundForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void PickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(pickupRaycastStart.position, pickupRaycastStart.forward, out hit, pickupRaycastDistance))
        {
            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.CompareTag("Pickup"))
            {
                pickedUpObject = hit.transform.gameObject;

                pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
                pickedUpObject.GetComponent<Rigidbody>().useGravity = false;
                pickedUpObject.GetComponent<BoxCollider>().enabled = false;
                pickedUpObject.transform.position = transform.position + new Vector3(0, 2, 1.5f);
                pickedUpObject.transform.parent = transform;
                isHoldingObject = true;
            }
        }
    }

    void DropObject()
    {
        if (Input.GetKeyDown(KeyCode.E) && isHoldingObject)
        {
            pickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedUpObject.GetComponent<Rigidbody>().useGravity = true;
            pickedUpObject.GetComponent<BoxCollider>().enabled = true;
            pickedUpObject.transform.parent = null;
            isHoldingObject = false;
        }
    }
}
