using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2D;
    // private float moveSpeed;
    [SerializeField]

    private bool canChangeGravity;
    private Animator animator;
    private bool isUpsideDown;
    // Start is called before the first frame update
    void Start()
    {
        isUpsideDown = false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // To adjust the height of jumping, change the value of jumpForce
        canChangeGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canChangeGravity && Input.GetKeyDown (KeyCode.Space))
		{
            isUpsideDown = !isUpsideDown;
            animator.SetBool("UpsideDown",isUpsideDown);
            rb2D.gravityScale *= -1;
		}

        if (Input.GetKey(KeyCode.P))
		{
            animator.SetBool("isEating",true);
		}
        if (Input.GetKeyUp (KeyCode.P))
		{
            animator.SetBool("isEating",false);
		}	

    }





    //The following two functions can be used to set the changing gravity point.

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "changingPoint")
    //     {
    //         canChangeGravity = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     if(collision.gameObject.tag == "changingPoint")
    //     {
    //         canChangeGravity = false;
    //     }
    // }
}
