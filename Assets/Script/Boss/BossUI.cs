using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public static BossUI Instance;

    public GameObject bossBody;
    private SpriteRenderer bossBodyRenderer;

    public Color originalColor;
    
    public float flashTime = 0.3f;

    // Damage text
    public GameObject prefab;
    public Vector3 offset = new Vector3(0, 10, 0);

    public GameObject bloodEffect;

    private bool isFrozen = false;
    
    void Awake()
    {
        Instance = this;
                
        bossBodyRenderer = bossBody.GetComponent<SpriteRenderer>();

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
        GameObject temp = GameObject.Instantiate(prefab);
        temp.transform.parent = GameObject.Find("Canvas").transform;
        temp.transform.position = Camera.main.WorldToScreenPoint(transform.position) + offset;
        temp.GetComponent<Text>().text = "-" + damage.ToString() ; 
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

    void ResetColor()
    {
        SetColor(originalColor);
    }
}
