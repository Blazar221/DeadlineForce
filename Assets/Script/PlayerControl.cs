using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2D;
    // private float moveSpeed;
    [SerializeField]

    //这些是其他class需要调用的变量
    public bool canChangeGravity;
    public int score;
    public int health;


    private Animator animator;
    private bool isUpsideDown;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        canChangeGravity = false;
        health = 100;

        isUpsideDown = false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GravSwitch")
        {
            canChangeGravity = true;
        }
        
        if(collision.gameObject.tag == "food")
        {
            canChangeGravity = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GravSwitch")
        {
            
        }

        if(collision.gameObject.tag == "food")
        {
            
        }
    }
}
