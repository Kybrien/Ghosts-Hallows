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
    [SerializeField] private float fastFallForce = 10.0f;


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
    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool isJetpackActive = false;

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
        switch (context.phase)
        {
            case InputActionPhase.Started:
                // Le joueur démarre un saut
                if (isGrounded && currentStamina >= jumpStaminaCost)
                {
                    Debug.Log("Jump");
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    currentStamina -= jumpStaminaCost;
                    isGrounded = false;
                }
                break;

            case InputActionPhase.Performed:
                // Le joueur active le jetpack après avoir commencé à sauter
                if (!isGrounded && currentStamina >= jetpackStaminaCost)
                {
                    Debug.Log("Jetpack");
                    isJetpackActive = true;
                }
                break;

            case InputActionPhase.Canceled:
                // Le joueur relâche la touche, désactivant le jetpack
                isJetpackActive = false;
                break;
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

    void OnCollisionEnter(Collision collision)
    {
        /*if(collision.transform.parent.tag == "Map")
        {
            isGrounded = true;
        }*/
        if (collision.transform.parent != null && collision.transform.parent.tag == "Map")
        {
            isGrounded = true;
        }
        else if (collision.gameObject.tag == "Map")
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

        if(isJetpackActive && currentStamina > 0)
        {
            //Copilot
            /*rb.AddForce(Vector3.up * jetpackForce, ForceMode.Acceleration);
            currentStamina -= jetpackStaminaCost * Time.fixedDeltaTime;*/

            //Base
            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Impulse);
            currentStamina -= jetpackStaminaCost;
        }
        if (!isGrounded && isCrouching)
        {
            Debug.Log("FAST FALL");
            rb.AddForce(Vector3.down * fastFallForce, ForceMode.Acceleration);
        }


        UpdateStaminaBar();
    }

    private void UpdateStaminaBar()
    {
        if (staminaBarImage != null)
        {
            staminaBarImage.fillAmount = currentStamina / maxStamina;
        }
    }

}
