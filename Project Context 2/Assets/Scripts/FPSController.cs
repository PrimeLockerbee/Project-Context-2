using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.QuestMachine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float walkingspeedreturn;

    CharacterController characterController;
    private Vector3 moveDirection;

    [HideInInspector]
    public bool canMove = true;

    public Animator _anim;

    public bool isHoldingObject = false;
    public GameObject pickedUpObject;
    public Transform pickupRaycastStart;
    public float pickupRaycastDistance = 5f;

    private float rotationSpeed = 100.0f;

    private int smashDropHeight = 500;

    

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        walkingspeedreturn = walkingSpeed;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal"); // Add this line

        // Rotate the character using mouse position
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

        // Move the character forward and backward using W and S keys
        Vector3 forward = transform.forward * v * walkingSpeed;
        Vector3 right = transform.right * h * walkingSpeed; // Add this line
        //characterController.SimpleMove(forward + right); // Add right vector

         moveDirection = (forward + right);

        if (Input.GetKey(KeyCode.LeftShift) && v != 0)  // check if sprinting key is pressed
        {
            walkingSpeed = runningSpeed;  // if so, set movement speed to sprinting speed
        }
        else
        {
            walkingSpeed = walkingspeedreturn;
        }


        /*
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            _anim.SetTrigger("Jump");
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        */
        moveDirection.y -= smashDropHeight;

        if (Input.GetButtonDown("Jump"))
        {
            _anim.SetTrigger("GroundPound");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, /*smashRadius*/ 2);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    Destroy(hitCollider.gameObject);     
                }
            }
            moveDirection.y -= smashDropHeight;
        }


        // Move the controller
        characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingObject)
            {
                PickUpObject();
                _anim.SetTrigger("Interact");
            }
            else
            {
                DropObject();

            }
        }

        Move();
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

                Debug.Log("Picked up object: " + pickedUpObject.name);
            }
        }
        else
        {
            Debug.Log("Did not detect any pickup objects.");
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
            pickedUpObject = null;
        }
    }

    private void Move()

    {
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);

        if (Input.GetAxisRaw("Vertical") != 0 && walkingSpeed != runningSpeed)
        {
            Walk();
        }
        else if (walkingSpeed == runningSpeed)
        {
            Run();
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            Idle();
        }
    }

    private void Idle()
    {
        _anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void Walk()
    {
        _anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        _anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

}
