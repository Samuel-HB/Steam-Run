using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    public Transform playerTransform;
    private float camPositionX = 0;
    private float lookForwardPositionX;
    [SerializeField] private int borderMinX = -6;
    [SerializeField] private int borderMaxX = 6;
    [SerializeField] private int borderMinY = 0;
    [SerializeField] private int borderMaxY = 3;


    private void Start()
    {
        EventManager.Instance.PlayerFlipRight += FlipCameraRight;
        EventManager.Instance.PlayerFlipLeft += FlipCameraLeft;
        EventManager.Instance.PlayerMove += StartSmoothTargetOffsetMovement;
    }

    private void FlipCameraRight()
    {
        lookForwardPositionX = 3;
    }

    private void FlipCameraLeft()
    {
        lookForwardPositionX = -3;
    }

    private void StartSmoothTargetOffsetMovement()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothTargetOffsetPositive(10f));
    }

    private void Update()
    {
        cameraTransform.position = new Vector3(Mathf.Clamp(playerTransform.position.x + camPositionX, borderMinX, borderMaxX),
                                               Mathf.Clamp(playerTransform.position.y, borderMinY, borderMaxY),
                                                           cameraTransform.position.z);
    }

    IEnumerator SmoothTargetOffsetPositive(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            camPositionX = Mathf.Lerp(camPositionX, lookForwardPositionX, (time / duration));
            yield return null;
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.PlayerFlipRight += FlipCameraRight;
        EventManager.Instance.PlayerFlipLeft += FlipCameraLeft;
        EventManager.Instance.PlayerMove -= StartSmoothTargetOffsetMovement;
    }
}
