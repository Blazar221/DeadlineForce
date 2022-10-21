using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    [SerializeField] private float bossMoveSpeed = 0.3f;
    [SerializeField] private float bossAttackPeriod = 10f;

    private int state = 0;
    private float _fireRate;
    private float _canFire = 2f;
    public int[] stateCount = {0,0,0};
    private SpriteRenderer render;
    private Color originalColor;

    [SerializeField] public int bossHealth = 100;

    [SerializeField] private GameObject laser;
    
    [SerializeField] private int lineNum;
    
    public Animator bossAnimator;

    public float speed = 1f;
    public float flashTime = 0.3f;
    public HealthBar healthBar;
    public GameObject deathEffect;

    public GameObject bossBody;
    public GameObject bossHead;
    public GameObject bossLeftLeg;
    public GameObject bossRightLeg;
    List<SpriteRenderer> bossRenderers;
    
    bool startMove;

    // Line Index from top to bottom: 0, 1, 2, 3
    public int attackingLine;
    public float moveDestY;
    public int curLine = 2;
    public Vector3 moveDest;

    public enum ElemType {Blank, Fire, Water, Grass, Rock};
    public ElemType eleType;

    void Start()
    {
        instance = this;
        
        healthBar.SetMaxHealth(bossHealth);
        // Get the corresponding property of the gameObject
        bossRenderers = new List<SpriteRenderer>();
        bossRenderers.Add(bossBody.GetComponent<SpriteRenderer>());
        bossRenderers.Add(bossHead.GetComponent<SpriteRenderer>());
        bossRenderers.Add(bossLeftLeg.GetComponent<SpriteRenderer>());
        bossRenderers.Add(bossRightLeg.GetComponent<SpriteRenderer>());

        originalColor = Color.black;
        
        bossAnimator = GetComponent<Animator>();
        
        // Level Control
        // if (SceneManager.GetActiveScene().name == "Level1")
        // {
        //     BlankState();
        //     Debug.Log("level1");
        // } else if (SceneManager.GetActiveScene().name == "Level2")
        // {
        //     Debug.Log("level2");
        //     SwitchState();
        //     SwitchState();
        // } else if (SceneManager.GetActiveScene().name == "Level3")
        // {
        //     Debug.Log("level3");
        //     SwitchState();
        //     SwitchState();
        // }

        // SwitchState();
        // SwitchState();
        
        StartCoroutine(AutoAttack());
        startMove = false;
    }

    void Update()
    {
        if(startMove)
        {
            CheckMoveEnd();
        }

        // // Level control
        // if (SceneManager.GetActiveScene().name == "Level1")
        // {
        //     BlankState();
        //     // Debug.Log("level1");
        // } else if (SceneManager.GetActiveScene().name == "Level2")
        // {
        //     // Debug.Log("level2");
        //     SwitchState();
        //     SwitchState();
        // } else if (SceneManager.GetActiveScene().name == "Level3")
        // {
        //     // Debug.Log("level3");
        //     SwitchState();
        //     SwitchState();
        // }
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossAttackPeriod);

            while(attackingLine == curLine)
            {
                attackingLine = Random.Range(1, 3);
            }

            moveDestY = attackingLine switch
            {
                0 => 7,
                1 => 4,
                2 => 2,
                3 => -1,
                _ => 0,
            };

            moveDest = new Vector3(9.34f, moveDestY, 0);
            startMove = true;
            bossAnimator.SetBool("isMove", true);
        }
    }

    void CheckMoveEnd()
    {
        Debug.Log("moveDest" + moveDest);
        Debug.Log("transform" + transform.position);
        transform.position = Vector3.MoveTowards(transform.position, moveDest, bossMoveSpeed);
        if(transform.position.y == moveDestY){
            curLine = attackingLine;
            startMove = false;
            bossAnimator.SetBool("isMove", false);
            bossAnimator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        float yPos = attackingLine switch
        {
            0 => 4,
            1 => 1,
            2 => -1,
            3 => -4,
            _ => 0,
        };
        var newItem = Instantiate(laser, new Vector3(-1, yPos, 0), Quaternion.identity);
        Destroy(newItem, 1f);
    }

    /*
    public void SwitchState()
    {
        state++;
        // Debug.Log("state:"+state);
        switch (state%8)
        {
            case 0:
                FireState();
                break;
            case 2:
                WaterState();
                break;
            case 4:
                GrassState();
                break;
            case 6:
                RockState();
                break;
        }
    }

    void BlankState()
    {
        eleType = ElemType.Blank;
        SetColor(Color.black);
    }

    void FireState() 
    {
        eleType = ElemType.Fire;
        SetColor(Color.red);
    }

    void WaterState()
    {
        eleType = ElemType.Water;
        SetColor(Color.blue);
    }

    void GrassState()
    {
        eleType = ElemType.Grass;
        SetColor(Color.green);
    }

    void RockState()
    {
        eleType = ElemType.Rock;
        SetColor(Color.yellow);
    }
    */

    void SetColor(Color nextColor)
    {
        foreach(SpriteRenderer renderer in bossRenderers)
        {
            renderer.color = Color.Lerp(renderer.color, nextColor, 1);
        }
    }
    
    public void TakeDamage(List<Item> attackingItems)
    {
        for (int i = 0; i < attackingItems.Count; i++)
        {
            int attackingVal = attackingItems[i].itemType switch 
            {
                Item.ItemType.Water => 0,
                Item.ItemType.Fire => 1,
                Item.ItemType.Grass => 2,
                _ => 3,
            };
            int defendingVal = eleType switch 
            {
                ElemType.Water => 0,
                ElemType.Fire => 1,
                ElemType.Grass => 2,
                _ => 3,
            }; 

            // FlashColor(flashTime);

            bossHealth -= CalcDamage(attackingVal, defendingVal);
            healthBar.SetHealth(bossHealth);

            if (bossHealth <= 0)
            {
                Dead();
                GameController.Instance.EnableCongratsMenu();
            }
        }
        
    }

    public int CalcDamage(int attacking, int defending){
        int baseDmg = 5;
        if((attacking == 3 && defending == 0)||(defending - attacking == 1))
        {
            baseDmg *= 2;
            stateCount[1]++;
        }else if(((defending == 3 && attacking == 0)||(attacking - defending == 1)))
        {
            baseDmg /= 2;
            stateCount[2]++;
        }else{
            stateCount[0]++;
        }
        return baseDmg;
    }

    void Dead()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Effect of being attacked
    void FlashColor(float time)
    {
        SetColor(Color.white);
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        SetColor(originalColor);
    }
}
