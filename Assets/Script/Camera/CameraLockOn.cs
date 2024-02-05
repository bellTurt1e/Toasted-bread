using UnityEngine;

public class CameraLockOn : MonoBehaviour {
    public Transform playerBoardAnchor;
    public Transform opponentBoardAnchor;

    private float distance = 73f;
    private float angle = 55.95002f;

    private bool isMainView = true;

    public FadeController fadeController;

    void Start() {
        SetCameraPosition(playerBoardAnchor, isMainView);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            StartCoroutine(fadeController.FadeOutIn(() =>
            {
                isMainView = !isMainView;
                Transform currentAnchor = isMainView ? playerBoardAnchor : opponentBoardAnchor;
                SetCameraPosition(currentAnchor, isMainView);
            }));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            StartCoroutine(fadeController.FadeOutIn(() => {
                isMainView = !isMainView;
                Transform currentAnchor = isMainView ? playerBoardAnchor : opponentBoardAnchor;
                SetCameraPosition(currentAnchor, isMainView);
            }));
        }
    }



    void SetCameraPosition(Transform boardAnchor, bool isMainView) {
        Vector3 offset = Quaternion.Euler(angle, isMainView ? 0 : 180, 0) * new Vector3(0, 0, -distance);
        transform.position = boardAnchor.position + offset;
        transform.LookAt(boardAnchor.position);
    }
}
