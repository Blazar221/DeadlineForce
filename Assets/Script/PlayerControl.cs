using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public static event Action OnPlayerDeath;

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

    private float keepLongScoreTime = 0f;
    private float longScoreTimeBar = 0.7f;
    
    private bool canGetSingleScore;
    private bool canGetLongScore;
    private bool canAvoidDamage;
    private bool canChangeGravity;
    private bool missFood;
    private bool missMine;

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
        canGetSingleScore = false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime) {
            animator.SetBool("isEating",false);
            animator.SetBool("isDamaged",false);
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
            missFood = false;
            animator.SetBool("isEating",true);
            nextTime = Time.time + 0.1f; //Eating time lasts for 0.2s
            if (canGetSingleScore){
                ScoreSingle();
            }
            else if (!canAvoidDamage){
                MissSingle();
            }
            if (canAvoidDamage){
                avoidMine();
            }
		}

        if (Input.GetKey(KeyCode.O))
		{
            animator.SetBool("isEating",true);
            keepLongScoreTime += Time.fixedDeltaTime;
            if (canGetLongScore){
                ScoreLong();
            }
		}
        
        //Debug.Log(hitScore + "/" + missScore);
    }

    // Score on single diamond function
    void ScoreSingle()
    {
        hitScore++;
        Destroy(toHit);
        Instantiate(hitEffect, transform.position + new Vector3(-2.0f,0,0), hitEffect.transform.rotation);
        // Update hit times
        ScoreManager.instance.AddHit();
        // Update final score
        GameOverScreen.instance.IncreaseScore();
    }

    void ScoreLong()
    {
        if(keepLongScoreTime > longScoreTimeBar){
            keepLongScoreTime = 0f;

            hitScore++;
            
            Instantiate(hitEffect, transform.position + new Vector3(-2.0f,0,0), hitEffect.transform.rotation);
            // Update hit times
            ScoreManager.instance.AddHit();
            // Update final score
            GameOverScreen.instance.IncreaseScore();
        }
    }

    void MissSingle()
    {
        missScore++;
        Instantiate(missEffect, transform.position + new Vector3(-2.0f,0,0), missEffect.transform.rotation);
        // Update final score
        GameOverScreen.instance.DecreaseScore();
    }

    // Mine collision functions
    void avoidMine()
    {
        missMine = false;
        Destroy(toHit);
        Instantiate(hitEffect, transform.position + new Vector3(-2.0f,0,0), hitEffect.transform.rotation);
    }

    void collideMine()
    {
        nextTime = Time.time + 0.3f;
        animator.SetBool("isDamaged",true);
        Instantiate(missEffect, transform.position + new Vector3(-2.0f,0,0), missEffect.transform.rotation);
        Destroy(toHit);
        // damage
        TakeDamage(10);
    }

    // TakeDamage function
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0) 
        {
            currentHealth = 0;
            Debug.Log("You are dead!");
            OnPlayerDeath?.Invoke();
        }
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
            missFood = true;
            toHit = collision.gameObject;
            canGetSingleScore = true;
        }

        if(collision.gameObject.tag == "LongNote")
        {
            canGetLongScore = true;
        }

        if(collision.gameObject.tag == "Mine")
        {
            missMine = true;
            toHit = collision.gameObject;
            canAvoidDamage = true;
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
            if (missFood){
                MissSingle();
            }
            missFood = false;
            toHit = null;
            canGetSingleScore = false;
        }

        if(collision.gameObject.tag == "Mine")
        {
            if (missMine){
                collideMine();
            }
            toHit = null;
            canAvoidDamage = false;
        }
    }
}
