using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public List<Transform> startingPositions;
    public List<GameObject> rooms;     //index 0--> LR, index 1--> LRB, index 2--> LRT, index 1--> LRBT,
    public float moveAmount;
    public LayerMask room;

    private int direction;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX, maxX, minY;

    public bool stopGeneration = false;

    private int downCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        int ranStartingPos = Random.Range(0, startingPositions.Count);
        transform.position = startingPositions[ranStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        PlayerController.instance.transform.position = transform.position;

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if(timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if(direction == 1 || direction == 2)
        {
            if(transform.position.x < maxX)
            {
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Count);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if(direction == 3) { direction = 2; }
                else if(direction == 4) { direction = 5; }
            }

            else
            {
                direction = 5;
            }
        }

        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;

                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                
                int rand = Random.Range(0, rooms.Count);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }

            else
            {
                direction = 5;
            }

        }

        else if (direction == 5)
        {

            downCounter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                if(roomDetection.GetComponent<RoomType>().type != 1 || roomDetection.GetComponent<RoomType>().type != 3)
                {

                    if(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }

                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 3);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                
                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
            }
        }
        
    }

}
