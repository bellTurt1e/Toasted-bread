using UnityEngine;
using System.Collections;

public class ArenaCameraSwitcher : MonoBehaviour {
    public Camera[] cameras;
    private int currentCameraIndex;
    public FadeController fadeController;
    private bool isTransitioning = false; // Flag to indicate if a transition is in progress

    void Start() {
        foreach (Camera cam in cameras) {
            cam.gameObject.SetActive(false);
        }

        if (cameras.Length > 0) {
            cameras[0].gameObject.SetActive(true);
        }
    }

    void Update() {
        if (!isTransitioning) // Check if a transition is not already in progress
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                isTransitioning = true; // Set the flag to true to block other transitions
                StartCoroutine(fadeController.FadeOutIn(() => {
                    SwitchCamera(-1);
                    isTransitioning = false; // Reset the flag when transition is complete
                }));

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                isTransitioning = true; // Set the flag to true to block other transitions
                StartCoroutine(fadeController.FadeOutIn(() => {
                    SwitchCamera(1);
                    isTransitioning = false; // Reset the flag when transition is complete
                }));
            }
        }
    }

    void SwitchCamera(int direction) {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + direction + cameras.Length) % cameras.Length;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
