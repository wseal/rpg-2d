using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private Camera mainCamera;
    private float previousCameraX;
    private float cameraHalfWidth;
    [SerializeField] private ParallaxLayer[] parallaxLayers;

    void Awake()
    {
        mainCamera = Camera.main;
        previousCameraX = mainCamera.transform.position.x;

        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect; // ((float)Screen.width / Screen.height)

        CalculateBackgroundWidths();
    }

    void FixedUpdate()
    {
        float currentCameraX = mainCamera.transform.position.x;
        float cameraDeltaX = currentCameraX - previousCameraX;
        previousCameraX = currentCameraX;

        // float cameraLeftEdge = currentCameraX - cameraHalfWidth;
        // float cameraRightEdge = currentCameraX + cameraHalfWidth;

        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(cameraDeltaX);
        }
    }

    private void CalculateBackgroundWidths()
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.CalculateWidth();
        }
    }
}
