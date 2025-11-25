using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
  [SerializeField] private Transform layerTransform;
  [SerializeField] private float parallaxFactor;

  // private Vector3 initialPosition;
  private float layerWidth;
  private float halfLayerWidth;

  public void CalculateWidth()
  {
    layerWidth = layerTransform.GetComponent<SpriteRenderer>().bounds.size.x;
    halfLayerWidth = layerWidth / 2;
  }

  public void Move(float distanceToMove)
  {
    // Vector3 newPosition = layerTransform.position + Vector3.right * distanceToMove * parallaxFactor;
    layerTransform.position += Vector3.right * (distanceToMove * parallaxFactor);
  }

  public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
  {
    // float layerWidth = layerTransform.GetComponent<SpriteRenderer>().bounds.size.x;
    float layerLeftEdge = layerTransform.position.x - halfLayerWidth;
    float layerRightEdge = layerTransform.position.x + halfLayerWidth;

    if (cameraRightEdge < layerLeftEdge)
    {
      layerTransform.position -= Vector3.right * layerWidth;
    }
    else if (cameraLeftEdge > layerRightEdge)
    {
      layerTransform.position += Vector3.right * layerWidth;
    }
  }
}