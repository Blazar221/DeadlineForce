using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public static BossBehavior Instance;

    private PlayerMovement playerMovement;

    private Vector3 originalLocalScale;

    private Animator bossAnimator;


    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject missile;
    private GameObject newItem;

    public int laserHarm = 20, missileHarm = 10;
    
    private float playerX = -2f;
    private float bossX = 5f;

    private int attackCounter = 0;
    // Boss Track & Attack Var
    private int attackingLine;
    private float moveDestY;
    private Vector3 moveDest;
    // Boss State Flag    
    bool moving = false;
    bool meleeAttackWaiting = false;
    bool rangeAttackWaiting = false;
    bool retreatWaiting = false;
    
    bool startStruggle = false;
    // Boss Unique Data
    private float[] bossYArr;
    private float bossMoveSpeed;
    private float bossAttackPeriod;
    private int bossMeleeHarm;
    private float bossAttackPoint;
    // Boss Color Change
    private Color freezeColor = new Color(0f, 0.9f, 0.9f, 1f);

    void Awake()
    {   
        Instance = this;
        bossAnimator = GetComponent<Animator>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        //Init Boss Unique Data
        switch(name)
        {
            case "BigRed":
                bossYArr = new float[]{3.77f, 1.81f, -1.55f, -3.53f};
                bossMoveSpeed = 0.3f;
                bossAttackPeriod = 8f;
                bossMeleeHarm = 20;
                bossAttackPoint = 0.1f;
                break;
            case "Orc":
                bossYArr = new float[]{3.77f, 1.81f, -1.55f, -3.53f};
                bossMoveSpeed = 0.3f;
                bossAttackPeriod = 7f;
                bossMeleeHarm = 20;
                bossAttackPoint = 0.1f;
                break;
            case "Rebo":
                bossYArr = new float[]{3.77f, 1.81f, -1.55f, -3.53f};
                bossMoveSpeed = 0.3f;
                bossAttackPeriod = 7f;
                bossMeleeHarm = 20;
                bossAttackPoint = 0.1f;
                break;
            default:
                break;
        }

        originalLocalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Start()
    {
        AlertController.Instance.EndAllAlert();
        
        StartCoroutine(AutoAttack());
    }

    IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossAttackPeriod);
            switch(name)
            {
                case "BigRed":
                    CallBigRedTrack();
                    break;
                case "Orc":
                    CallOrcTrack();
                    break;
                case "Rebo":
                    CallReboTrack();
                    break;
                default:
                    break;
            }
        }
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
            if(meleeAttackWaiting)
            {  
                meleeAttackWaiting = false;
                StartCoroutine(CallMeleeAttack());
            }
            if(rangeAttackWaiting)
            {  
                rangeAttackWaiting = false;
                CallRangeAttack();
            }
            if(retreatWaiting){
                retreatWaiting = false;
                StartCoroutine(CallRetreat());
            }
        }
    }

    IEnumerator CallMeleeAttack(){
        switch(name)
        {
            case "BigRed":
                yield return new WaitForSeconds(bossAttackPoint);
                bossAnimator.SetTrigger("attack");
                
                AlertController.Instance.EndAllAlert();

                if(canHurtPlayer()){
                    PlayerHealth.Instance.TakeDamage(bossMeleeHarm);
                }
                break;
            case "Orc":
                yield return new WaitForSeconds(bossAttackPoint);
                bossAnimator.SetTrigger("attack");
                
                AlertController.Instance.EndAllAlert();

                if(canHurtPlayer()){
                    PlayerHealth.Instance.TakeDamage(bossMeleeHarm);
                }
                if(attackCounter%2==1){
                    yield return new WaitForSeconds(bossAttackPoint);
                    CallOrcTrack();
                }
                break;
            case "Rebo":
                yield return new WaitForSeconds(bossAttackPoint);
                bossAnimator.SetTrigger("attack");
                
                AlertController.Instance.EndAllAlert();

                if(canHurtPlayer()){
                    PlayerHealth.Instance.TakeDamage(bossMeleeHarm);
                }
                break;
            default:
                break;
        }
    }

    void CallRangeAttack(){
        switch(name)
        {
            case "Rebo":
                newItem = Instantiate(missile, new Vector3(bossX, bossYArr[attackingLine], 0), Quaternion.identity);
                break;
            default:
                break;
        }
    }

    IEnumerator CallRetreat(){
        yield return new WaitForSeconds(1f);

        attackingLine = playerMovement.GetYPos();
        moveDestY = bossYArr[attackingLine];
        moveDest = new Vector3(bossX, moveDestY, 0);

        SetLocalScale();

        moving = true;
        meleeAttackWaiting = false;
        retreatWaiting = false;

        bossAnimator.SetBool("isMove", true);
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

    bool canHurtPlayer()
    {   
        float distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
        Debug.Log(distance);
        return distance < 1.2f;
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
    void CallBigRedTrack()
    {
        attackingLine = playerMovement.GetYPos();
        moveDestY = bossYArr[attackingLine];
        moveDest = new Vector3(playerX, moveDestY, 0);

        SetLocalScale();

        moving = true;
        meleeAttackWaiting = true;
        retreatWaiting = true;

        bossAnimator.SetBool("isMove", true);

        AlertController.Instance.StartAlert(attackingLine);
    }

    /***********************************
    **          Boss: Orc          **
    ************************************/
    void CallOrcTrack()
    {
        attackingLine = playerMovement.GetYPos();
        moveDestY = bossYArr[attackingLine];
        moveDest = new Vector3(playerX, moveDestY, 0);

        SetLocalScale();

        moving = true;
        meleeAttackWaiting = true;
        if(attackCounter%2==0){
            retreatWaiting = false;
        }else{
            retreatWaiting = true;
        }
        attackCounter += 1;

        bossAnimator.SetBool("isMove", true);

        AlertController.Instance.StartAlert(attackingLine);
    }

    /***********************************
    **          Boss: Rebo          **
    ************************************/
    void CallReboTrack()
    {
        attackingLine = playerMovement.GetYPos();
        moveDestY = bossYArr[attackingLine];

        SetLocalScale();

        moving = true;
        if(attackCounter%1==0)
        {
            meleeAttackWaiting = true;
            rangeAttackWaiting = false;
            retreatWaiting = true;
            moveDest = new Vector3(playerX, moveDestY, 0);
        }
        else
        {
            meleeAttackWaiting = false;
            rangeAttackWaiting = true;
            retreatWaiting = true;
            moveDest = new Vector3(bossX, moveDestY, 0);
        }
        attackCounter += 1;

        bossAnimator.SetBool("isMove", true);

        AlertController.Instance.StartAlert(attackingLine);
    }
    
    // // Line Index from top to bottom: 0, 1, 2, 3
    // public IEnumerator AutoAttack()
    // {

    //     while (true)
    //     {
    //         yield return new WaitForSeconds(bossAttackPeriod);

    //         attackingLine = playerMovement.GetYPos();
    //         moveDestY = bossYArr[attackingLine];

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
            
    //         if (startStruggle == false && BgmController.instance.songPosition > struggleStartTime)
    //         {
    //             Struggle();
    //             _bossUI.StruggleColor();
    //             startStruggle = true;
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
    
    // IEnumerator SpawnAttack()
    // {
    //     float pos1 = LineIndToPos(attackingLine),
    //         pos2 = LineIndToPos((attackingLine + 3) % 4),
    //         pos3 = LineIndToPos((attackingLine + 1) % 4);
    //     switch (attackCounter)
    //     {
    //         case 0:
    //             StartAlert(attackingLine);
    //             yield return new WaitForSeconds(1.2f);
    //             newItem = Instantiate(laser, new Vector3(-4, pos1, 0), Quaternion.identity);
    //             Destroy(newItem, 1f);
    //             EndAlert(attackingLine);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 3) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             newItem = Instantiate(missile, new Vector3(6, pos2, 0), Quaternion.identity);
    //             Destroy(newItem, 2f);
    //             EndAlert((attackingLine + 3) % 4);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 1) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             newItem = Instantiate(missile, new Vector3(6, pos3, 0), Quaternion.identity);
    //             Destroy(newItem, 2f);
    //             EndAlert((attackingLine + 1) % 4);
    //             break;
    //         default:
    //             StartAlert(attackingLine);
    //             yield return new WaitForSeconds(1.2f);
    //             newItem = Instantiate(missile, new Vector3(6, pos1, 0), Quaternion.identity);
    //             Destroy(newItem, 2f);
    //             EndAlert(attackingLine);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 3) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             newItem = Instantiate(missile, new Vector3(6, pos2, 0), Quaternion.identity);
    //             Destroy(newItem, 2f);
    //             EndAlert((attackingLine + 3) % 4);
                
    //             yield return new WaitForSeconds(0.3f);
                
    //             StartAlert((attackingLine + 1) % 4);
    //             yield return new WaitForSeconds(1.2f);
    //             newItem = Instantiate(missile, new Vector3(6, pos3, 0), Quaternion.identity);
    //             Destroy(newItem, 2f);
    //             EndAlert((attackingLine + 1) % 4);
    //             break;
    //             ;
    //     }
    //     attackCounter = (attackCounter + 1)%3;
    // }

    public void Struggle()
    {
        bossAttackPeriod *= 0.9f;
        laserHarm = (int)(laserHarm*1.5f);
        missileHarm = (int)(missileHarm*1.5f);
        BossUI.Instance.originalColor = Color.black;
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