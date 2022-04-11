using UnityEngine;

public class GameRunner : MonoBehaviour {
    public GameObject mainCamera;
    public GameObject freeCam;
    public GameObject canvas;
    public Terrain terrain;

    public float mainCameraSpeed;

    private Vector3 freeCamInitPos;
    private float freeCamInitCameraRotation;

    private Vector3 terrainCenter;
    private float cameraRotation;
    private float cameraRotationRadius;

    private enum Mode {
        MAIN_MENU,
        FREE_CAM
    }
    private Mode mode = Mode.MAIN_MENU;

    private void Awake() {
        freeCamInitPos = freeCam.transform.position;
        freeCamInitCameraRotation = freeCam.transform.GetChild(0).transform.eulerAngles.x;

        int width = terrain.terrainData.heightmapResolution;
        terrainCenter = new Vector3(width / 2, 0, width / 2);
        cameraRotation = mainCamera.transform.localEulerAngles.y;
        Vector3 mainCameraPos = new Vector3(mainCamera.transform.position.x, 0, mainCamera.transform.position.z);
        cameraRotationRadius = (terrainCenter - mainCameraPos).magnitude;
    }

    private void Update() {
        switch (mode) {
            case Mode.MAIN_MENU:
                RotateMainCamera();
                break;
            case Mode.FREE_CAM:
                if (Input.GetButtonDown("Cancel")) {
                    OnExitFreeCam();
                }
                break;
        }
    }

    private void RotateMainCamera() {
        cameraRotation += mainCameraSpeed * Time.deltaTime;
        mainCamera.transform.localRotation = Quaternion.Euler(freeCamInitCameraRotation, cameraRotation - 180, 0f);

        float newPosX = (cameraRotationRadius * Mathf.Sin(cameraRotation * Mathf.PI / 180f)) + terrainCenter.x;
        float newPosZ = (cameraRotationRadius * Mathf.Cos(cameraRotation * Mathf.PI / 180f)) + terrainCenter.z;
        mainCamera.transform.position = new Vector3(newPosX, mainCamera.transform.position.y, newPosZ);
    }

    public void OnStartFreeCam() {
        if (mode == Mode.FREE_CAM) {
            return;
        }

        mainCamera.SetActive(false);
        canvas.SetActive(false);

        freeCam.transform.position = mainCamera.transform.position;
        freeCam.transform.localRotation = Quaternion.Euler(0f, cameraRotation - 180, 0f);
        freeCam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
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
