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
    [SerializeField] private GameObject trackingMissile;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject arcaneBall;

    [SerializeField] private GameObject fireBg;
    
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
    private float[] bossYArr = {3.77f, 1.81f, -1.55f, -3.53f};
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
                bossMoveSpeed = 0.1f;
                bossAttackPeriod = 8f;
                bossMeleeHarm = 15;
                bossAttackPoint = 0.1f;
                break;
            case "Orc":
                bossMoveSpeed = 0.3f;
                bossAttackPeriod = 7f;
                bossMeleeHarm = 15;
                bossAttackPoint = 0.3f;
                break;
            case "Rebo":
                bossMoveSpeed = 0.3f;
                bossAttackPeriod = 7f;
                bossMeleeHarm = 15;
                bossAttackPoint = 0.0f;
                break;
            case "Tank":
                bossMoveSpeed = 0.3f;
                bossAttackPeriod = 6f;
                bossMeleeHarm = 0;
                bossAttackPoint = 0.0f;
                bossX = 7;
                break;
            case "Lil":
                bossMoveSpeed = 0.3f;
                bossAttackPeriod = 6f;
                bossMeleeHarm = 0;
                bossAttackPoint = 2f;
                bossX = 7;
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
                case "Tank":
                    CallTankTrack();
                    break;
                case "Lil":
                    CallLilTrack();
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

                if(CanHurtPlayer()){
                    PlayerHealth.Instance.TakeDamage(bossMeleeHarm);
                }
                break;
            case "Orc":
                yield return new WaitForSeconds(bossAttackPoint);
                bossAnimator.SetTrigger("attack");
                
                AlertController.Instance.EndAllAlert();

                if(CanHurtPlayer()){
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

                if(CanHurtPlayer()){
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
                Instantiate(trackingMissile, BossUI.Instance.transform.position, Quaternion.identity);
                break;
            case "Tank":
                int[] yArr;
                if(attackCounter%2==0)
                {
                    AlertController.Instance.StartAlert(attackingLine);
                    for(int i = 0; i < 5; i++)
                    {
                        Vector3 bossPos = BossUI.Instance.transform.position;
                        Vector3 spawnedPosition = new Vector3(bossPos.x + i, bossYArr[attackingLine], 0);
                        GameObject bulletX = Instantiate(bullet, spawnedPosition, Quaternion.identity);
                        Bullet blt = bulletX.GetComponent<Bullet>();
                        blt.SetMidTarget(new Vector3(-1, bossYArr[attackingLine], 0));

                        if(i == 4)
                        {
                            blt.SetAlertLine(attackingLine);
                        }
                    }
                }
                else
                {
                    yArr = new int[]{attackingLine, (attackingLine+1)%4, (attackingLine+3)%4};
                    for(int i = 0; i < 3; i++)
                    {
                        AlertController.Instance.StartAlert(yArr[i]);
                        
                        Vector3 spawnedPosition = new Vector3(bossX, bossYArr[attackingLine], 0);
                        GameObject bulletX = Instantiate(bullet, spawnedPosition, Quaternion.identity);
                        Bullet blt = bulletX.GetComponent<Bullet>();
                        blt.SetMidTarget(new Vector3(5, bossYArr[yArr[i]], 0));

                        blt.SetAlertLine(yArr[i]);
                    }
                }
                break;
            case "Lil":
                bossAnimator.SetTrigger("attack");
                if(attackCounter%2==0)
                {
                    StartCoroutine(LaserAttack());
                }
                else
                {
                    for(int i = 0; i < 4; i++)
                    {
                        AlertController.Instance.StartAlert(i);
                        
                        Vector3 spawnedPosition = new Vector3(bossX + (attackingLine + i)%4, bossYArr[i], 0);
                        GameObject arcaneBallX = Instantiate(arcaneBall, spawnedPosition, Quaternion.identity);
                        Bullet blt = arcaneBallX.GetComponent<Bullet>();
                        blt.SetMidTarget(new Vector3(5, bossYArr[i], 0));

                        blt.SetAlertLine(i);
                    }
                }
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

    public void Struggle()
    {
        bossAttackPeriod *= 0.7f;
        bossMoveSpeed *= 2f;
        fireBg.SetActive(true);
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
        yield return new WaitForSeconds(4f);
        bossMoveSpeed *= 10;
        bossAttackPeriod /=2;
        BossUI.Instance.ResetColor();
    }

    bool CanHurtPlayer()
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
        // Melee Attack
        if(attackCounter%2==0)
        {
            attackingLine = playerMovement.GetYPos();
            moveDestY = bossYArr[attackingLine];
            moveDest = new Vector3(playerX, moveDestY, 0);

            SetLocalScale();

            moving = true;
            meleeAttackWaiting = true;
            rangeAttackWaiting = false;
            retreatWaiting = true;
            AlertController.Instance.StartAlert(attackingLine);
        }
        else
        // Range Attack
        {
            moving = true;
            meleeAttackWaiting = false;
            rangeAttackWaiting = true;
            retreatWaiting = false;
        }
        attackCounter += 1;

        bossAnimator.SetBool("isMove", true);
    }
    /***********************************
    **          Boss: Tank          **
    ************************************/
    void CallTankTrack()
    {
        attackingLine = playerMovement.GetYPos();
        moveDestY = bossYArr[attackingLine];
        moveDest = new Vector3(bossX, moveDestY, 0);
        
        SetLocalScale();

        moving = true;
        meleeAttackWaiting = false;
        rangeAttackWaiting = true;
        retreatWaiting = false;

        attackCounter += 1;

        bossAnimator.SetBool("isMove", true);
    }
    /***********************************
    **          Boss: Lil          **
    ************************************/
    void CallLilTrack()
    {
        attackingLine = playerMovement.GetYPos();
        moveDestY = bossYArr[attackingLine];
        moveDest = new Vector3(bossX, moveDestY, 0);
        
        SetLocalScale();

        moving = true;
        meleeAttackWaiting = false;
        rangeAttackWaiting = true;
        retreatWaiting = false;

        attackCounter += 1;

        bossAnimator.SetBool("isMove", true);
    }

    IEnumerator LaserAttack()
    {
        if(attackingLine < 2)
        {
            AlertController.Instance.StartAlert(0);
            AlertController.Instance.StartAlert(1);
        }
        else{
            AlertController.Instance.StartAlert(2);
            AlertController.Instance.StartAlert(3);
        }
        yield return new WaitForSeconds(bossAttackPoint);
        GameObject laser1;
        GameObject laser2;
        if(attackingLine < 2)
        {
            laser1 = Instantiate(laser, new Vector3(-4, bossYArr[0], 0), Quaternion.identity);
            laser2 = Instantiate(laser, new Vector3(-4, bossYArr[1], 0), Quaternion.identity);
            laser1.GetComponent<Laser>().SetAlertLine(0);
            laser2.GetComponent<Laser>().SetAlertLine(1);
        }
        else{
            laser1 = Instantiate(laser, new Vector3(-4, bossYArr[2], 0), Quaternion.identity);
            laser2 = Instantiate(laser, new Vector3(-4, bossYArr[3], 0), Quaternion.identity);
            laser1.GetComponent<Laser>().SetAlertLine(2);
            laser2.GetComponent<Laser>().SetAlertLine(3);
        }
        Destroy(laser1, 3f);
        Destroy(laser2, 3f);
    }

}