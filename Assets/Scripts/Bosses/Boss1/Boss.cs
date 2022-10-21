using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public GameObject GFX;

    public void LookAtPlayer()
    {
        if(transform.position.x > player.position.x)
        {
            GFX.transform.localScale = new Vector3(-1, 1 , 1);
        }

        else
        {
            GFX.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
