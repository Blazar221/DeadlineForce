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
    [SerializeField] private Image upgradeItem;
    public GameObject targetLine;
    
    private Color blue = new(0.01568625f, 0.1250286f, 0.9921569f, 1.0f);
    private Color green = new(0.4305087f, 0.8207547f, 0.2516465f, 1.0f);
    private Color cyan = new(1.0f, 1.0f, 1.0f, 1.0f);
    private Color dark = new(1f,0.05368341f, 0f, 1f);
    // private Dictionary<string, bool[]> isVisible;

    private IDictionary<string, GameObject> lineDict;
    // private List<GameObject> gems;
    private IDictionary<string, List<Image>> gemDict;
    private IDictionary<string, float> timeDict;

    private int[] formulas = {1,0,2};
    private Target[] targets =
    {
        new(new[]{1}, new[]{10f}),   // for testing
        new(new[]{0,2,1,3}, new[]{15f, 25f, 20f, 30f}), 
        new(new[]{0,2}, new[]{25f, 15f}), 
        new( new[]{3,1}, new[]{20f, 25f})
    };

    // 0: blue*3 = knife
    // 1: green*3 = shield
    // 2: cyan*3 = mine
    // 3: dark*3 = sword

    
    private int targetIndex = 0;
    private int gemCounter = 0;

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
        else if (IsSameColor(color, cyan))
        {
            SetColor(color, "cyan");
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
                    item.sprite = items[0];
                    colorName = "blue";
                    // isVisible.Add("blue", new []{true, true, true});
                    break;
                case 1:
                    firstGem.color = green;
                    secondGem.color = green;
                    thirdGem.color = green;
                    item.sprite = items[1];
                    colorName = "green";
                    // isVisible.Add("green", new []{true, true, true});
                    break;
                case 2:
                    firstGem.color = cyan;
                    secondGem.color = cyan;
                    thirdGem.color = cyan;
                    item.sprite = items[2];
                    colorName = "cyan";
                    // isVisible.Add("cyan", new []{true, true, true});
                    break;
                case 3:
                    firstGem.color = dark;
                    secondGem.color = dark;
                    thirdGem.color = dark;
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
