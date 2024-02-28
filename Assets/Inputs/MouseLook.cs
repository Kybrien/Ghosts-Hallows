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

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 50;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 50;

            // Apply mouse movement for rotation
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            // Apply joystick movement for rotation
            float joystickX = joystickInput.x * controllerSensitivity * Time.deltaTime;
            float joystickY = joystickInput.y * controllerSensitivity * Time.deltaTime;

            // Apply joystick Y for up and down camera rotation
            xRotation -= joystickY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Apply joystick X for left and right body rotation
            playerBody.Rotate(Vector3.up * joystickX);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            joystickInput = context.ReadValue<Vector2>();
        }
    }
}
