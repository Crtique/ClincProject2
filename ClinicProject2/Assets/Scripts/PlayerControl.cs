using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    // -- Declare Variables --
    [SerializeField] float speed = 100f;
    [SerializeField] float movement = 10f;
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float gravity = -9.81f;

    [Header("Checks")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Header("Controller")]
    [SerializeField] CharacterController controller;

    private float leftBound = -4.4f;
    private float rightBound = 4.4f;

    Vector3 velocity;
    bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        PlayerMove();
        Jump();
    }

    // -- Player Movement --
    void PlayerMove()
    {
        //-- Check if the player is near the ground by creating a sphere and seeing how close it is from the ground --
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // -- Move Player object forward --
        controller.Move(Vector3.forward * Time.fixedDeltaTime * speed);

        // -- Move Left --
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // -- Prevent player character from falling off the edge --
            if (transform.position.x > leftBound)
            {
                controller.Move(Vector3.left * Time.fixedDeltaTime * movement);
            }
        }
        // -- Move right --
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightBound)
            {
                controller.Move(Vector3.right * Time.fixedDeltaTime * movement);
            }
        }

        //-- Apply gravity --
        velocity.y += gravity * Time.deltaTime;

        //-- Apply the Velocity when gravity happens --
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
