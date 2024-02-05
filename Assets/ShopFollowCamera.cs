using UnityEngine;

public class ShopFollowCamera : MonoBehaviour {
    public Transform cameraTransform;

    void Update() {
        // Position the shop UI near the camera, offset as needed
        transform.position = cameraTransform.position + cameraTransform.forward;

        // Always face the camera
        transform.LookAt(cameraTransform);
    }
}
