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

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    [HideInInspector]
    public bool canMove = true;

    public Animator _anim;

    private bool isHoldingObject = false;
    private GameObject pickedUpObject;
    public Transform pickupRaycastStart;
    public float pickupRaycastDistance = 5f;

    private float rotationSpeed = 100.0f;

    private int smashDropHeight = 500;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        characterController.SimpleMove(forward + right); // Add right vector

        if (Input.GetKey(KeyCode.LeftShift))  // check if sprinting key is pressed
        {
            walkingSpeed = runningSpeed;  // if so, set movement speed to sprinting speed
        }
        else
        {
            walkingSpeed = 5.5f;
        }

        // Play walk animation when moving
        if (forward != Vector3.zero)
        {
            _anim.SetBool("Walk", true);
        }
        else
        {
            _anim.SetBool("Walk", false);
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
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
        characterController.Move(moveDirection * Time.deltaTime);

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
