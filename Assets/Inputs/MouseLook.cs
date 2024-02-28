using UnityEngine;
using UnityEngine.InputSystem;

namespace Polyperfect.Universal
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 3f;
        public Transform playerBody;
        public float controller_sensitivity = 3f;
        float xRotation = 0f;

        private float joystick_direction = 0.0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            playerBody.Rotate(Vector3.up * joystick_direction * controller_sensitivity * Time.deltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var movementInput = context.ReadValue<Vector2>();
            Debug.Log(movementInput);
            joystick_direction = movementInput.x;
            
        }
    }
}
