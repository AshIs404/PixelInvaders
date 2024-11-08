using UnityEngine;

/// <summary>
/// Adjusts the camera to dynamically adapt to different screen aspect ratios without adding black bars.
/// </summary>
public class CameraScaler : MonoBehaviour
{
    /// <summary>
    /// Target orthographic size for the camera.
    /// </summary>
    [SerializeField] private float baseOrthographicSize = 5.0f;

    /// <summary>
    /// Initializes the camera by adjusting its orthographic size.
    /// </summary>
    private void Start()
    {
        AdjustCamera();
    }

    /// <summary>
    /// Adjusts the camera's orthographic size to ensure the game fills the screen on different devices.
    /// </summary>
    private void AdjustCamera()
    {
        Camera camera = GetComponent<Camera>();

        // Calculate the target orthographic size based on the screen aspect ratio
        float screenAspect = (float)Screen.width / Screen.height;
        camera.orthographicSize = baseOrthographicSize / screenAspect;
    }
}
