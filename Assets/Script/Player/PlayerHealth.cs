using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    // To call game over menu
    public static event Action OnPlayerDeath;

    private Animator animator;
    private bool isUpsideDown;

    [SerializeField]
    // Control health of player
    public int maxHealth = 100; 
    public int currentHealth;
    public SliderBar healthBar;

    //
    public bool hasShield = false;

    // Damage text
    public GameObject prefab;
    public Vector3 offset = new Vector3(0, 120, 0);

    void Awake()
    {
        Instance = this;
        
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        isUpsideDown = false;
        
        animator = GetComponent<Animator>();
    }

    public void EnableShield()
    {
        hasShield = true;
    }

    public void DisableShield()
    {
        hasShield = false;
    }

    public int GetPlayerHealth()
    {
        return currentHealth;
    }
    // TakeDamage Function
    public void TakeDamage(int damage, bool hasAnim = true)
    {
        if(!hasShield)
        {
            currentHealth -= damage;
            healthBar.SetValue(currentHealth);
            // add damage text
            GameObject temp = GameObject.Instantiate(prefab);
            temp.transform.parent = GameObject.Find("Canvas").transform;
            temp.transform.position = Camera.main.WorldToScreenPoint(transform.position) + offset;
            temp.GetComponent<Text>().text = "-" + damage.ToString() ; 
            // take damage anim
            if(hasAnim){
                animator.SetTrigger("takeDamage");
            }

            if(currentHealth <= 0) 
            {
                currentHealth = 0;
                Debug.Log("You are dead!");
                OnPlayerDeath?.Invoke();
            }
        }
    }
}