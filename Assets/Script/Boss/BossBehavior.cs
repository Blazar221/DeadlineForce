using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] public PlayerControl player;
    [SerializeField] private float bossMoveSpeed = 0.3f;
    [SerializeField] private float bossAttackPeriod = 5f;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject bandit;
    public int laserHarm=20, banditHarm=10;
    
    private int _attackingLine;
    private int _count=0;
    private float _moveDestY;
    public Vector3 moveDest;
    public bool startMove;
    [SerializeField] private Animator bossAnimator;
    

    private GameObject _newBullet;
    // Start is called before the first frame update
    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        startMove = false;
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

            _attackingLine = player.curYPos;
            _moveDestY = _attackingLine switch
            {
                0 => 7,
                1 => 4,
                2 => 2,
                3 => -1,
                _ => 0,
            };

            moveDest = new Vector3(9.34f, _moveDestY, 0);
            startMove = true;
            bossAnimator.SetBool("isMove", true);
        }
    }
    
    public void CheckMoveEnd()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveDest, bossMoveSpeed);
        if(transform.position.y == _moveDestY){
            startMove = false;
            bossAnimator.SetBool("isMove", false);
            bossAnimator.SetTrigger("Attack");
        }
    }
    
    public void Attack()
    {
        float yPos = _attackingLine switch
        {
            0 => 4,
            1 => 1,
            2 => -1,
            3 => -4,
            _ => 0,
        };
        switch (_count)
        {
            case 0:
                _newBullet = Instantiate(laser, new Vector3(-2, yPos, 0), Quaternion.identity);
                Destroy(_newBullet, 1f);
                break;
            default:
                _newBullet = Instantiate(bandit, new Vector3(3, yPos, 0), Quaternion.identity);
                Destroy(_newBullet, 2f);
                break;
                ;
        }
        _count = (_count + 1)%3;
    }

    public void Struggle()
    {
        bossAttackPeriod *= 0.6f;
        laserHarm = (int)(laserHarm*1.5f);
        banditHarm = (int)(banditHarm*1.5f);
    }
}
