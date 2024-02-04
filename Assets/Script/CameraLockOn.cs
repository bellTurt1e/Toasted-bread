using UnityEngine;

public class CameraLockOn : MonoBehaviour {
    public Transform playerBoardAnchor; // Assign this to the player's board anchor in the Inspector
    public Transform opponentBoardAnchor; // Assign this to the opponent's board anchor in the Inspector

    private float distance = 73f; // Fixed distance from the board anchor
    private float angle = 55.95002f; // Fixed tilt angle from the horizontal

    private bool isMainView = true; // Track whether the main view or opposing view is currently active

    void Start() {
        // Set the camera to the main view at the start
        SetCameraPosition(playerBoardAnchor, isMainView);
    }

    void Update() {
        // Toggle the camera view between the main and opposing views when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            isMainView = !isMainView; // Toggle the view state

            // Choose the appropriate board anchor based on the current view state
            Transform currentAnchor = isMainView ? playerBoardAnchor : opponentBoardAnchor;
            SetCameraPosition(currentAnchor, isMainView);
        }
    }

    void SetCameraPosition(Transform boardAnchor, bool isMainView) {
        // Calculate the offset from the board anchor based on the fixed distance and angle
        Vector3 offset = Quaternion.Euler(angle, isMainView ? 0 : 180, 0) * new Vector3(0, 0, -distance);

        // Position the camera using the calculated offset
        transform.position = boardAnchor.position + offset;

        // Make the camera look towards the board anchor
        transform.LookAt(boardAnchor.position);
    }
}
