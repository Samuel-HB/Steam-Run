using UnityEngine;
using System.Collections;
public class SC_GearRotation : MonoBehaviour
{
    public bool isMoving;
    void Start()
    {

    }

    private void Update()
    {
        if (isMoving == true)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.y + 0.06f, 0);
        }
    }
}
