using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    public float startSpeed;
    private float speed;
    public float sprintSpeed;
    public float jumpForce;
    public float groundPoundForce;

    private Vector3 movement;
    private Rigidbody playerRigidbody;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isSprinting;
    [SerializeField] private bool isGroundPounding;

    private PlayerState playerState;

    private enum PlayerState
    {
        IDLE,
        RUNNING,
        SPRINTING,
        JUMPING,
        GROUNDPOUNDING
    }

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        transform.Translate(movement, Space.World);
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
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
        if (!isGrounded && Input.GetKeyDown(KeyCode.LeftControl))
        {
            isGroundPounding = true;
        }
        if (isGroundPounding)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, -groundPoundForce, 0f), ForceMode.Impulse);
            isGroundPounding = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
