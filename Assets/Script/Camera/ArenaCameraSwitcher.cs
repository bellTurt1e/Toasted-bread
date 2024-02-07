using System.Collections.Generic;
using UnityEngine;

public class ArenaCameraSwitcher : MonoBehaviour {
    private List<GameObject> cameras;
    private int currentCameraIndex;

    void Start() {
        cameras = new List<GameObject>(); // Initialize the list

        GameObject[] camerasWithTag = GameObject.FindGameObjectsWithTag("ArenaCamera");
        foreach (GameObject cam in camerasWithTag) {
            cameras.Add(cam); // Add found camera to the list
            Debug.Log("Found ArenaCamera by Tag: " + cam.name);
        }

        if (cameras.Count > 0) {
            cameras[0].gameObject.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            SwitchCamera(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            SwitchCamera(1);
        }
    }


    void SwitchCamera(int direction) {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + direction + cameras.Count) % cameras.Count;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}