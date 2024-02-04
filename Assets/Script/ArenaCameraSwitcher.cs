using UnityEngine;

public class ArenaCameraSwitcher : MonoBehaviour {
    public Camera[] cameras; // Array to hold all the cameras
    private int currentCameraIndex; // To keep track of the currently active camera

    void Start() {
        // Deactivate all cameras except the first one
        foreach (Camera cam in cameras) {
            cam.gameObject.SetActive(false);
        }

        if (cameras.Length > 0) {
            cameras[0].gameObject.SetActive(true); // Activate the first camera
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            SwitchCamera(1); // Move to the next camera
        }
        else if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            SwitchCamera(-1); // Move to the previous camera
        }
    }

    void SwitchCamera(int direction) {
        cameras[currentCameraIndex].gameObject.SetActive(false); // Deactivate the current camera

        // Calculate the new camera index using modulo to wrap around
        currentCameraIndex = (currentCameraIndex + direction + cameras.Length) % cameras.Length;

        cameras[currentCameraIndex].gameObject.SetActive(true); // Activate the new camera
    }
}
