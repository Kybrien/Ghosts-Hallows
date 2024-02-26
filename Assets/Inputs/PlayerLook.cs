using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera PlayerCamera;
    private float xRotation = 0f;

    public float XmouseSensitivity = 30f;
    public float YmouseSensitivity = 30f;
    // Start is called before the first frame update
    
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //Calculate the rotation of the camera for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * YmouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //Apply the rotation to the camera
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //rotate the player around the y axis
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * XmouseSensitivity);
        

        //Github copilot suggestion
        /*float mouseX = input.x * XmouseSensitivity * Time.deltaTime;
        float mouseY = input.y * YmouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);*/
    }
}
