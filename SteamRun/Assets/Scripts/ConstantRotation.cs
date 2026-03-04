using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
