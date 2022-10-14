using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] public int bossHealth = 100;
    public HealthBar healthBar;
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
    }
}
