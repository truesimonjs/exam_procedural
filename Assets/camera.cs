using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    //movement stats
    public float jumpPower;
    public float speed;
    public float mouseSens = 5;
    float camXRotation = 0;
    public Transform cam;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime, Space.Self);
        //move camera up and down 
        camXRotation += -mouseSens * Time.deltaTime * Input.GetAxis("Mouse Y");
        camXRotation = Mathf.Clamp(camXRotation, -90, 90);
        cam.localRotation = Quaternion.Euler(camXRotation, cam.localEulerAngles.y, cam.localEulerAngles.z);
        //move wasd
        Vector3 movement = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal") + transform.up * Input.GetAxis("Flight")).normalized;
        transform.position += movement * Time.deltaTime * speed;
    }
}
