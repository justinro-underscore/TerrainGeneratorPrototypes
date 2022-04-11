using UnityEngine;

public class GameRunner : MonoBehaviour {
    public GameObject mainCamera;
    public GameObject freeCam;
    public GameObject canvas;

    private Vector3 freeCamInitPos;
    private float freeCamInitCameraRotation;

    private enum Mode {
        MAIN_MENU,
        FREE_CAM
    }
    private Mode mode = Mode.MAIN_MENU;

    private void Awake() {
        freeCamInitPos = freeCam.transform.position;
        freeCamInitCameraRotation = freeCam.transform.GetChild(0).transform.eulerAngles.x;
    }

    private void Update() {
        if (mode == Mode.FREE_CAM && Input.GetButtonDown("Cancel")) {
            OnExitFreeCam();
        }
    }

    public void OnStartFreeCam() {
        if (mode == Mode.FREE_CAM) {
            return;
        }

        mainCamera.SetActive(false);
        freeCam.SetActive(true);
        canvas.SetActive(false);
        mode = Mode.FREE_CAM;
    }

    private void OnExitFreeCam() {
        if (mode != Mode.FREE_CAM) {
            return;
        }

        freeCam.SetActive(false);
        freeCam.transform.position = freeCamInitPos;
        freeCam.transform.localRotation = Quaternion.Euler(0f, 45f, 0f);
        freeCam.transform.GetChild(0).transform.localRotation = Quaternion.Euler(freeCamInitCameraRotation, 0f, 0f);

        mainCamera.SetActive(true);
        canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        mode = Mode.MAIN_MENU;
    }
}
