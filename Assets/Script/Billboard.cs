using UnityEngine;

public class Billboard : MonoBehaviour {
    private Transform cam;

    void Start() {
        // Automatically find and use the main camera
        if (Camera.main != null) {
            cam = Camera.main.transform;
        }
        else {
            Debug.LogError("No main camera found. Ensure your camera is tagged as 'MainCamera'.", this);
        }
    }

    void LateUpdate() {
        if (cam != null) {
            // Make the billboard face the camera by looking in the opposite direction
            Vector3 lookDirection = cam.position - transform.position;
            lookDirection.y = 0; // Optional: This keeps the billboard upright and only rotates it on the Y axis
            transform.rotation = Quaternion.LookRotation(-lookDirection, Vector3.up);
        }
    }
}
