using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    public float speed;

    private Rigidbody2D rb;
    private Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        Destroy(gameObject, 1.5f);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        pos.x -= speed;
    }
}
