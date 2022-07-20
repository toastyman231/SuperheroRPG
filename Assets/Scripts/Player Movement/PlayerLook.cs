using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Wallrun wallRun;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;
    public bool cameraEnabled = true;
    float mouseX, mouseY;
    float multiplier = 0.01f;
    float xRotation, yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (cameraEnabled)
        {
            mouseX = Mouse.current.delta.x.ReadValue();
            mouseY = Mouse.current.delta.y.ReadValue();

            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, wallRun.tilt);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
