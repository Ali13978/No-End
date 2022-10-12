using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwnRoom : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGeneration levelGen;

    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(roomDetection == null && levelGen.stopGeneration == true)
        {
            //Random Room Spawn
            int rand = Random.Range(0, levelGen.rooms.Count);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
