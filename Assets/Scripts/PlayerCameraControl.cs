using System;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{

    [SerializeField] float sensitivity;
    private float xRotation = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.Rotate(new Vector3(0, mouseX, 0));
    }
}
