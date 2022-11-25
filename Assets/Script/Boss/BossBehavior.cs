using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public static BossBehavior Instance;

    [SerializeField] private GameObject Alert0;
    [SerializeField] private GameObject Alert1;
    [SerializeField] private GameObject Alert2;
    [SerializeField] private GameObject Alert3;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float bossMoveSpeed = 1f;
    [SerializeField] private float bossAttackPeriod = 5f;

    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject bandit;
    public int laserHarm=20, banditHarm=10;
    
    private int _count=0;
    
    private int attackingLine;
    private float moveDestY;
    private Vector3 moveDest;

    bool moving = false;
    bool attackWaiting = false;
    bool retreatWaiting = false;

    bool canHurtPlayer = true;

    private Vector3 originalLocalScale;

    [SerializeField] private Animator bossAnimator;
    private BossUI _bossUI;
    
    private GameObject _newBullet;
    
    [SerializeField] private GameObject bgm;
    [SerializeField] private float struggleStartTime;
    private BgmController _bgmHandler;
    private bool _startStruggle = false;

    private Vector3 playerOffset = new Vector3(2f, 0f, 0f);
    private float playerX = -2f;
    private float bossX = 5f;

    private float[] bigRedYArr = {3.77f, 1.81f, -1.55f, -3.53f};
    private float[] currentYArr;

    private Color freezeColor = new Color(0f, 0.9f, 0.9f, 1f);

    void Awake()
    {   
        Instance = this;
        bossAnimator = GetComponent<Animator>();

        _bossUI = GetComponent<BossUI>();
        _bgmHandler = bgm.GetComponent<BgmController>();

        switch(name)
        {
            case "BigRed":
                currentYArr = bigRedYArr;
                break;
            default:
                break;
        }

        originalLocalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Start()
    {
        Alert0.GetComponent<SpriteRenderer>().enabled = false;
        Alert0.GetComponent<Animator>().enabled = false;
        Alert1.GetComponent<SpriteRenderer>().enabled = false;
        Alert1.GetComponent<Animator>().enabled = false;
        Alert2.GetComponent<SpriteRenderer>().enabled = false;
        Alert2.GetComponent<Animator>().enabled = false;
        Alert3.GetComponent<SpriteRenderer>().enabled = false;
        Alert3.GetComponent<Animator>().enabled = false;

        switch(name){
            case "BigRed":
                StartCoroutine(BigRedAutoAttack());
                break;
            default:
                break;
        }
        // StartCoroutine(AutoAttack());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moving)
        {
            CheckMoveEnd();
        }
    }

    public void CheckMoveEnd()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDest, bossMoveSpeed);
        if(transform.position == moveDest){
            moving = false;
            bossAnimator.SetBool("isMove", false);
            if(attackWaiting)
            {  
                attackWaiting = false;
                bossAnimator.SetTrigger("attack");
                if(canHurtPlayer){
                    PlayerHealth.Instance.TakeDamage(20);
                }
            }
            if(retreatWaiting){
                StartCoroutine(CallRetreat());
            }
        }
    }

    IEnumerator CallRetreat(){
        switch(name)
        {
            case "BigRed":
                yield return new WaitForSeconds(1f);
                CallBigRedRetreat();
                break;
            default:
                break;
        }
    }

    public void Freeze()
    {
        bossMoveSpeed /= 10;
        bossAttackPeriod *= 2;
        BossUI.Instance.SetColor(freezeColor);
        StartCoroutine(Unfreeze());
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(8f);
        bossMoveSpeed *= 10;
        bossAttackPeriod /=2;
        BossUI.Instance.SetColor(Color.white);
    }

    void SetLocalScale(){
        if(attackingLine == 0 || attackingLine == 2)
        {
            transform.localScale = new Vector3(originalLocalScale.x, -originalLocalScale.y, originalLocalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y, originalLocalScale.z);
        }
    }

    /***********************************
    **          Boss: BigRed          **
    ************************************/
    IEnumerator BigRedAutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossAttackPeriod);
            CallBigRedTrack();
        }
    }

    void CallBigRedTrack()
    {
        attackingLine = playerMovement.GetYPos();
        moveDestY = currentYArr[attackingLine];
        moveDest = new Vector3(playerX, moveDestY, 0);

        SetLocalScale();

        moving = true;
        attackWaiting = true;
        retreatWaiting = true;

        bossAnimator.SetBool("isMove", true);
    }

    void CallBigRedRetreat()
    {
        attackingLine = playerMovement.GetYPos();
        moveDestY = currentYArr[attackingLine];
        moveDest = new Vector3(bossX, moveDestY, 0);

        SetLocalScale();

        moving = true;
        attackWaiting = false;
        retreatWaiting = false;

        bossAnimator.SetBool("isMove", true);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Player")
        {
            canHurtPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.name == "Player")
        {
            canHurtPlayer = false;
        }
    }

    
    // // Line Index from top to bottom: 0, 1, 2, 3
    // public IEnumerator AutoAttack()
    // {

    //     while (true)
    //     {
    //         yield return new WaitForSeconds(bossAttackPeriod);

    //         attackingLine = playerMovement.GetYPos();
    //         moveDestY = currentYArr[attackingLine];

    //         if(attackingLine == 0 || attackingLine == 2)
    //         {
    //             transform.localScale = new Vector3(originalLocalScale.x, -originalLocalScale.y, originalLocalScale.z);
    //         }
    //         else
    //         {
    //             transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y, originalLocalScale.z);
    //         }

    //         moveDest = new Vector3(transform.position.x, moveDestY, transform.position.z);
    //         moving = true;
    //         bossAnimator.SetBool("isMove", true);
            
    //         if (_startStruggle == false && _bgmHandler.songPosition > struggleStartTime)
    //         {
    //             Struggle();
    //             _bossUI.StruggleColor();
    //             _startStruggle = true;
    //         }
    //     }
    // }
    
    // public void Attack()
    // {
    //     switch(name){
    //         case "BigRed":
    //             CallBigRedAttack();
    //             break;
    //         default:
    //             break;
    //     }
    //     // StartCoroutine(SpawnAttack());
    // }
    
    public void StartAlert(int pos)
    {
        switch (pos)
        {
            case 0:
                Alert0.GetComponent<SpriteRenderer>().enabled = true;
                Alert0.GetComponent<Animator>().enabled = true;
                break;
            case 1:
                Alert1.GetComponent<SpriteRenderer>().enabled = true;
                Alert1.GetComponent<Animator>().enabled = true;
                break;
            case 2:
                Alert2.GetComponent<SpriteRenderer>().enabled = true;
                Alert2.GetComponent<Animator>().enabled = true;
                break;
            case 3:
                Alert3.GetComponent<SpriteRenderer>().enabled = true;
                Alert3.GetComponent<Animator>().enabled = true;
                break;
            default:
                break;
        }
    }
    
    public void EndAlert(int pos)
    {
        switch (pos)
        {
            case 0:
                Alert0.GetComponent<SpriteRenderer>().enabled = false;
                Alert0.GetComponent<Animator>().enabled = false;
                break;
            case 1:
                Alert1.GetComponent<SpriteRenderer>().enabled = false;
                Alert1.GetComponent<Animator>().enabled = false;
                break;
            case 2:
                Alert2.GetComponent<SpriteRenderer>().enabled = false;
                Alert2.GetComponent<Animator>().enabled = false;
                break;
            case 3:
                Alert3.GetComponent<SpriteRenderer>().enabled = false;
                Alert3.GetComponent<Animator>().enabled = false;
                break;
            default:
                break;
        }
    }

    // IEnumerator SpawnAttack()
    // {
    //     float pos1 = LineIndToPos(attackingLine),
    //         pos2 = LineIndToPos((attackingLine + 3) % 4),
    //         pos3 = LineIndToPos((attackingLine + 1) % 4);
    //     switch (_count)
    //     {
    //         case 0:
    //             StartAlert(attackingLine);
    //             yield return new WaitForSeconds(1.2f);
    //             _newBullet = Instantiate(laser, new Vector3(-4, pos1, 0), Quaternion.identity);
    //             Destroy(_newBullet, 1f);
    //             EndAlert(attackingLine);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 3) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             _newBullet = Instantiate(bandit, new Vector3(6, pos2, 0), Quaternion.identity);
    //             Destroy(_newBullet, 2f);
    //             EndAlert((attackingLine + 3) % 4);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 1) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             _newBullet = Instantiate(bandit, new Vector3(6, pos3, 0), Quaternion.identity);
    //             Destroy(_newBullet, 2f);
    //             EndAlert((attackingLine + 1) % 4);
    //             break;
    //         default:
    //             StartAlert(attackingLine);
    //             yield return new WaitForSeconds(1.2f);
    //             _newBullet = Instantiate(bandit, new Vector3(6, pos1, 0), Quaternion.identity);
    //             Destroy(_newBullet, 2f);
    //             EndAlert(attackingLine);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 3) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             _newBullet = Instantiate(bandit, new Vector3(6, pos2, 0), Quaternion.identity);
    //             Destroy(_newBullet, 2f);
    //             EndAlert((attackingLine + 3) % 4);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 1) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             _newBullet = Instantiate(bandit, new Vector3(6, pos3, 0), Quaternion.identity);
    //             Destroy(_newBullet, 2f);
    //             EndAlert((attackingLine + 1) % 4);
    //             break;
    //             ;
    //     }
    //     _count = (_count + 1)%3;
    // }

    public void Struggle()
    {
        bossAttackPeriod *= 0.9f;
        laserHarm = (int)(laserHarm*1.5f);
        banditHarm = (int)(banditHarm*1.5f);
        _bossUI.originalColor = Color.black;
    }

    float LineIndToPos(int ind)
    {
        var yPos = ind switch
        {
            0 => 4.2f,
            1 => 1.25f,
            2 => -1.25f,
            3 => -4.2f,
            _ => 0,
        };
        return yPos;
    }
}
