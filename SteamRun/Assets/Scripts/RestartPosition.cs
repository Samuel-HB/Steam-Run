using UnityEngine;

public class RestartPosition : MonoBehaviour
{
    private Vector3 position = new Vector3();
    private Quaternion rotation = new Quaternion();

    private void Start()
    {
        EventManager.Instance.RestartLevel += RestartTransform;

        position = transform.position;
        rotation = transform.rotation;
    }

    public void RestartTransform()
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void OnDestroy()
    {
        EventManager.Instance.RestartLevel -= RestartTransform;
    }
}
