using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class TargetPanel : MonoBehaviour
{
    public static TargetPanel instance;
    [SerializeField] private Sprite[] items;
    // [SerializeField] private Image firstGem;
    // [SerializeField] private Image secondGem;
    // [SerializeField] private Image thirdGem;
    [SerializeField] private Image[] gems;
    [SerializeField] private Image upgradeItem;
    
    private Color blue = new Color(0.01568625f, 0.1250286f, 0.9921569f, 1.0f);
    private Color green = new Color(0.4305087f, 0.8207547f, 0.2516465f, 1.0f);
    private Color cyan = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private bool[] isVisible = new bool[3];

    private int[] formulas = {1,0,2};

    // 0: blue*3 = knife
    // 1: green*3 = shield
    // 2: cyan*3 = mine
    // 3: cyan*2 + green*1 = sword

    
    private int itemIndex = 0;
    private int gemCounter = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        setGemsAndItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void targetHit(Color color)
    {
        Debug.Log("Color parameter: " + color);
        for (var i = 0; i < 3; i++)
        {
            if (!isVisible[i] || !isSameColor(color, gems[i].color)) continue;
            // player get the same gem; set this gem to invisible
            gems[i].color = new Color(color.r, color.g, color.b, 0.0f);
            isVisible[i] = false;
            gemCounter++;
            break;
        }
        if(gemCounter == 3)
        {
            // player get all gems; set the item to visible
            // set next round item
            setGemsAndItem();
            gemCounter = 0;
        }
    }

    private void setGemsAndItem()
    {
        switch (formulas[itemIndex])
        {
            case 0:
            // 0: blue*3 = knife
                // firstGem.GetComponent<Image>().color = blue;
                // secondGem.GetComponent<Image>().color = blue;
                // thirdGem.GetComponent<Image>().color = blue;
                gems[0].color = blue;
                gems[1].color = blue;
                gems[2].color = blue;
                upgradeItem.sprite = items[0];
                break;
            case 1:
                // 1: green*3 = shield
                gems[0].color= green;
                gems[1].color= green;
                gems[2].color= green;
                upgradeItem.sprite = items[1];
                break;
            case 2:
                // 2: cyan*3 = mine
                gems[0].color= cyan;
                gems[1].color= cyan;
                gems[2].color= cyan;
                upgradeItem.sprite = items[2];
                break;
            case 3:
                // 3: cyan*2 + green*1 = sword
                gems[0].color= cyan;
                gems[1].color= cyan;
                gems[2].color= green;
                upgradeItem.sprite = items[3];
                break;
            default:
                break;
        }
        for(var i = 0; i < 3; i++)
        {
            isVisible[i] = true;
        }
        itemIndex++;
        // let target to loop in the current item order
        if (itemIndex == items.Length)
            itemIndex = 0;
    }
    
    private bool isSameColor(Color color1, Color color2)
    {
        return (Math.Round(color1.r,3)).Equals(Math.Round(color2.r,3)) 
               && (Math.Round(color1.g,3)).Equals(Math.Round(color2.g,3))
               && (Math.Round(color1.b,3)).Equals(Math.Round(color2.b,3));
    }
}
