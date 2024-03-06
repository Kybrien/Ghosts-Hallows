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
    [SerializeField] private float defaultFOV = 70f;  // FOV par défaut
    [SerializeField] private float sprintFOV = 85f;   // FOV lors du sprint
    [SerializeField] private float fovChangeSpeed = 10f;
    [SerializeField] private float standHeight = 1.0f;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private Transform arrowTransform;


    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrain = 10f;
    [SerializeField] private float staminaRegenRate = 5f;
    [SerializeField] private float jumpStaminaCost = 20f;
    [SerializeField] private float jetpackStaminaCost = 5f;

    [Header("Appearence Settings")]
    [SerializeField] private GameObject landingFXPrefab;
    [SerializeField] private Image staminaBarImage = null;

    private Rigidbody rb;
    private float currentStamina;
    private bool isGrounded = true;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool isFastFalling = false;
    private bool isJetpackActive = false;
    private Vector2 movementInput = Vector2.zero;
    //GPT
    private Camera playerCamera; // Ajoutez un champ pour la caméra



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        playerCamera = cameraTransform.GetComponent<Camera>(); // Initialiser la caméra
        playerCamera.fieldOfView = defaultFOV;// Initialiser le FOV
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
                    /*Debug.Log("Jump");*/
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    currentStamina -= jumpStaminaCost;
                    isGrounded = false;
                }
                break;

            case InputActionPhase.Performed:
                // Le joueur active le jetpack après avoir commencé à sauter
                if (!isGrounded && currentStamina >= jetpackStaminaCost)
                {
                    /*Debug.Log("Jetpack");*/
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
        //On active isFastFalling que si la touche est maintenue dans les airs
        if (!isGrounded && isCrouching)
        {
            isFastFalling = true;
        }
        else
        {
            isFastFalling = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
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

        if (isSprinting && playerCamera.fieldOfView < sprintFOV)
        {
            // Augmenter progressivement le FOV jusqu'à atteindre sprintFOV
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, fovChangeSpeed * Time.deltaTime);
        }
        else if (!isSprinting && playerCamera.fieldOfView > defaultFOV)
        {
            // Diminuer progressivement le FOV jusqu'à atteindre defaultFOV
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, defaultFOV, fovChangeSpeed * Time.deltaTime);
        }

        rb.AddForce(movement, ForceMode.Acceleration);

        if (isCrouching)
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, crouchHeight, cameraTransform.localPosition.z);
            arrowTransform.localPosition = new Vector3(arrowTransform.localPosition.x, 0, arrowTransform.localPosition.z);

        }
        else
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, standHeight, cameraTransform.localPosition.z);
            arrowTransform.localPosition = new Vector3(arrowTransform.localPosition.x, 0.5f, arrowTransform.localPosition.z);
        }

        if (isJetpackActive && currentStamina > 0)
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
            isFastFalling = true;
            rb.AddForce(Vector3.down * fastFallForce, ForceMode.Acceleration);

        }
        if (isFastFalling && isGrounded)
        {
            Instantiate(landingFXPrefab, transform.position, Quaternion.identity);
            isFastFalling = false;
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
