using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossUI : MonoBehaviour
{
    public static BossUI instance;

    public static event Action OnBossDeath;
    public Color originalColor;

    [SerializeField] public int bossHealth = 100;
    
    public float flashTime = 0.3f;
    public SliderBar healthBar;
    public GameObject deathEffect;

    public GameObject bossBody;
    public GameObject bossHead;
    public GameObject bossLeftLeg;
    public GameObject bossRightLeg;
    List<SpriteRenderer> bossRenderers;

    public GameObject bloodEffect;

    private bool isFrozen = false;
    
    
    void Start()
    {
        instance = this;
        
        healthBar.SetMaxValue(bossHealth);
        // Get the corresponding property of the gameObject
        bossRenderers = new List<SpriteRenderer>();
        bossRenderers.Add(bossBody.GetComponent<SpriteRenderer>());
        bossRenderers.Add(bossHead.GetComponent<SpriteRenderer>());
        bossRenderers.Add(bossLeftLeg.GetComponent<SpriteRenderer>());
        bossRenderers.Add(bossRightLeg.GetComponent<SpriteRenderer>());

        originalColor = Color.white;
        SetColor(originalColor);
    }

    public void SetColor(Color nextColor)
    {
        // Debug.Log("nextColor");
        // Debug.Log(nextColor);
        foreach(SpriteRenderer renderer in bossRenderers)
        {
            renderer.color = Color.Lerp(renderer.color, nextColor, 1);
        }
    }
    
    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, bossHead.transform.position, Quaternion.identity);

        FlashColor(flashTime);

        bossHealth -= damage;
        healthBar.SetValue(bossHealth);

        if (bossHealth <= 0)
        {
            bossHealth = 0;
            OnBossDeath?.Invoke();
        }
    }

    public int GetBossHealth()
    {
        return bossHealth;
    }
    
    void Dead()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Effect of being attacked
    void FlashColor(float time)
    {
        SetColor(Color.red);
        Invoke("ResetColor", time);
    }

    public void struggleColor()
    {
        SetColor(Color.black);
    }

    void ResetColor()
    {
        SetColor(originalColor);
    }
}
