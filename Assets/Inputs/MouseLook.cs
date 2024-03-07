using UnityEngine;
using UnityEngine.InputSystem;

namespace Polyperfect.Universal
{
    public class MouseLook : MonoBehaviour
    {

        public float mouseSensitivity = 100f;
        public float controllerSensitivity = 100f;
        public Transform playerBody;
        float xRotation = 0f;

        private Vector2 joystickInput = Vector2.zero;

        private PlayerInput playerInput;
        public int playerID;


        void Start()
        {
            playerInput = GetComponent<PlayerInput>();

            Cursor.lockState = CursorLockMode.Locked;
            playerID = PlayerManager.Instance.RegisterPlayer(transform.parent.gameObject);
        }

        void Update()
        {
            
            if (playerID == 1)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 50;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 50;

                ApplyRotation(mouseX, mouseY);
            }
            if (playerID == 2 || playerID == 3 || playerID == 4)
            {
                float joystickX = joystickInput.x * controllerSensitivity * Time.deltaTime;
                float joystickY = joystickInput.y * controllerSensitivity * Time.deltaTime;

                ApplyRotation(joystickX, joystickY);
            }

        }
        void ApplyRotation(float x, float y)
        {
            xRotation -= y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * x);
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            joystickInput = context.ReadValue<Vector2>();
        }
    }
}