using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb2D;

    SpriteRenderer fireRenderer;
    SpriteRenderer grassRenderer;
    SpriteRenderer waterRenderer;
    SpriteRenderer rockRenderer;
    public static event Action OnPlayerDeath;
    public static PlayerControl instance;

    // Healthbar
    [SerializeField]
    public int maxHealth = 100; 
    public int currentHealth;
    public HealthBar healthBar;

    public GameObject fireDiamond;
    public HealthBar fireBar;
    public GameObject grassDiamond;
    public HealthBar grassBar;
    public GameObject waterDiamond;
    public HealthBar waterBar;
    public GameObject rockDiamond;
    public HealthBar rockBar;
    private int fireCount = 0;
    private int grassCount = 0;
    private int waterCount = 0;
    private int rockCount = 0;

    // Boss 
    public BossUI boss;

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

    private float longNoteScoreTimeCounter = 0f;
    private float longNoteScoreTimeBar = 0.2f;
    
    private bool canGetSingleScore;
    private bool canGetLongScore;
    private bool canAvoidDamage;
    public bool canChangeGravity;
    public bool canCross;
    private bool missFood;
    private bool missMine;
    private bool pressingK;

    float[] playerYPosArr;
    public int curYPos;

    // for hitting and blood effect
    public GameObject hitEffect, goodEffect, perfectEffect ,missEffect, bloodEffectCeil, bloodEffectFloor;
    
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

        nextTime = Time.time;
        canGetSingleScore = false;
        
        playerYPosArr = new float[4];
        playerYPosArr[0] = 3.4f;
        playerYPosArr[1] = 1.6f;
        playerYPosArr[2] = -1.6f;
        playerYPosArr[3] = -3.4f;
        curYPos = 1;
        isUpsideDown = false;

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        
        animator = GetComponent<Animator>();

        
        targetPanel = TargetPanel.Instance;
        // inventory = targetPanel.inventory;

        fireBar.SetMinHealth(fireCount);
        grassBar.SetMinHealth(grassCount);
        waterBar.SetMinHealth(waterCount);
        rockBar.SetMinHealth(rockCount);

        fireRenderer = fireDiamond.GetComponent<SpriteRenderer>();
        grassRenderer = grassDiamond.GetComponent<SpriteRenderer>();
        waterRenderer = waterDiamond.GetComponent<SpriteRenderer>();
        rockRenderer = rockDiamond.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPosition = transform.position;
        if (Time.time >= nextTime && !pressingK) {
            animator.SetBool("isEating",false);
            animator.SetBool("isDamaged",false);
        }
        
        
        if (canChangeGravity)
		{
            if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && curYPos !=0)
            {
                if(curYPos!=2){
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(curPosition.x, playerYPosArr[curYPos-1], curPosition.z), 2f);
                }else{
                    transform.position = new Vector3(curPosition.x, playerYPosArr[curYPos-1], curPosition.z);
                }
                curYPos -=1;
                rb2D.gravityScale*=-1;
                isUpsideDown = !isUpsideDown;
                canChangeGravity = false;
                animator.SetBool("UpsideDown",isUpsideDown);
            }
            if((Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow)) && curYPos !=3)
            {
                if(curYPos!=1){
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(curPosition.x, playerYPosArr[curYPos+1], curPosition.z), 2f);
                }else{
                    transform.position = new Vector3(curPosition.x, playerYPosArr[curYPos+1], curPosition.z);
                }
                curYPos +=1;
                rb2D.gravityScale*=-1;
                isUpsideDown = !isUpsideDown;
                canChangeGravity = false;
                animator.SetBool("UpsideDown",isUpsideDown);
            }
		}

        if (Input.GetKeyDown(KeyCode.Space))
		{
            missFood = false;
            animator.SetBool("isEating",true);
            nextTime = Time.time + 0.1f; //Eating time lasts for 0.1s
            if (canGetSingleScore){
                ScoreSingle(Time.time);
            }
            else {
                TakeDamage(5);
            }
            if (canAvoidDamage){
                avoidMine();
            }
		}

        if (Input.GetKeyUp(KeyCode.Space)){
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
        // Reset diamond
        DiamondCollection.Instance.Reset();
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.Space))
		{  
            pressingK = true;
            animator.SetBool("isEating",true);
            longNoteScoreTimeCounter += Time.fixedDeltaTime;
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
        
        if (toHit.GetComponent<SpriteRenderer>().color == fireRenderer.color)
        {
            DiamondCollection.Instance.AddFireCount();
        } else if (toHit.GetComponent<SpriteRenderer>().color == grassRenderer.color)
        {
            DiamondCollection.Instance.AddGrassCount();
        } else if (toHit.GetComponent<SpriteRenderer>().color == waterRenderer.color)
        {
            DiamondCollection.Instance.AddWaterCount();
        } else if (toHit.GetComponent<SpriteRenderer>().color == rockRenderer.color)
        {
            DiamondCollection.Instance.AddRockCount();
        }
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
        if(longNoteScoreTimeCounter > longNoteScoreTimeBar){
            longNoteScoreTimeCounter = 0;
            DiamondCollection.Instance.AddFireCount();
            DiamondCollection.Instance.AddWaterCount();
            DiamondCollection.Instance.AddGrassCount();
            DiamondCollection.Instance.AddRockCount();
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
