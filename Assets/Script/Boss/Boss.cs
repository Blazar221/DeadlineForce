using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private float gameTime = 0.0f;
    private float step = 0.0f;
    private int state = 0;
    private float _fireRate;
    private float _canFire = 2f;
    public int[] stateCount = {0,0,0};
    private SpriteRenderer render;
    private Color originalColor;

    [SerializeField] public int bossHealth = 100;
    [SerializeField] private GameObject laser;
    [SerializeField] private int lineNum;
    //public Animator anim;
    public float speed = 1f;
    public float flashTime = 0.3f;
    public HealthBar healthBar;
    public GameObject deathEffect;
    public GameObject colorBody;
    public GameObject boss;
    public static Boss instance;

    public bool isHide;

    public enum ElemType {Fire, Water, Grass, Rock};
    public ElemType eleType;


    void Start()
    {
        instance = this;
        healthBar.SetMaxHealth(bossHealth);
        // Get the corresponding property of the gameObject
        render = colorBody.GetComponent<SpriteRenderer>();
        originalColor = render.color;
        //anim = GetComponent<Animator>();
        SwitchState();
        SwitchState();
        StartCoroutine(SpawnLaser());
        isHide = true;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        step = speed * gameTime;
        if(isHide){
            Hide();
        }else{
            Appear();
        }
    }

    private IEnumerator SpawnLaser()
    {
        var posHandle = 1;
        float yPos;
        while (true)
        {
            yield return new WaitForSeconds(5f);
            //anim.Play("LaserPrep");
            Debug.Log(posHandle%lineNum);
            if (lineNum == 4)
            {
                yPos = posHandle switch
                {
                    0 => 4,
                    1 => 1,
                    2 => -1,
                    3 => -4,
                    _ => 0,
                };
            }
            else
            {
                yPos = posHandle switch
                {
                    0 => 1,
                    1 => -1,
                    _ => 0,
                };
            }
            Debug.Log("yPos:"+yPos);
            var newItem = Instantiate(laser, new Vector3(-1, yPos, 0), Quaternion.identity);
            Destroy(newItem, 3f);
            posHandle = ++posHandle%lineNum;
        }
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

    public void Appear()
    {
        boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(9.34f, 0.63f, 0.0786f), .02f);
    }

    public void Hide()
    {
        boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(11.18f, 0.63f, 0.0786f), .02f);
    }

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
