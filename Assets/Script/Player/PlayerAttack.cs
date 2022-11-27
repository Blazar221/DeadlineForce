using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool pressingK;
    // The diamond to destroy
    private GameObject toHit, toGemHit;

    private int hitScore;
    private int missScore;
    private bool isUpsideDown;
    private bool isEating;
    private float nextTime;
    private float collsionTime;
    private float longNoteScoreTimeCounter = 0f;
    private float longNoteScoreTimeBar = 0.2f;
    
    private bool canGetSingleScore;
    private int _collideGemNum;
    private bool canGetLongScore;
    private bool canAvoidDamage;
    private bool canChangeGravity;
    private bool canCross;
    private bool missFood;
    private bool missMine;

    private Animator animator;

    [SerializeField]
    public GameObject hitEffect;
    public GameObject missEffect;
    public GameObject goodEffect;
    public GameObject perfectEffect;
    
    // Diamond collection
    SpriteRenderer fireRenderer;
    SpriteRenderer grassRenderer;
    SpriteRenderer waterRenderer;
    SpriteRenderer rockRenderer;
    public GameObject fireDiamond;
    public SliderBar fireBar;
    public GameObject grassDiamond;
    public SliderBar grassBar;
    public GameObject waterDiamond;
    public SliderBar waterBar;
    public GameObject rockDiamond;
    public SliderBar rockBar;
    private int fireCount = 0;
    private int grassCount = 0;
    private int waterCount = 0;
    private int rockCount = 0;

    private bool isClone;

    // Boss 
    [SerializeField] private GameObject _boss;
    private BossBehavior _bossHandler;

    void Awake()
    {
        hitScore = 0;
        missScore = 0;
        pressingK = false;
        isUpsideDown = false;
        canChangeGravity = true;

        nextTime = Time.time;
        canGetSingleScore = false;
        _collideGemNum = 0;

        animator = GetComponent<Animator>();
        _bossHandler = _boss.GetComponent<BossBehavior>();

        fireBar.SetMinValue(fireCount);
        grassBar.SetMinValue(grassCount);
        waterBar.SetMinValue(waterCount);
        rockBar.SetMinValue(rockCount);
        fireRenderer = fireDiamond.GetComponent<SpriteRenderer>();
        grassRenderer = grassDiamond.GetComponent<SpriteRenderer>();
        waterRenderer = waterDiamond.GetComponent<SpriteRenderer>();
        rockRenderer = rockDiamond.GetComponent<SpriteRenderer>();

        isClone = (name == "PlayerClone");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime && !pressingK) {
            animator.SetBool("isEating",false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
		{
            missFood = false;
            Debug.Log("press");
            animator.SetBool("isEating",true);
            nextTime = Time.time + 0.1f; //Eating time lasts for 0.1s
            if (canGetSingleScore){
                ScoreSingle(Time.time);
            }
            else if (canGetLongScore == false){
                if(!isClone){
                    PlayerHealth.Instance.TakeDamage(5, false);
                }
            }
            if (canAvoidDamage){
                avoidMine();
            }
		}

        if (Input.GetKeyUp(KeyCode.Space)){
            pressingK = false;
        }
        // Reset diamond
        CollectionController.Instance.Reset();
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
        if(toGemHit != null)
        {
            TargetPanel.Instance.TargetHit(toGemHit.GetComponent<SpriteRenderer>().color);
        }
        if (toGemHit.GetComponent<SpriteRenderer>().color == fireRenderer.color)
        {
            CollectionController.Instance.AddFireCount();
        } else if (toGemHit.GetComponent<SpriteRenderer>().color == grassRenderer.color)
        {
            CollectionController.Instance.AddGrassCount();
        } else if (toGemHit.GetComponent<SpriteRenderer>().color == waterRenderer.color)
        {
            CollectionController.Instance.AddWaterCount();
        } else if (toGemHit.GetComponent<SpriteRenderer>().color == rockRenderer.color)
        {
            CollectionController.Instance.AddRockCount();
        }

        Destroy(toGemHit);

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
    }

    void ScoreLong()
    {
        if(longNoteScoreTimeCounter > longNoteScoreTimeBar){
            longNoteScoreTimeCounter = 0;
            CollectionController.Instance.AddFireCount();
            CollectionController.Instance.AddWaterCount();
            CollectionController.Instance.AddGrassCount();
            CollectionController.Instance.AddRockCount();
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
        addHitEffect(missEffect);
        Destroy(toHit);
        // damage
        if(!isClone){
            PlayerHealth.Instance.TakeDamage(damage);
        }
    }

    public void CallAttack()
    {
        animator.SetTrigger("attack");
    }

    void addHitEffect(GameObject effectType)
    {
        if (isUpsideDown)
        {
            Instantiate(effectType, transform.position + new Vector3(-3.0f,0.0f,0), effectType.transform.rotation);
        }
        else
        {
            Instantiate(effectType, transform.position + new Vector3(-3.0f,0.0f,0), effectType.transform.rotation);
        }
    }

    // The following two functions can be used to set the changing gravity point.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "food")
        {
            missFood = true;
            toGemHit = collision.gameObject;
            canGetSingleScore = true;
            _collideGemNum++;
            collsionTime = Time.time;
            Debug.Log("onEnter: "+ _collideGemNum);
        }

        if(collision.gameObject.tag == "LongNote")
        {
            canGetLongScore = true;
        }

        if(collision.gameObject.tag == "Bandit")
        {
            missMine = true;
            toHit = collision.gameObject;
            canAvoidDamage = true;
        }
        if(collision.gameObject.tag == "Laser")
        {
            Collide(_bossHandler.laserHarm);
            toHit = null;
            canAvoidDamage = false;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "food")
        {
            if (missFood){
                MissSingle();
            }
            missFood = false;
            _collideGemNum--;
            if (_collideGemNum == 0)
            {
                toGemHit = null;
                canGetSingleScore = false;
            }
            Debug.Log("onExit: "+ _collideGemNum);
        }

        if(collision.gameObject.tag == "LongNote")
        {
            canGetLongScore = false;
        }

        if(collision.gameObject.tag == "Bandit")
        {
            if (missMine){
                Collide(20);
            }
            toHit = null;
            canAvoidDamage = false;
        }
    }
}
