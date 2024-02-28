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
