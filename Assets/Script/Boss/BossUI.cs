using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private SpriteRenderer bossBodyRenderer;

    private Animator bossAnimator;

    // Damage text
    public GameObject prefab;
    public Vector3 offset = new Vector3(0, 10, 0);

    public GameObject bloodEffect;

    private bool isFrozen = false;
    
    
    void Start()
    {
        instance = this;
        
        healthBar.SetMaxValue(bossHealth);
        
        bossBodyRenderer = bossBody.GetComponent<SpriteRenderer>();

        bossAnimator = GetComponent<Animator>();

        originalColor = Color.white;
        SetColor(originalColor);
    }

    public void SetColor(Color nextColor)
    {
        bossBodyRenderer.color = Color.Lerp(bossBodyRenderer.color, nextColor, 1);
    }
    
    public void TakeDamage(int damage)
    {
        // Instantiate(bloodEffect, bossBody.transform.position, Quaternion.identity);
        Debug.Log("take damage");
        bossAnimator.SetTrigger("damage");
        
        bossHealth -= damage;
        healthBar.SetValue(bossHealth);

        // add damage text
        GameObject temp = GameObject.Instantiate(prefab);
        temp.transform.parent = GameObject.Find("Canvas").transform;
        temp.transform.position = Camera.main.WorldToScreenPoint(transform.position) + offset;
        temp.GetComponent<Text>().text = "-" + damage.ToString() ; 

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
        SetColor(new Color(253f/255f, 150f/255f, 9f/255f));
    }

    void ResetColor()
    {
        SetColor(originalColor);
    }
}
