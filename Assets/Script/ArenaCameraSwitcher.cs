using UnityEngine;

public class ArenaCameraSwitcher : MonoBehaviour {
    public Camera[] cameras;
    private int currentCameraIndex;

    void Start() {
        foreach (Camera cam in cameras) {
            cam.gameObject.SetActive(false);
        }

        if (cameras.Length > 0) {
            cameras[0].gameObject.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchCamera(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchCamera(1);
        }
    }


    void SwitchCamera(int direction) {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + direction + cameras.Length) % cameras.Length;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
