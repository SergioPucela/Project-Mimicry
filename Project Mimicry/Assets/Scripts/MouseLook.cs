using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    private float xRotation = 0f;
    private float yRotation = 0f;

    [SerializeField] float xLockAngle = 60f;
    [SerializeField] float yLockAngle = 60f;

    [SerializeField] float mouseSensitivity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -xLockAngle, xLockAngle);
        yRotation = Mathf.Clamp(yRotation, -yLockAngle, yLockAngle);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
