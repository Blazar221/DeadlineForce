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

    private bool isUpsideDown;

    [SerializeField]
    // Control health of player
    public int maxHealth = 100; 
    public int currentHealth;
    public SliderBar healthBar;

    //
    public bool hasShield = false;

    // Blood Effect
    public GameObject bloodEffectCeil;
    public GameObject bloodEffectFloor;
    // Damage text
    public GameObject prefab;
    public Vector3 offset = new Vector3(0, 120, 0);

    void Awake()
    {
        Instance = this;
        
        currentHealth = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        isUpsideDown = false;
        
    }

    public void EnableShield()
    {
        hasShield = true;
    }

    public void DisableShield()
    {
        hasShield = false;
    }

    // TakeDamage Function
    public void TakeDamage(int damage)
    {
        if(hasShield)
        {
            damage/=10;
        }
        currentHealth -= damage;
        healthBar.SetValue(currentHealth);
        // add damage text
        GameObject temp = GameObject.Instantiate(prefab);
        temp.transform.parent = GameObject.Find("Canvas").transform;
        temp.transform.position = Camera.main.WorldToScreenPoint(transform.position) + offset;
        temp.GetComponent<Text>().text = "-" + damage.ToString() ; 
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