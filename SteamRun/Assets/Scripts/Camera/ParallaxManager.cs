using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform;
        [Range(0, 1)] public float parallaxeFactor;
    }

    public ParallaxLayer[] layers;

    public Transform camTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = camTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = camTransform.position - lastCameraPosition;

        foreach (ParallaxLayer layer in layers)
        {
            float movePositionX = cameraDelta.x * layer.parallaxeFactor;
            float movePositionY = cameraDelta.y * layer.parallaxeFactor;

            layer.layerTransform.position += new Vector3(movePositionX, movePositionY, 0);
        }
        lastCameraPosition = camTransform.position;
    }
}
