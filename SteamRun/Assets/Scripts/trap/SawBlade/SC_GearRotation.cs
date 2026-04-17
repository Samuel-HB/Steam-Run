using UnityEngine;

public class SC_GearRotation : MonoBehaviour
{
    public bool isMoving;

    private void Update()
    {
        if (isMoving == true)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.y + 0.06f, 0);
        }
    }
}
