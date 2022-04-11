using UnityEngine;

public class MovementController : MonoBehaviour {
    public float baseSpeed;

    private CharacterController controller;

    void OnEnable() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        float x = Input.GetAxis("MovementX");
        float y = Input.GetAxis("MovementY") / 2; // Cut in half so we ascend/descend slower
        float z = Input.GetAxis("MovementZ");

        Vector3 move = transform.right * x + transform.up * y + transform.forward * z;

        float speed = baseSpeed;
        if (Input.GetButton("Speed")) {
            speed *= 2;
        }

        controller.Move(move * speed * Time.deltaTime);
    }
}
