using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] public int bossHealth = 100;
    public HealthBar healthBar;
    public GameObject deathEffect;

    void Start()
    {
        healthBar.SetMaxHealth(bossHealth);
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        healthBar.SetHealth(bossHealth);

        if (bossHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
