using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private float speed;

    private Rigidbody2D rb;
    private Vector3 pos;

    public enum ElemType {Fire, Water, Grass, Rock};
    [SerializeField]
    public ElemType myType;

    public void SetSpeed(float s)
    {
        speed = s;
    }

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        pos.x -= speed;
    }
}
