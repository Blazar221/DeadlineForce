using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 xValue;
    public float jumpHeight;
    public bool changeDirecionBool, leftFacing, changeIt, easing;
    Animator animator;
    Vector3 desiredPosition;
    // Start is called before the first frame update
    void Start()
    {
        DesiredPosition();
        changeDirection();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (changeIt)
        {
            if (easing)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPosition, .04f);
            }
            else
            {
                transform.position = desiredPosition;
            }
          

            if (Mathf.Abs(desiredPosition.x - transform.position.x) <= 0.1f)
            {
                Debug.Log("Changing gavity");
                changeIt = false;
            }

        }
        animator.SetBool("ChangeDirection", changeDirecionBool);
    }
    public void CallThisMethod()
    {
        DesiredPosition();
        changeDirection();
    }
    public void DesiredPosition()
    {
        desiredPosition = new Vector2(changeDirecionBool ? xValue.x : xValue.y, transform.position.y + jumpHeight);
    }
    void changeDirection()
    {
        changeDirecionBool = !changeDirecionBool;
        // transform.position = Vector3.Lerp(transform.position,desiredPosition,.1f);
        if (changeDirecionBool != leftFacing)
        {
            changeIt = true;

            leftFacing = changeDirecionBool;
        }

        Debug.Log(transform.position.x);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            Debug.Log("dead Player");
            FindObjectOfType<GameManager>().RestartScene();

        }
    }
}
