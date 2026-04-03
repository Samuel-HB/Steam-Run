using UnityEngine;

public class SC_Spike : MonoBehaviour
{
    public GameObject trap;
    public GameObject trapSpawner;
    public int numberOfTrapToSpawn=0;
    public bool direction =true;
    public float spaceBetween;

    public bool isThereGap;
    public float gap;
    public float gapLength;
    public int numberOfGap;

    public bool goUpwardOrDownward;
    public bool goUpward;
    public float heightChangeSpace;
    public int numberBeforeHeightChange;
    public int numberOfHeightChange;
    private bool changeHeight;

    public bool shouldChangeDirection;
    public int numberBeforeDirectionChange;
    public int numberOfDirectionChange;

    void Start()
    {
        int gapIndex = 0;
        int heightChangeIndex = 0;
        float gapLengthIndex = gapLength;
        if (goUpwardOrDownward==true )
        {
            if (goUpward == false)
            {
                heightChangeSpace *= -1;
            }
            changeHeight = true;
        }
        if (direction ==false)
        {
            spaceBetween *= -1;
        }
        Vector2 spawnPosition = new Vector2(trapSpawner.transform.position.x, trapSpawner.transform.position.y);
        for (int i = 0; numberOfTrapToSpawn > 0; i++)
        {
            spawnPosition.x = trapSpawner.transform.position.x +(spaceBetween*i);

            Instantiate(trap, spawnPosition, transform.rotation);
            numberOfTrapToSpawn--;

            if (changeHeight == true && numberOfHeightChange > 0)
            {
                heightChangeIndex++;
                if(heightChangeIndex == numberBeforeHeightChange)
                {
                    heightChangeIndex = 0;
                    spawnPosition.y += heightChangeSpace;
                    numberOfHeightChange--;
                }
            }
            if (isThereGap == true && numberOfGap > 0)
            {
                gapIndex++;
                if(gapIndex == gap)
                {
                    while(gapLengthIndex != 0)
                    {
                        i++;
                        gapLengthIndex--;
                    }
                    gapIndex = 0;
                    gapLengthIndex = gapLength;
                    numberOfGap--;
                }
            }
        }
    }
}
