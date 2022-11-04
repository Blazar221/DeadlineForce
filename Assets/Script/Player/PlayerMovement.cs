using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private bool canChangeGravity;
    private bool canCross;
    private Animator animator;
    private Rigidbody2D rb2D;
    private bool isUpsideDown;
    private bool reverseControl = false;
    private float[] playerYPosArr;
    private int curYPos;

    void Awake()
    {
        instance = this;

        canChangeGravity = true;
        canCross = false;

        playerYPosArr = new float[4];
        playerYPosArr[0] = 3.4f;
        playerYPosArr[1] = 1.6f;
        playerYPosArr[2] = -1.6f;
        playerYPosArr[3] = -3.4f;
        curYPos = 1;
        isUpsideDown = false;

        rb2D = gameObject.GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPosition = transform.position;
        
        if (canChangeGravity)
		{
            if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                if(!reverseControl)
                {
                    SetYPos((curYPos+3)%4);
                }
                else
                {
                    SetYPos((curYPos+1)%4);
                }
            }
            if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                if(!reverseControl)
                {
                    SetYPos((curYPos+1)%4);
                }
                else
                {
                    SetYPos((curYPos+3)%4);
                }
            }
		}
    }

    public void SetYPos(int yPos)
    {
        curYPos = yPos;
        // Directly move position
        transform.position = new Vector3(transform.position.x, playerYPosArr[yPos], transform.position.z);
        // Set Gravity Direction and isUpsideDown Flag
        if(yPos == 0 || yPos == 2)
        {
            rb2D.gravityScale = -1f;
            isUpsideDown = true;
        }
        else
        {
            rb2D.gravityScale = 1f;
            isUpsideDown = false;
        }
        canChangeGravity = false;
        animator.SetBool("UpsideDown",isUpsideDown);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.gameObject.tag == "GravSwitch")
        // {
        //     canChangeGravity = true;
        // }
        
        if( collision.gameObject.tag == "OriginalPlatForm")
        {
            canChangeGravity = true;
            canCross = false;
        }

        if(collision.gameObject.tag == "Platform"){
            canChangeGravity = true;
            canCross = true;
        }
    }
}
