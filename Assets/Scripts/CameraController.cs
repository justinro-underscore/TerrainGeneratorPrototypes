using UnityEngine;

public class CameraController : MonoBehaviour {
    public float mouseSensitivity;

    private float xRotation;

    private void OnEnable() {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = transform.localEulerAngles.x;
    }

    private void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.transform.Rotate(Vector3.up * mouseX);
    }
}
