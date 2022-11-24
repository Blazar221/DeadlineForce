using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    private bool reverseControl = false;
    private float[] playerYPosArr;
    private int curYPos;
    
    private Vector3 originalLocalScale;

    void Awake()
    {
        playerYPosArr = new float[4];
        playerYPosArr[0] = 3.86f;
        playerYPosArr[1] = 1.69f;
        playerYPosArr[2] = -1.67f;
        playerYPosArr[3] = -3.84f;
        curYPos = 1;

        animator = GetComponent<Animator>();

        originalLocalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if(name == "PlayerClone"){
            GetComponent<SpriteRenderer>().color = new Color(39f/255f, 183f/255f, 162f/255f, 0.8f);
            reverseControl = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPosition = transform.position;
        
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

    public void SetYPos(int yPos)
    {
        curYPos = yPos;

        // Directly move position
        transform.position = new Vector3(transform.position.x, playerYPosArr[yPos], transform.position.z);
        // Set Gravity Direction and local Scale
        if(yPos == 0 || yPos == 2)
        {
            transform.localScale = new Vector3(originalLocalScale.x, -originalLocalScale.y, originalLocalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y, originalLocalScale.z);
        }
    }

    public int GetYPos()
    {
        return curYPos;
    }
}
