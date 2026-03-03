using UnityEngine;
using System.Collections;

public class SC_SpikeMovement : MonoBehaviour
{
    public bool isMoving =false;
    void Start()
    {
        if (isMoving == true)
        {
            StartCoroutine(Movement());
        }        
    }
    IEnumerator Movement()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 newPos = transform.position;
            newPos.y +=  0.1f;
            transform.position = newPos;
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 10; i++)
        {
            Vector2 newPos = transform.position;
            newPos.y -= 0.1f;
            transform.position = newPos;
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(Movement());
    }
}
