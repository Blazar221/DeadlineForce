using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    [SerializeField] public PlayerControl player;

    [SerializeField] private float bossMoveSpeed = 0.3f;
    [SerializeField] private float bossAttackPeriod = 10f;

    private int state = 0;
    private float _fireRate;
    private float _canFire = 2f;
    public int[] stateCount = {0,0,0};
    private Color originalColor;

    [SerializeField] public int bossHealth = 100;

    [SerializeField] private GameObject laser;
    
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

    public GameObject bloodEffect;
    
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

        originalColor = Color.white;
        
        bossAnimator = GetComponent<Animator>();
        
        StartCoroutine(AutoAttack());
        startMove = false;
    }

    void Update()
    {
        if(startMove)
        {
            CheckMoveEnd();
        }
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossAttackPeriod);

            attackingLine = player.curYPos;
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

            // blood effect
            Instantiate(bloodEffect, bossHead.transform.position, Quaternion.identity);

            FlashColor(flashTime);

            // TODO give real data
            bossHealth -= CalcDamage(1,2,3,4);
            healthBar.SetHealth(bossHealth);

            if (bossHealth <= 0)
            {
                Dead();
                GameController.Instance.EnableCongratsMenu();
            }
        }
        
    }

    public int CalcDamage(int rCnt, int bCnt, int gCnt, int yCnt){
        int groupCnt = Mathf.Min(rCnt, bCnt, gCnt, yCnt);
        
        return groupCnt * 30 + rCnt*rCnt/2 + bCnt*bCnt/2 + gCnt*gCnt/2 + yCnt*yCnt/2;
    }

    void Dead()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Effect of being attacked
    void FlashColor(float time)
    {
        SetColor(Color.red);
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        SetColor(originalColor);
    }
}
