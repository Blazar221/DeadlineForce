using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    // To call game over menu
    public static event Action OnPlayerDeath;

    private Rigidbody2D rb2D;
    private bool isUpsideDown;
    private Animator animator;
    private int curYPos;

    [SerializeField]
    // Control health of player
    public int maxHealth = 100; 
    public int currentHealth;
    public SliderBar healthBar;

    private float[] playerYPosArr;
    // Blood Effect
    public GameObject bloodEffectCeil;
    public GameObject bloodEffectFloor;

    void Awake()
    {
        instance = this;
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
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

    // TakeDamage Function
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetValue(currentHealth);
        // add blood effect
        if (isUpsideDown){
            Instantiate(bloodEffectCeil, transform.position + new Vector3(0, -0.2f, 0), bloodEffectCeil.transform.rotation);
        }
        else
        {
            Instantiate(bloodEffectFloor, transform.position + new Vector3(0,0.2f,0), bloodEffectFloor.transform.rotation);
        }
        if(currentHealth <= 0) 
        {
            currentHealth = 0;
            Debug.Log("You are dead!");
            OnPlayerDeath?.Invoke();
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
        animator.SetBool("UpsideDown",isUpsideDown);
    }
}