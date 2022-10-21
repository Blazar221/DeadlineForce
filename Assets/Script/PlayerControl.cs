using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public static event Action OnPlayerDeath;
    public static PlayerControl instance;

    // Healthbar
    [SerializeField]
    public int maxHealth = 100; 
    public int currentHealth;
    public HealthBar healthBar;

    // Boss 
    public Boss boss;

    //这些是其他class需要调用的变量
    public int hitScore;
    public int missScore;
    // num of eaten diamond
    public static int numOfFood;

    // The diamond to destory
    private GameObject toHit;
    

    private Animator animator;
    private bool isUpsideDown;
    private bool isEating;
    private float nextTime;
    private float collsionTime;

    private float itemReleaseTimeCounter = 0f;
    [SerializeField] private float itemReleaseTimeBar = 0.14f;
    
    private bool canGetSingleScore;
    private bool canGetLongScore;
    private bool canAvoidDamage;
    public bool canChangeGravity;
    public bool canCross;
    private bool missFood;
    private bool missMine;
    private bool pressingK;
    // for hitting and blood effect
    public GameObject hitEffect, goodEffect, perfectEffect ,missEffect, bloodEffectCeil, bloodEffectFloor;
    // for hitting effect
    
    private TargetPanel targetPanel;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {   
        instance = this;
        pressingK = false;
        hitScore = 0;
        missScore = 0;
        canChangeGravity = true;
        canCross = false;
        numOfFood = 0;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        isUpsideDown = false;
        nextTime = Time.time;
        canGetSingleScore = false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        targetPanel = TargetPanel.Instance;
        inventory = targetPanel.inventory;
        
    }

    bool IsLvlOne()
    {
        return "Level1" == SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = GetComponent<Transform>().position;
        if (Time.time >= nextTime && !pressingK) {
            animator.SetBool("isEating",false);
            animator.SetBool("isDamaged",false);
        }
        
        
        if (canChangeGravity && !IsLvlOne())
		{
            if (Input.GetKeyDown (KeyCode.W) && !isUpsideDown){
                canCross = false;
                isUpsideDown = true;
                Debug.Log(canChangeGravity);
                rb2D.gravityScale = -15;
                canChangeGravity = false;
            } else if (Input.GetKeyDown (KeyCode.S) && isUpsideDown) {
                canCross = false;
                isUpsideDown = false;
                Debug.Log(canChangeGravity);
                rb2D.gravityScale = 15;
                canChangeGravity = false;
            }
            animator.SetBool("UpsideDown",isUpsideDown);
		}

        if (canCross)
		{
            if (!isUpsideDown && Input.GetKeyDown (KeyCode.S)){
                isUpsideDown = true;
                rb2D.gravityScale = -5;
                this.gameObject.transform.position = new Vector2(temp.x,-1.1f);
            } else if (isUpsideDown && Input.GetKeyDown (KeyCode.W)) {
                isUpsideDown = false;
                rb2D.gravityScale = 5;
                this.gameObject.transform.position = new Vector2(temp.x,1.1f);
            }
            animator.SetBool("UpsideDown",isUpsideDown);
		}

        //加了空格可以跳 不要的话直接删掉就行
        // if (Input.GetKeyDown(KeyCode.Space)){
        //     Vector2 direc;
        //     if (isUpsideDown){
        //         direc = new Vector2(0,-850);
        //     } else {
        //         direc = new Vector2(0,850);
        //     }
        //     GetComponent<Rigidbody2D>().AddForce(direc);
		// }

        // If the player click space on the wrong point, it will take damage.
        // if (canChangeGravity == false && Input.GetKeyDown (KeyCode.Space))
        // {
        //     TakeDamage(5);
        // }

        if (Input.GetKeyDown(KeyCode.J))
		{
            missFood = false;
            animator.SetBool("isEating",true);
            nextTime = Time.time + 0.1f; //Eating time lasts for 0.1s
            if (canGetSingleScore){
                ScoreSingle(Time.time);
            }
            else if (!canAvoidDamage){
                MissSingle();
            }
            if (canAvoidDamage){
                avoidMine();
            }
		}

        if (Input.GetKeyUp(KeyCode.K)){
            pressingK = false;
        }
        // // Update hit rate
        // GameOverScreen.instance.GetHitRate();
        // ScoreManager.instance.GetHitRate();
        // Update the final score
        GameOverScreen.instance.getScore();
        ScoreManager.instance.GetTotalScore();
        // Update the rank
        GameOverScreen.instance.GetRank();
        ScoreManager.instance.GetRank();
        //Debug.Log(hitScore + "/" + missScore);
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.K))
		{  
            pressingK = true;
            animator.SetBool("isEating",true);
            itemReleaseTimeCounter += Time.fixedDeltaTime;
            if (canGetLongScore){
                ScoreLong();
            }
            
		}
    }

    // Score on single diamond function
    void ScoreSingle(float scoreTime)
    {
        hitScore++;
        targetPanel.TargetHit(toHit.GetComponent<SpriteRenderer>().color);
        // best way is to set tag for each color of gem
        // if(toHit.tag == "food"){
            // toHit.SetActive(false);
        // }else{
            Destroy(toHit);
        // }
        if (scoreTime - collsionTime < 0.03f){
            addHitEffect(hitEffect);
            GameOverScreen.instance.IncreaseScore();
        } else if (scoreTime - collsionTime < 0.07f){
            addHitEffect(goodEffect);
            GameOverScreen.instance.DoubleScore();
        } else {
            addHitEffect(perfectEffect);
            GameOverScreen.instance.TripleScore();
        }
        // Update hit times
        ScoreManager.instance.AddHit();
        // // Update final score
        // GameOverScreen.instance.IncreaseScore();
        // add one when eating one
        numOfFood++;
    }

    void ScoreLong()
    {
        if(itemReleaseTimeCounter > itemReleaseTimeBar){
            if(!inventory.isEmpty()){
                Item rlsItem = inventory.RemoveFirst();
                boss.TakeDamage(rlsItem);
                itemReleaseTimeCounter = 0f;

                hitScore++;
                
                addHitEffect(hitEffect);
                // Update hit times
                ScoreManager.instance.AddHit();
                // Update final score
                GameOverScreen.instance.IncreaseScore();
                // Update hit rate
                ScoreManager.instance.CalHitRate();
                GameOverScreen.instance.CalHitRate();
            }
        }
    }

    void MissSingle()
    {
        missScore++;
        addHitEffect(missEffect);
        // Update miss times
        ScoreManager.instance.AddMiss();
        // Update final score
        GameOverScreen.instance.DecreaseScore();
        // Update hit rate
        ScoreManager.instance.CalHitRate();
        GameOverScreen.instance.CalHitRate();
    }

    // Mine collision functions
    void avoidMine()
    {
        missMine = false;
        Destroy(toHit);
        addHitEffect(hitEffect);
    }

    void Collide(int damage)
    {
        nextTime = Time.time + 0.3f;
        animator.SetBool("isDamaged",true);
        addHitEffect(missEffect);
        Destroy(toHit);
        // damage
        TakeDamage(damage);
        // add blood effect
        if (isUpsideDown){
            Instantiate(bloodEffectCeil, transform.position + new Vector3(0, -0.2f, 0), bloodEffectCeil.transform.rotation);
        }
        else
        {
            Instantiate(bloodEffectFloor, transform.position + new Vector3(0,0.2f,0), bloodEffectFloor.transform.rotation);
        }
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

    void addHitEffect(GameObject effectType)
    {
        if (isUpsideDown)
        {
            Instantiate(effectType, transform.position + new Vector3(-2.0f,-1.0f,0), effectType.transform.rotation);
        }
        else
        {
            Instantiate(effectType, transform.position + new Vector3(-2.0f,1.0f,0), effectType.transform.rotation);
        }
    }

    // The following two functions can be used to set the changing gravity point.

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
            // Vector2 temp = GetComponent<Transform>().position;
            // if (temp.y>-2 && temp.y<2){
            //     canCross = true;
            // }
        }

        if(collision.gameObject.tag == "Platform"){
            canChangeGravity = true;
            canCross = true;
        }


        if(collision.gameObject.tag == "food")
        {
            missFood = true;
            toHit = collision.gameObject;
            canGetSingleScore = true;
            collsionTime = Time.time;
        }

        if(collision.gameObject.tag == "LongNote")
        {
            canGetLongScore = true;
            // boss.isHide = false;
        }

        if(collision.gameObject.tag == "Mine")
        {
            missMine = true;
            toHit = collision.gameObject;
            canAvoidDamage = true;
        }
        if(collision.gameObject.tag == "Laser")
        {
            Collide(20);
            toHit = null;
            canAvoidDamage = false;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // if(collision.gameObject.tag == "GravSwitch")
        // {
        //     canChangeGravity = false;
            
        // }

        if(collision.gameObject.tag == "food")
        {
            if (missFood){
                MissSingle();
            }
            missFood = false;
            toHit = null;
            canGetSingleScore = false;
        }

        if(collision.gameObject.tag == "LongNote")
        {
            canGetLongScore = false;
            // boss.SwitchState();
            // boss.isHide = true;
        }

        if(collision.gameObject.tag == "Mine")
        {
            if (missMine){
                Collide(10);
            }
            toHit = null;
            canAvoidDamage = false;
        }
    }
}
