using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private float gameTime = 0.0f;
    private float step = 0.0f;
    SpriteRenderer render;

    [SerializeField] public int bossHealth = 100;
    public float speed = 1f;
    public HealthBar healthBar;
    public GameObject deathEffect;
    public GameObject colorBody;
    public GameObject boss;


    void Start()
    {
        healthBar.SetMaxHealth(bossHealth);
        // Get the corresponding property of the gameObject
        render = colorBody.GetComponent<SpriteRenderer>();
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

        // Switch properties
        if (gameTime / 10.0f % 4 > 0 && gameTime / 10.0f % 4 <= 1)
        {
            FireState();
        } 
        else if (gameTime / 10.0f % 4 > 1 && gameTime / 10.0f % 4 <= 2)
        {
            WaterState();
        }
        else if (gameTime / 10.0f % 4 > 2 && gameTime / 10.0f % 4 <= 3)
        {
            GrassState();
        }
        else 
        {
            EarthState();
        }
    }

    void FireState() 
    {
        // Debug.Log("fire");
        // 0.1f is the smoothing factor
        render.color = Color.Lerp(render.color, Color.red, 0.1f * gameTime);
    }

    void WaterState()
    {
        // Debug.Log("water");
        render.color = Color.Lerp(render.color, Color.blue, 0.1f * gameTime);
    }

    void GrassState()
    {
        // Debug.Log("grass");
        render.color = Color.Lerp(render.color, Color.green, 0.1f * gameTime);
    }

    void EarthState()
    {
        // Debug.Log("earth");
        render.color = Color.Lerp(render.color, Color.yellow, 0.1f * gameTime);
    }

    void Appear()
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, new Vector3(9.34f, 0.63f, 0.0786f), step);
    }

    void Hide()
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, new Vector3(11.18f, 0.63f, 0.0786f), step);
    }

    public void TakeDamage(int damage)
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
