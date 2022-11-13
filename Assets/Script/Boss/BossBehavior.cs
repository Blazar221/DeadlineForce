using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public static BossBehavior instance;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private float bossMoveSpeed = 0.3f;
    [SerializeField] private float bossAttackPeriod = 5f;

    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject bandit;
    public int laserHarm=20, banditHarm=10;
    
    private int _attackingLine;
    private int _count=0;
    private float _moveDestY;
    public Vector3 moveDest;
    public bool startMove = false;

    private Vector3 originalLocalScale;

    [SerializeField] private Animator bossAnimator;
    private BossUI _bossUI;
    
    private GameObject _newBullet;
    
    [SerializeField] private GameObject bgm;
    [SerializeField] private float struggleStartTime;
    private BgmController _bgmHandler;
    private bool _startStruggle = false;

    void Awake()
    {   
        instance = this;
        bossAnimator = GetComponent<Animator>();
        _bossUI = GetComponent<BossUI>();
        _bgmHandler = bgm.GetComponent<BgmController>();

        originalLocalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Start()
    {
        StartCoroutine(AutoAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if(startMove)
        {
            CheckMoveEnd();
        }
    }
    
    // Line Index from top to bottom: 0, 1, 2, 3
    public IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossAttackPeriod);

            _attackingLine = playerMovement.GetYPos();
            _moveDestY = _attackingLine switch
            {
                0 => 4.92f,
                1 => 0.57f,
                2 => -0.57f,
                3 => -4.92f,
                _ => 0,
            };

            if(_attackingLine == 0 || _attackingLine == 2)
            {
                transform.localScale = new Vector3(originalLocalScale.x, -originalLocalScale.y, originalLocalScale.z);
            }
            else
            {
                transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y, originalLocalScale.z);
            }

            moveDest = new Vector3(transform.position.x, _moveDestY, transform.position.z);
            startMove = true;
            bossAnimator.SetBool("isMove", true);
            
            if (_startStruggle == false && _bgmHandler.songPosition > struggleStartTime)
            {
                Struggle();
                _bossUI.struggleColor();
                _startStruggle = true;
            }
        }
    }
    
    public void CheckMoveEnd()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDest, bossMoveSpeed);
        if(transform.position.y == _moveDestY){
            startMove = false;
            bossAnimator.SetBool("isMove", false);
            bossAnimator.SetTrigger("attack");
        }
    }

    public void Freeze()
    {
        bossMoveSpeed /= 10;
        bossAttackPeriod *= 2;
        BossUI.instance.SetColor(Color.blue);
        Debug.Log("Boss Freeze");
        StartCoroutine(Unfreeze());
    }

    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(8f);
        bossMoveSpeed *= 10;
        bossAttackPeriod /=2;
        BossUI.instance.SetColor(Color.white);
    }
    
    public void Attack()
    {
        StartCoroutine(SpawnAttack());
    }

    IEnumerator SpawnAttack()
    {
        float pos1 = LineIndToPos(_attackingLine),
            pos2 = LineIndToPos((_attackingLine + 3) % 4),
            pos3 = LineIndToPos((_attackingLine + 1) % 4);
        switch (_count)
        {
            case 0:
                _newBullet = Instantiate(laser, new Vector3(-2, pos1, 0), Quaternion.identity);
                Destroy(_newBullet, 1f);
                yield return new WaitForSeconds(0.3f);
                _newBullet = Instantiate(bandit, new Vector3(6, pos2, 0), Quaternion.identity);
                Destroy(_newBullet, 2f);
                yield return new WaitForSeconds(0.6f);
                _newBullet = Instantiate(bandit, new Vector3(6, pos3, 0), Quaternion.identity);
                Destroy(_newBullet, 2f);
                break;
            default:
                _newBullet = Instantiate(bandit, new Vector3(6, pos1, 0), Quaternion.identity);
                Destroy(_newBullet, 2f);
                yield return new WaitForSeconds(0.5f);
                _newBullet = Instantiate(bandit, new Vector3(6, pos2, 0), Quaternion.identity);
                Destroy(_newBullet, 2f);
                yield return new WaitForSeconds(0.5f);
                _newBullet = Instantiate(bandit, new Vector3(6, pos3, 0), Quaternion.identity);
                Destroy(_newBullet, 2f);
                break;
                ;
        }
        _count = (_count + 1)%3;
    }

    public void Struggle()
    {
        bossAttackPeriod *= 0.8f;
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
