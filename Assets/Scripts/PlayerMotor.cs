using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool playerOnGround;
    private bool lerpCrouch;
    private bool crouching;
    private bool sprinting;
    private float crouchTimer;

    public float playerSpeed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float sprintSpeed = 8.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController not found");
        }
    }

    void Update()
    {
        playerOnGround = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1; // Durée de l'interpolation
            controller.height = Mathf.Lerp(controller.height, crouching ? crouchHeight : standHeight, p);
            if (p >= 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(transform.TransformDirection(moveDirection) * Time.deltaTime * playerSpeed);
        playerVelocity.y += gravity * Time.deltaTime;
        if (playerOnGround && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (playerOnGround)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        lerpCrouch = true;
        crouchTimer = 0;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        playerSpeed = sprinting ? sprintSpeed : 5.0f;
    }
}
