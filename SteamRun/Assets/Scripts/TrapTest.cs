using UnityEngine;
using System;

public class TrapTest : MonoBehaviour
{
    private Vector3 position = new Vector3();
    private Quaternion rotation = new Quaternion();

    public event Action RestartLevel;

    private void Start()
    {
        RestartLevel += RestartTransform;

        position = transform.position;
        rotation = transform.rotation;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
        transform.Rotate(0, 0, 50 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.U))
        {
            RestartLevel.Invoke();
        }
    }

    public void RestartTransform()
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void OnDestroy()
    {
        RestartLevel -= RestartTransform;
    }
}
