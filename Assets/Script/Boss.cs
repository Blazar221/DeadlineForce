using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private float gameTime = 0.0f;
    private float step = 0.0f;
    private int state = 0;
    SpriteRenderer render;

    [SerializeField] public int bossHealth = 100;
    public float speed = 1f;
    public HealthBar healthBar;
    public GameObject deathEffect;
    public GameObject colorBody;
    public GameObject boss;

    public enum ElemType {Fire, Water, Grass, Rock};
    public ElemType eleType;


    void Start()
    {
        healthBar.SetMaxHealth(bossHealth);
        // Get the corresponding property of the gameObject
        render = colorBody.GetComponent<SpriteRenderer>();
        SwitchState();
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        step = speed * gameTime;
        // Switch boss state(appear, hide)
        if (gameTime % 10.0f > 2 && gameTime % 10.0f <= 8)
        {
            Hide();
        } else
        {
            Appear();
        }
    }

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
                EarthState();
                break;
        }
    }

    void FireState() 
    {
        // Debug.Log("fire");
        // 0.1f is the smoothing factor
        render.color = Color.Lerp(render.color, Color.red, 1);
        boss.eleType = ElemType.Fire;
    }

    void WaterState()
    {
        // Debug.Log("water");
        render.color = Color.Lerp(render.color, Color.blue, 1);
        boss.eleType = ElemType.Water;
    }

    void GrassState()
    {
        // Debug.Log("grass");
        render.color = Color.Lerp(render.color, Color.green, 1);
        boss.eleType = ElemType.Grass;
    }

    void EarthState()
    {
        // Debug.Log("earth");
        render.color = Color.Lerp(render.color, Color.yellow, 1);
        boss.eleType = ElemType.Rock;
    }

    void Appear()
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, new Vector3(9.34f, 0.63f, 0.0786f), step);
    }

    void Hide()
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, new Vector3(11.18f, 0.63f, 0.0786f), step);
    }

    public void TakeDamage(Item attackingItem)
    {
        bossHealth -= damage;
        healthBar.SetHealth(bossHealth);

        if (bossHealth <= 0)
        {
            Dead();
            GameController.Instance.EnableCongratsMenu();
        }
    }

    void Dead()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
