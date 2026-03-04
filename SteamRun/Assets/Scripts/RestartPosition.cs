using UnityEngine;
using System;

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

    private void Update()
    {
        // movement to test the event
        //transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
        //transform.Rotate(0, 0, 50 * Time.deltaTime);
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
