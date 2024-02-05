using UnityEngine;
using System.IO;
using System.Collections;

public class RenderTextureExporter : MonoBehaviour {
    public RenderTexture renderTexture;
    public float delayBeforeCapture = 2.0f; // Delay in seconds before capturing the RenderTexture

    private void Start() {
        StartCoroutine(CaptureWithDelay());
    }

    private IEnumerator CaptureWithDelay() {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeCapture);

        // After the delay, capture the RenderTexture
        SaveRenderTextureToPNG();
    }
    public void SaveRenderTextureToPNG() {
        RenderTexture.active = renderTexture;
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        byte[] byteArray = texture2D.EncodeToPNG();
        string path = Path.Combine(Application.dataPath, "RenderTextureExport.png");
        File.WriteAllBytes(path, byteArray);

        Debug.Log($"Saved RenderTexture to {path}");

        // Clean up
        RenderTexture.active = null;
        DestroyImmediate(texture2D);
    }
}
