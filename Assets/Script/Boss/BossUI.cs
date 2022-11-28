using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public static BossUI Instance;

    private SpriteRenderer bossBodyRenderer;

    public Color originalColor;

    // Damage text
    public GameObject damageText;
    private Vector3 damageTextOffset = new Vector3(0, 10, 0);

    private bool isFrozen = false;
    
    void Awake()
    {
        Instance = this;
                
        bossBodyRenderer = GetComponent<SpriteRenderer>();

        originalColor = Color.white;
        SetColor(originalColor);
    }

    public void SetColor(Color nextColor)
    {
        bossBodyRenderer.color = Color.Lerp(bossBodyRenderer.color, nextColor, 1);
    }
    
    public void CallDamageHint(int damage)
    {
        // add damage text
        GameObject temp = GameObject.Instantiate(damageText);
        temp.transform.parent = GameObject.Find("Canvas").transform;
        temp.transform.position = Camera.main.WorldToScreenPoint(transform.position) + damageTextOffset;
        temp.GetComponent<Text>().text = "-" + damage.ToString(); 
    }
    
    // Effect of being attacked
    void FlashColor(float time)
    {
        SetColor(Color.red);
        Invoke("ResetColor", time);
    }

    public void StruggleColor()
    {
        SetColor(new Color(253f/255f, 150f/255f, 9f/255f));
    }

    public void ResetColor()
    {
        SetColor(originalColor);
    }
}
