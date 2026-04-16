using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SpikeMovement : MonoBehaviour
{
    public bool isMoving =false;
    [SerializeField] private float speed;
    [SerializeField] private bool isInGround;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isOnWall;
    [SerializeField] private bool isOnLeftWall;
    private float direction = 0.1f;

    [SerializeField] private List<string> layers;
    private int LayerIndex =1;
    private List<GameObject> children = new List<GameObject>(); 
    private int childrenNumber;
    private Transform childrenTransform;
    private GameObject childrenGameObject;
    void Start()
    {
        childrenNumber = transform.childCount;
        for(int i = 0; i < childrenNumber; i++)
        {
            childrenTransform = transform.GetChild(i);
            childrenGameObject = childrenTransform.gameObject;
            children.Add(childrenGameObject);
        }

        if (isMoving == true)
        {
            if(isInGround == true)
            {
                direction *= -1;
                LayerIndex = 1;
                childrenGameObject.layer = LayerMask.NameToLayer("Default");
                SetLayer();
                if(isOnWall == true)
                {
                    if(isOnLeftWall == true)
                    {
                        transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                    }
                }
                else
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                }
            }
            StartCoroutine(Movement());
        }        
    }
    IEnumerator Movement()
    {
        SetLayer();
        if (isOnWall == true)
        {
            for (int i = 0; i < 10; i++)
            {

                Vector2 newPos = transform.position;
                if (isOnLeftWall)
                {
                    newPos.x -= direction;
                }
                else
                {
                    newPos.x += direction;
                }

                    transform.position = newPos;
                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {

                Vector2 newPos = transform.position;
                if (isOnGround)
                {
                    newPos.y -= direction;
                }
                else
                {
                    newPos.y += direction;
                }
                transform.position = newPos;
                yield return new WaitForSeconds(0.005f);
            }
        }    
        yield return new WaitForSeconds(speed);
        direction *= -1;
        StartCoroutine(Movement());
    }
    void SetLayer()
    {
        for(int i = 0;i < childrenNumber; i++)
        {
            children[i].layer = LayerMask.NameToLayer(layers[LayerIndex]);
        }
        if(LayerIndex == 0)
        {
            LayerIndex = 1;
        }
        else
        {
            LayerIndex = 0;
        }
    }
}
