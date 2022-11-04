using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    // To call game over menu
    public static event Action OnPlayerDeath;

    private Animator animator;
    private bool isUpsideDown;

    [SerializeField]
    // Control health of player
    public int maxHealth = 100; 
    public int currentHealth;
    public SliderBar healthBar;

    // Blood Effect
    public GameObject bloodEffectCeil;
    public GameObject bloodEffectFloor;

    void Awake()
    {
        instance = this;
        
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        isUpsideDown = false;
        
        animator = GetComponent<Animator>();
    }

    // TakeDamage Function
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetValue(currentHealth);
        // add blood effect
        if (isUpsideDown){
            Instantiate(bloodEffectCeil, transform.position + new Vector3(0, -0.2f, 0), bloodEffectCeil.transform.rotation);
        }
        else
        {
            Instantiate(bloodEffectFloor, transform.position + new Vector3(0,0.2f,0), bloodEffectFloor.transform.rotation);
        }
        if(currentHealth <= 0) 
        {
            currentHealth = 0;
            Debug.Log("You are dead!");
            OnPlayerDeath?.Invoke();
        }
    }
}