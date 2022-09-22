using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2D;

    // Healthbar
    [SerializeField]
    public int maxHealth = 100; 
    public int currentHealth;
    public HealthBar healthBar;

    //这些是其他class需要调用的变量
    public int hitScore;
    public int missScore;

    // The diamond to destory
    private GameObject toHit;
    

    private Animator animator;
    private bool isUpsideDown;
    private bool isEating;
    private float nextTime;
    private bool canGetScore;
    private bool canChangeGravity;

    // for hitting effect
    public GameObject hitEffect, goodEffect, perfectEffect ,missEffect;
    // for hitting effect

    // Start is called before the first frame update
    void Start()
    {
        hitScore = 0;
        missScore = 0;
        canChangeGravity = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        isUpsideDown = false;
        nextTime = Time.time;
        canGetScore = false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime) {
            animator.SetBool("isEating",false);
        }
        if (canChangeGravity && Input.GetKeyDown (KeyCode.Space))
		{
            isUpsideDown = !isUpsideDown;
            animator.SetBool("UpsideDown",isUpsideDown);
            rb2D.gravityScale *= -1;
		}

        // If the player click space on the wrong point, it will take damage.
        if (canChangeGravity == false && Input.GetKeyDown (KeyCode.Space))
        {
            TakeDamage(5);
        }

        if (Input.GetKeyDown(KeyCode.P))
		{
            animator.SetBool("isEating",true);
            nextTime = Time.time + 0.1f; //Eating time lasts for 0.2s
            if (canGetScore){
                ScoreSingle();
            }
            else {
                MissSingle();
            }
		}
        Debug.Log(hitScore + "/" + missScore);
    }

    // Score on single diamond function
    void ScoreSingle()
    {
        hitScore++;
        Destroy(toHit);
        Instantiate(hitEffect, transform.position + new Vector3(-2.0f,0,0), hitEffect.transform.rotation);
        // Update hit times
        ScoreManager.instance.AddPoint();
    }

    void MissSingle()
    {
        missScore++;
        Instantiate(missEffect, transform.position + new Vector3(-2.0f,0,0), missEffect.transform.rotation);
    }

    // TakeDamage function
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    // The following two functions can be used to set the changing gravity point.

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GravSwitch")
        {
            canChangeGravity = true;
        }
        
        if(collision.gameObject.tag == "food")
        {
            toHit = collision.gameObject;
            canGetScore = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GravSwitch")
        {
            canChangeGravity = false;
        }

        if(collision.gameObject.tag == "food")
        {
            toHit = null;
            canGetScore = false;
        }
    }
}
