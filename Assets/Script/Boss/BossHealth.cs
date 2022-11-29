using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public static BossHealth Instance;

    [SerializeField] 
    public int bossHealth = 100;
    public int maxHealth;
    public SliderBar healthBar;

    bool struggle = false;

    private Animator bossAnimator;

    public static event Action OnBossDeath;
    
    void Awake()
    {
        Instance = this;

        maxHealth = bossHealth;
        healthBar.SetMaxValue(bossHealth);

        bossAnimator = GetComponent<Animator>();
    }

    
    public void TakeDamage(int damage)
    {
        bossAnimator.SetTrigger("damage");

        bossHealth -= damage;
        healthBar.SetValue(bossHealth);

        BossUI.Instance.CallDamageHint(damage);

        if (bossHealth <= 0)
        {
            bossHealth = 0;
            OnBossDeath?.Invoke();
        }
        if (!struggle && bossHealth < maxHealth * 0.35f)
        {
            struggle = true;
            BossBehavior.Instance.Struggle();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    public int GetBossHealth()
    {
        return bossHealth;
    }
    
}
