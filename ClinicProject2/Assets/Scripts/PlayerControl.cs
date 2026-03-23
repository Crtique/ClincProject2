using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement Checks")]
    [SerializeField] float speed = 100f;
    [SerializeField] float movement = 10f;

    [Header("Jump Checks")]
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float gravity = -9.81f;

    [Header("Crounch Checks")]
    [SerializeField] float originalHeight;
    [SerializeField] float crouchHeight = 2f;
    [SerializeField] Vector3 originalCenter;

    [Header("Ground Checks")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Header("Controller")]
    [SerializeField] CharacterController controller;

    // -- Declare Variables --
    private float leftBound = -4.4f;
    private float rightBound = 4.4f;

    Vector3 velocity;

    bool isGrounded;
    public bool isCrouching;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        originalHeight = controller.height;
        originalCenter = controller.center;
    }

    void Update()
    {
        PlayerMove();
        Jump();
        Crouch();
    }

    // -- Player Movement Function -- //
    void PlayerMove()
    {
        //-- Check if the player is near the ground by creating a sphere and seeing how close it is from the ground --
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // -- Move Player object forward --
        controller.Move(Vector3.forward * Time.deltaTime * speed);

        // -- Move Left --
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // -- Prevent player character from falling off the edge --
            if (transform.position.x > leftBound && isGrounded)
            {
                controller.Move(Vector3.left * Time.deltaTime * movement);
            }
        }
        // -- Move right --
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightBound && isGrounded)
            {
                controller.Move(Vector3.right * Time.deltaTime * movement);
            }
        }

        //-- Apply gravity --
        velocity.y += gravity * Time.deltaTime;

        //-- Apply the Velocity when gravity happens --
        controller.Move(velocity * Time.deltaTime);
    }
    // -- Player Jump Function -- //
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
    // Make the player "Slide"
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCrouching = true;
            controller.height = crouchHeight;
            controller.center = new Vector3(originalCenter.x, crouchHeight / 2, originalCenter.z);        
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
            controller.center = originalCenter;
            controller.height = originalHeight;
        }
    }
}
