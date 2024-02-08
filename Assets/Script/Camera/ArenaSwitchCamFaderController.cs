using UnityEngine;
using UnityEngine.UI;

public class ArenaaCamButton : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;
    public FadeController fadeController;
    private bool isTransitioning = false; // Flag to indicate if a transition is in progress

    void Start()
    {
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }

        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }
    }

    // Add this method to be called when the button is clicked
    public void OnButtonClick()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            StartCoroutine(fadeController.FadeOutIn(() =>
            {
                SwitchCamera(1); // You can customize the direction based on your needs
                isTransitioning = false;
            }));
        }
    }

    void SwitchCamera(int direction)
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + direction + cameras.Length) % cameras.Length;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
