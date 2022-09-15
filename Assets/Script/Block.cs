using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
    private Vector3 prevPos;
    
    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
        Destroy(gameObject, 6f);
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = new Vector3(prevPos.x - speed, prevPos.y);
        transform.position = curPos;
        prevPos = curPos;
    }
}
