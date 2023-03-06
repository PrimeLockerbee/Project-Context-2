using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    float originalHeight;
    float crouchHeight = 0.5f;
    bool isCrouching = false;
    public float crouchSpeed = 3.0f;

    [HideInInspector]
    public bool canMove = true;

    private Animator _anim;

    private bool isHoldingObject = false;
    private GameObject pickedUpObject;
    public Transform pickupRaycastStart;
    public float pickupRaycastDistance = 5f;

    private float rotationSpeed = 10.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //// Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // Save the original height of the character controller
        originalHeight = characterController.height;

        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouchKeyPressed = Input.GetKey(KeyCode.C);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (isCrouchKeyPressed && !isCrouching)
        {
            // Crouch
            isCrouching = true;
            characterController.height = crouchHeight;
            curSpeedX = crouchSpeed * Input.GetAxis("Vertical");
            curSpeedY = crouchSpeed * Input.GetAxis("Horizontal");
        }
        else if (!isCrouchKeyPressed && isCrouching)
        {
            // Stand up
            isCrouching = false;
            characterController.height = originalHeight;
        }


        if (curSpeedX == 0 && curSpeedY == 0)
        {
            _anim.SetBool("Walk", false);
        }
        else
        {
            _anim.SetBool("Walk", true);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = hit.point;

            // Calculate the direction from the character to the mouse
            Vector3 directionToMouse = (mousePosition - transform.position).normalized;

            // Calculate the target rotation for the character
            Quaternion targetRotation = Quaternion.LookRotation(directionToMouse);

            // Smoothly rotate the character towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
