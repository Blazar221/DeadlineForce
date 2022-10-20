using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float bossMoveSpeed = 0.3f; 

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
    public GameObject colorBody;
    public GameObject boss;

    public static Boss instance;

    public bool isHide;
    public bool startMove;

    // Line Index from top to bottom: 0, 1, 2, 3
    public int attackingLine;
    public float moveDestY;
    public int curLine;
    public Vector3 moveDest;

    public enum ElemType {Fire, Water, Grass, Rock};
    public ElemType eleType;


    void Start()
    {
        instance = this;
        healthBar.SetMaxHealth(bossHealth);
        // Get the corresponding property of the gameObject
        render = colorBody.GetComponent<SpriteRenderer>();
        originalColor = render.color;
        
        bossAnimator = GetComponent<Animator>();
        
        SwitchState();
        SwitchState();
        StartCoroutine(AutoAttack());

        isHide = true;
        startMove = false;
    }

    void Update()
    {
        if(startMove)
        {
            CheckMoveEnd();
        }
        // if(isHide){
        //     Hide();
        // }else{
        //     Appear();
        // }
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

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

    public void SwitchState()
    {
        state++;
        Debug.Log("state:"+state);
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

    void FireState() 
    {
        // Debug.Log("fire");
        // 0.1f is the smoothing factor
        render.color = Color.Lerp(render.color, Color.red, 1);
        eleType = ElemType.Fire;
    }

    void WaterState()
    {
        // Debug.Log("water");
        render.color = Color.Lerp(render.color, Color.blue, 1);
        eleType = ElemType.Water;
    }

    void GrassState()
    {
        // Debug.Log("grass");
        render.color = Color.Lerp(render.color, Color.green, 1);
        eleType = ElemType.Grass;
    }

    void RockState()
    {
        // Debug.Log("earth");
        render.color = Color.Lerp(render.color, Color.yellow, 1);
        eleType = ElemType.Rock;
    }

    // public void Appear()
    // {
    //     boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(7.34f, 0.63f, 0.0786f), .02f);
    // }

    // public void Hide()
    // {
    //     boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(9.18f, 0.63f, 0.0786f), .02f);
    // }

    public void TakeDamage(Item attackingItem)
    {
        int attackingVal = attackingItem.itemType switch 
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

        FlashColor(flashTime);

        bossHealth -= CalcDamage(attackingVal, defendingVal);
        healthBar.SetHealth(bossHealth);

        if (bossHealth <= 0)
        {
            Dead();
            GameController.Instance.EnableCongratsMenu();
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
        render.color = Color.white;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        render.color = originalColor;
    }
}
