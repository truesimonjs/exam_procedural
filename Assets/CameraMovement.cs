using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 10f;

    // Bool to toggle between camera movement and UI interaction
    private bool isCameraMovementEnabled = true;

    // Update is called once per frame
    void Update()
    {
        // Check if the specific key (e.g., the spacebar) is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle the flag
            isCameraMovementEnabled = !isCameraMovementEnabled;

            // Update cursor visibility and lock state based on the flag
            Cursor.visible = !isCameraMovementEnabled;
            Cursor.lockState = isCameraMovementEnabled ? CursorLockMode.Confined : CursorLockMode.None;
        }

        // Check if camera movement is enabled
        if (isCameraMovementEnabled)
        {
            // Get mouse input
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            // Calculate movement direction
            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

            // Adjust movement based on camera rotation
            moveDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * moveDirection;

            // Move the camera
            transform.Translate(moveDirection * panSpeed * Time.deltaTime, Space.World);
        }

    }
}
