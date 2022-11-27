using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float speed = 0.03f;

    GameObject player;

    void Start()
    {
       player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        if(transform.position == player.transform.position)
        {
            Destroy(this);
        }
    }
}
