using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{


    [Header("Player Settings")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float jetpackForce = 5.0f;
    [SerializeField] private float sprintSpeedMultiplier = 1.5f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;
    

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float standHeight = 1.0f;
    [SerializeField] private float crouchHeight = 0.5f;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrain = 10f;
    [SerializeField] private float staminaRegenRate = 5f;
    [SerializeField] private float jumpStaminaCost = 20f;
    [SerializeField] private float jetpackStaminaCost = 5f;


    private float currentStamina;
    private bool isGrounded = true;
    private Rigidbody rb;
    private Vector2 movementInput = Vector2.zero;
    public bool isSprinting = false;
    private bool isCrouching = false;
    private bool jumped = false;


    [SerializeField] private Image staminaBarImage = null;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }


    //OnJump PERSO
    public void OnJump(InputAction.CallbackContext context)
    {
        if (currentStamina >= jumpStaminaCost && isGrounded && context.phase == InputActionPhase.Started)
        {
            Debug.Log("Jump");
            jumped = context.ReadValue<float>() > 0.0f;
            if (jumped) currentStamina -= jumpStaminaCost;
            isGrounded = false;

            //TEST
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (currentStamina >= jetpackStaminaCost && !isGrounded && context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Jetpack");
            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Impulse);
            currentStamina -= jetpackStaminaCost;
            jumped = false;
        }


    }


    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValue<float>() > 0.0f;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValue<float>() > 0.0f;
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.transform.parent.tag == "Map")
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
        movement = transform.TransformDirection(movement);

        if (isSprinting && currentStamina > 0)
        {
            movement *= playerSpeed * sprintSpeedMultiplier;
            currentStamina -= staminaDrain * Time.fixedDeltaTime;
        }
        else
        {
            movement *= playerSpeed * (isCrouching ? crouchSpeedMultiplier : 1);
            if (currentStamina < maxStamina)
                currentStamina += staminaRegenRate * Time.fixedDeltaTime;
        }

        rb.AddForce(movement, ForceMode.Acceleration);

        if (isCrouching)
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, crouchHeight, cameraTransform.localPosition.z);
        }
        else
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, standHeight, cameraTransform.localPosition.z);
        }
        UpdateStaminaBar();
        jumped = false;
    }

    private void UpdateStaminaBar()
    {
        if (staminaBarImage != null)
        {
            staminaBarImage.fillAmount = currentStamina / maxStamina;
        }
    }

}
