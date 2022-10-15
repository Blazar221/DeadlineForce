using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class TargetPanel : MonoBehaviour
{
    public static TargetPanel instance;
    [SerializeField] private Sprite[] items;
    // [SerializeField] private Image[] gems;
    [SerializeField] private Sprite[] sources;
    public GameObject targetLine;
    
    private Color blue = new(0.01568625f, 0.67f, 1f, 1.0f);
    private Color green = new(0.4305087f, 1f, 0.058f, 1.0f);
    private Color red = new(1.0f, 0.56f, 1.0f, 1.0f);
    private Color dark = new(0.89f,0f, 1f, 1f);
    // private Dictionary<string, bool[]> isVisible;

    private IDictionary<string, GameObject> lineDict;
    // private List<GameObject> gems;
    private IDictionary<string, List<Image>> gemDict;
    private IDictionary<string, float> timeDict;

    private int[] formulas = {1,0,2};
    private Target[] targets =
    {
        new(new[]{0}, new[]{3f}),   // for testing
        new(new[]{1}, new[]{3f}),   // for testing
        new(new[]{2}, new[]{3f}),   // for testing
        new(new[]{3}, new[]{3f}),   // for testing
        new(new[]{0,2,1,3}, new[]{15f, 25f, 20f, 30f}), 
        new(new[]{0,2}, new[]{25f, 15f}), 
        new( new[]{3,1}, new[]{20f, 25f})
    };

    // 0: blue*3 = waterWeapon
    // 1: green*3 = grassWeapon
    // 2: red*3 = fireWeapon
    // 3: dark*3 = darkWeapon

    
    private int targetIndex = 0;
    private int gemCounter = 0;
    
    //for inventory system
    private Inventory inventory;
    [SerializeField] private InventoryUI uiInventory;

    private void Awake()
    {
        instance = this;
        // targetLine = transform.Find("TargetLine").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // gems = new List<GameObject>();
        gemDict = new Dictionary<string, List<Image>>();
        lineDict = new Dictionary<string, GameObject>();
        timeDict = new Dictionary<string, float>();
        // isVisible = new Dictionary<string, bool[]>();
        SetNextTarget();
    }

    private void FixedUpdate()
    {
        var keys = new List<string>(lineDict.Keys);
        foreach (var key in keys)
        {
            if (timeDict[key] >= Time.fixedDeltaTime)
            {
                timeDict[key] -= Time.fixedDeltaTime;
            }
            else
            {
                timeDict.Remove(key);
                gemDict.Remove(key);
                var toDestroy = lineDict[key];
                lineDict.Remove(key);
                Destroy(toDestroy);
                // add function to return the gems to the inventory
                    
                // add function to return the gems to the inventory
                if (gemDict.Count == 0)
                {
                    SetNextTarget();
                }
            }
        }
    }

    private void SetColor(Color color ,string colorStr)
    {
        if(gemDict.ContainsKey(colorStr))
        {   
            // set the first one in the list to transparent and remove it from the list
            if(gemDict[colorStr].Count > 0)
            {
                gemDict[colorStr][0].color = new Color(color.r, color.g, color.b, 0.0f);
                gemDict[colorStr].RemoveAt(0);
                if(gemDict[colorStr].Count == 0)
                {
                    GameObject lineToDestroy = lineDict[colorStr];
                    gemDict.Remove(colorStr);
                    lineDict.Remove(colorStr);
                    timeDict.Remove(colorStr);
                    Destroy(lineToDestroy);
                    // add function to return the gems to the inventory
                    
                    // add function to return the gems to the inventory
                    if(gemDict.Count == 0)
                    {
                        SetNextTarget();
                    }
                }
            }
        }
    }

    public void TargetHit(Color color)
    {
        Debug.Log("Color parameter: " + color);

        if (IsSameColor(color, blue))
        {
            SetColor(color, "blue");
        }else if (IsSameColor(color, green))
        {
            SetColor(color, "green");
        }
        else if (IsSameColor(color, red))
        {
            SetColor(color, "red");
        }
        else if (IsSameColor(color, dark))
        {
            SetColor(color, "dark");
        }
        
    }

    private void SetNextTarget()
    {
        var lines = targets[targetIndex].GetFormulaIndex();
        var times = targets[targetIndex].GetTimeToCollect();
        for(var i = 0; i <  targets[targetIndex].TargetLength(); i++)
        {
            GameObject obj = Instantiate(targetLine, transform);
            var firstGem = obj.transform.Find("FirstItem").gameObject.GetComponent<Image>();
            var secondGem = obj.transform.Find("SecondItem").gameObject.GetComponent<Image>();
            var thirdGem = obj.transform.Find("ThirdItem").gameObject.GetComponent<Image>();
            var item = obj.transform.Find("UpgradeItem").gameObject.GetComponent<Image>();

            var colorName = "";
            
            switch (lines[i])
            {
                case 0:
                    firstGem.color = blue;
                    secondGem.color = blue;
                    thirdGem.color = blue;
                    firstGem.sprite = sources[0];
                    secondGem.sprite = sources[0];
                    thirdGem.sprite = sources[0];
                    item.sprite = items[0];
                    colorName = "blue";
                    // isVisible.Add("blue", new []{true, true, true});
                    break;
                case 1:
                    firstGem.color = green;
                    secondGem.color = green;
                    thirdGem.color = green;
                    firstGem.sprite = sources[1];
                    secondGem.sprite = sources[1];
                    thirdGem.sprite = sources[1];
                    item.sprite = items[1];
                    colorName = "green";
                    // isVisible.Add("green", new []{true, true, true});
                    break;
                case 2:
                    firstGem.color = red;
                    secondGem.color = red;
                    thirdGem.color = red;
                    firstGem.sprite = sources[2];
                    secondGem.sprite = sources[2];
                    thirdGem.sprite = sources[2];
                    item.sprite = items[2];
                    colorName = "red";
                    // isVisible.Add("cyan", new []{true, true, true});
                    break;
                case 3:
                    firstGem.color = dark;
                    secondGem.color = dark;
                    thirdGem.color = dark;
                    firstGem.sprite = sources[3];
                    secondGem.sprite = sources[3];
                    thirdGem.sprite = sources[3];
                    item.sprite = items[3];
                    colorName = "dark";
                    // isVisible.Add("dark", new []{true, true, true});
                    break;
                default:
                    break;
            }
            lineDict.Add(colorName, obj);
            gemDict.Add(colorName, new List<Image>{firstGem, secondGem, thirdGem});
            // set timer
            obj.GetComponent<TargetTimer>().timeLeft = times[i];
            timeDict.Add(colorName, times[i]);
        }
        
        targetIndex++;
        if(targetIndex == targets.Length)
            targetIndex = 0;
        
    }

    private static bool IsSameColor(Color color1, Color color2)
    {
        return (Math.Round(color1.r,3)).Equals(Math.Round(color2.r,3)) 
               && (Math.Round(color1.g,3)).Equals(Math.Round(color2.g,3))
               && (Math.Round(color1.b,3)).Equals(Math.Round(color2.b,3));
    }
}

public struct Target
{
    private int[] formulaIndex;
    private float[] timeToCollect;
    public Target(int[] formulaIndex, float[] timeToCollect)
    {
        this.formulaIndex = formulaIndex;
        this.timeToCollect = timeToCollect;
    }

    public int[] GetFormulaIndex()
    { 
        return formulaIndex;
    }
    
    public float[] GetTimeToCollect()
    {
        return timeToCollect;
    }
    
    public int TargetLength()
    {
        return formulaIndex.Length;
    }
}
