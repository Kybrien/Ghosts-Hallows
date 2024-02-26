using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.GroundActions ground;

    private PlayerMotor motor;
    private PlayerLook look;

    void Awake()
    {
        playerInput = new PlayerInput();
        ground = playerInput.Ground;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        ground.Jump.performed += ctx => motor.Jump();
        ground.Crouch.performed += ctx => motor.Crouch();
        ground.Sprint.performed += ctx => motor.Sprint();
    }

    void FixedUpdate()
    {
        motor.ProcessMove(ground.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(ground.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        ground.Enable();
    }

    private void OnDisable()
    {
        ground.Disable();
    }
}
