using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private Vector3 moveDirection = Vector3.zero;

    //Chat GPT
    [SerializeField] private float sprintSpeedMultiplier = 1.5f; // Define a sprint speed multiplier
    private bool isSprinting = false;
    private bool isCrouching = false;

    [SerializeField] private Transform cameraTransform; // Assign your camera's transform here in the inspector
    [SerializeField] private float standHeight = 1.0f; // Standard height of the camera
    [SerializeField] private float crouchHeight = 0.5f; // Height of the camera while crouching
    private float loweringSpeed = 0.4f; // Speed at which the camera lowers/raises to match the crouch/stand height




    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    private Vector2 movementInput = Vector2.zero;
    private bool jumped;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.ReadValue<float>() > 0.0f; // Lecture de la valeur du contexte et conversion en booléen
    }

    //Chat GPT
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValue<float>() > 0.0f; // Check if sprint button is pressed
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValue<float>() > 0.0f; // Check if crouch button is pressed
                                                         // Here, you would also handle changing the character's collider size or position for crouching
    }



    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        moveDirection = (forward * movementInput.y) + (right * movementInput.x);
        controller.Move(moveDirection * Time.deltaTime * playerSpeed);



        //Chat GPT
        if (isSprinting)
        {
            controller.Move(moveDirection * Time.deltaTime * playerSpeed * sprintSpeedMultiplier);
        }
        else
        {
            controller.Move(moveDirection * Time.deltaTime * playerSpeed);
        }

        if (isCrouching)
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, crouchHeight, cameraTransform.localPosition.z);
            controller.Move(moveDirection * Time.deltaTime * playerSpeed * loweringSpeed);
        }
        else
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, standHeight, cameraTransform.localPosition.z);
        }


        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        //permet de gerer le saut
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
