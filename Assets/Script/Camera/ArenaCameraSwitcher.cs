using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArenaCameraSwitcher : MonoBehaviour {
    private List<GameObject> cameras;
    private int currentCameraIndex;

    public void SetupCameras() {
        cameras = new List<GameObject>();

        GameObject[] camerasWithTag = GameObject.FindGameObjectsWithTag("ArenaCamera");
        foreach (GameObject cam in camerasWithTag) {
            cameras.Add(cam);
            cam.SetActive(false);
        }

        if (cameras.Count > 0) {
            cameras[0].SetActive(true);
        }
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Debug.Log("Left arrow pressed");
            SwitchCamera(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("Right arrow pressed");
            SwitchCamera(1);
        }
    }


    void SwitchCamera(int direction) {
        if (cameras.Count == 0) {
            Debug.LogWarning("No cameras available in the cameras list.");
            return; // Early exit if there are no cameras
        }

        // Deactivate the current camera
        if (currentCameraIndex >= 0 && currentCameraIndex < cameras.Count) {
            cameras[currentCameraIndex].SetActive(false);
        }
        else {
            Debug.LogWarning("Current camera index is out of range: " + currentCameraIndex);
        }

        // Adjust the currentCameraIndex based on direction, ensuring it stays within the bounds of the list
        currentCameraIndex = (currentCameraIndex + direction + cameras.Count) % cameras.Count;

        // Reactivate the camera at the new index
        if (currentCameraIndex >= 0 && currentCameraIndex < cameras.Count) {
            cameras[currentCameraIndex].SetActive(true);
        }
        else {
            Debug.LogError("Adjusted camera index is out of range: " + currentCameraIndex);
        }
    }
}