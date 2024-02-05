using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; // Include System for the Action delegate

public class FadeController : MonoBehaviour {
    public Image fadeOverlay; // Assign the full-screen Image in the Inspector
    public float fadeSpeed = 1f; // Speed of the fade

    // Modified FadeOutIn coroutine to include a callback action
    public IEnumerator FadeOutIn(Action onFullyFaded = null) {
        yield return StartCoroutine(Fade(1)); // Fade to black
        onFullyFaded?.Invoke(); // Invoke the callback action when fully faded to black
        yield return StartCoroutine(Fade(0)); // Fade back to clear
    }

    private IEnumerator Fade(float targetAlpha) {
        while (!Mathf.Approximately(fadeOverlay.color.a, targetAlpha)) {
            // Adjust the alpha value gradually
            Color color = fadeOverlay.color;
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeOverlay.color = color;

            yield return null;
        }
    }
}
