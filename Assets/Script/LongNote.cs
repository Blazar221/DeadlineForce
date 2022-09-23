using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float height;

    private GameObject player;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Vector3 pos;
    private Vector3 pos1;
    private Vector3 pos2;

    [SerializeField] public float preCheckLength;

    private LineRenderer lineRenderer;

    private bool isCollide;
    private bool isPressed;

    void Awake() {
        pos = transform.position;
        pos1 = transform.position;
        pos2 = transform.position;
        // This will be overwritten by SetLength()
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetLength(float length)
    {
        pos1.x -= length/2;
        pos2.x += length/2;

        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector3(length+preCheckLength, height);
        boxCollider.offset = new Vector3(-preCheckLength/2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("here");
        Debug.Log(pos.x);
        Debug.Log(pos1.x);
        Debug.Log(pos2.x);
        transform.position = pos;

        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }

    private void FixedUpdate()
    {
        pos.x -= speed;
        pos1.x -= speed;
        pos2.x -= speed;
    }

}
