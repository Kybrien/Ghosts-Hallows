using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float sprintSpeedMultiplier = 1.5f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float standHeight = 1.0f;
    [SerializeField] private float crouchHeight = 0.5f;
    private bool isGrounded = false;

    private Rigidbody rb;
    private Vector2 movementInput = Vector2.zero;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool jumped = false;
    private CapsuleCollider collider;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.ReadValue<float>() > 0.0f;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValue<float>() > 0.0f;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValue<float>() > 0.0f;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        movement = transform.TransformDirection(movement);
        // Réduction des valeurs de vitesse
        movement *= playerSpeed * (isSprinting ? sprintSpeedMultiplier : 1) * (isCrouching ? crouchSpeedMultiplier : 1);

        rb.AddForce(movement, ForceMode.Acceleration); // Utilisation de ForceMode.Force pour une application plus naturelle

        if (jumped && Mathf.Abs(rb.velocity.y) < 1.00f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (jumped && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // Réinitialiser isGrounded
        }

        // Gestion de la hauteur de la caméra pour l'accroupissement
        if (isCrouching)
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, crouchHeight, cameraTransform.localPosition.z);
        }
        else
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, standHeight, cameraTransform.localPosition.z);
        }
        if (Physics.Raycast(transform.position, -transform.up, collider.height * 0.5f + 0.1f))
        {
            Debug.Log("Ground");
        }
        else
        {
            Debug.Log("Air");
        }

        jumped = false; // Réinitialisation de l'état de saut
    }

}
