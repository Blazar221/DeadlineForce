using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float speed = 0.05f;

    GameObject player;

    void Start()
    {
       player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 1.2f)
        {
            PlayerHealth.Instance.TakeDamage(40);
            Destroy(gameObject);
        }
    }
}
