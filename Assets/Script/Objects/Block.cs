using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    public float speed;

    private Rigidbody2D rb;
    private Vector3 pos;

    public void SetSpeed(float s)
    {
        speed = s;
    }

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
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
