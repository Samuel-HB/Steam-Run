using UnityEngine;

public class SC_SawBladeMovement : MonoBehaviour
{

    [SerializeField]
    int speed = 10;
    public GameObject startPoint;
    public GameObject endPoint;
    bool goToTheEnd = true;
    void Start()
    {
        transform.position = startPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (goToTheEnd == true)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, endPoint.transform.position, speed*Time.deltaTime);
            transform.position = newPos;
        }

        if (goToTheEnd == false)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, startPoint.transform.position, speed * Time.deltaTime);
            transform.position = newPos;
        }

        if (transform.position == startPoint.transform.position)
        {
            goToTheEnd = true;
        }

        if (transform.position == endPoint.transform.position)
        {
            goToTheEnd = false;
        }
    }
}
