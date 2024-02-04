using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
    public Transform cam;

    void LateUpdate() {
        // Ensure the camera reference is set
        if (cam == null) {
            Debug.LogWarning("Camera not set on Billboard script", this);
            return;
        }

        // Make the billboard face the camera by looking in the opposite direction
        Vector3 lookDirection = cam.position - transform.position;
        lookDirection.y = 0; // Optional: This keeps the billboard upright and only rotates it on the Y axis
        transform.rotation = Quaternion.LookRotation(-lookDirection, Vector3.up);
    }
}

