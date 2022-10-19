using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class TargetPanel : MonoBehaviour
{
    public static TargetPanel Instance;

    [SerializeField] private Sprite[] items;

    // [SerializeField] private Image[] gems;
    [SerializeField] private Sprite[] sources;
    public GameObject targetLine;

    private readonly Color _blue = new(0.016f, 0.67f, 1f, 1.0f);
    private readonly Color _green = new(0.43f, 1f, 0.058f, 1.0f);
    private readonly Color _red = new(1.0f, 0.5f, 0f, 1.0f);
    private readonly Color _brown = new(0.8f, 0.38f, 0f, 1f);

    private readonly Color _brownSaber = new(0.58f, 0.3f, 0f, 1f);

    private IDictionary<string, GameObject> _lineDict;
    private IDictionary<string, List<Image>> _gemDict;
    private IDictionary<string, float> _timeDict;

    private Target[] _targets ;
    private int _targetLoopIndex;

    private readonly Target[] _level1Target = {
        new(new[] { 0 }, new[] { 10f }),
        new(new[] { 2 }, new[] { 10f }),
        new(new[] { 0, 2 }, new[] { 5f, 10f }),
        new(new[] { 2, 0 }, new[] { 5f, 10f }),
        // loop
        new(new[] { 0, 2 }, new[] { 4f, 8f }),
        new(new[] { 0 }, new[] { 3f}),
        new(new[] { 2, 0 }, new[] { 4f, 8f }),
        new(new[] { 2 }, new[] { 3f }),
        new(new[] { 0 }, new[] { 4f, 8f }),
        new(new[] { 2, 0 }, new[] { 4f, 6f }),
        new(new[] { 2, 0 }, new[] { 3f, 6f }),
    };
    private const int _level1LoopIndex = 4;

    // 0: blue*3 = waterWeapon
    // 1: green*3 = grassWeapon
    // 2: red*3 = fireWeapon
    // 3: dark*3 = darkWeapon

    
    private int _targetIndex;
    
    //for inventory system
    public Inventory inventory;
    [SerializeField] private InventoryUI uiInventory;

    private void Awake()
    {
        Instance = this;
        var scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Level1":
                _targets = _level1Target;
                _targetLoopIndex = _level1LoopIndex;
                break;
            case "Level2":
                break;
            case "Level3":
                break;
        }
        // targetLine = transform.Find("TargetLine").gameObject;
        
        //for inventory system
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }


    void Start()
    {        
        // gems = new List<GameObject>();
        _gemDict = new Dictionary<string, List<Image>>();
        _lineDict = new Dictionary<string, GameObject>();
        _timeDict = new Dictionary<string, float>();
        // isVisible = new Dictionary<string, bool[]>();
        SetNextTarget();
    }

    private void FixedUpdate()
    {
        var keys = new List<string>(_lineDict.Keys);
        foreach (var key in keys)
        {
            if (_timeDict[key] >= Time.fixedDeltaTime)
            {
                _timeDict[key] -= Time.fixedDeltaTime;
            }
            else
            {
                _timeDict.Remove(key);
                _gemDict.Remove(key);
                var toDestroy = _lineDict[key];
                _lineDict.Remove(key);
                Destroy(toDestroy);
                if (_gemDict.Count == 0)
                {
                    SetNextTarget();
                }
            }
        }
    }

    private void SetColor(Color color ,string colorStr)
    {
        if(_gemDict.ContainsKey(colorStr))
        {   
            // set the first one in the list to transparent and remove it from the list
            if(_gemDict[colorStr].Count > 0)
            {
                _gemDict[colorStr][0].color = new Color(color.r, color.g, color.b, 0.0f);
                _gemDict[colorStr].RemoveAt(0);
                if(_gemDict[colorStr].Count == 0)
                {
                    GameObject lineToDestroy = _lineDict[colorStr];
                    if (inventory.GetItemList().Count == 6) inventory.RemoveFirst();
                    inventory.AddSprite(_lineDict[colorStr].transform.Find("UpgradeItem").gameObject.GetComponent<Image>().sprite);
                    _gemDict.Remove(colorStr);
                    _lineDict.Remove(colorStr);
                    _timeDict.Remove(colorStr);
                    Destroy(lineToDestroy);
                    if(_gemDict.Count == 0)
                    {
                        SetNextTarget();
                    }
                }
            }
        }
    }

    public void TargetHit(Color color)
    {
        if (IsSameColor(color, _blue))
        {
            SetColor(color, "blue");
        }else if (IsSameColor(color, _green))
        {
            SetColor(color, "green");
        }
        else if (IsSameColor(color, _red))
        {
            SetColor(color, "red");
        }
        else if (IsSameColor(color, _brown))
        {
            SetColor(color, "brown");
        }
        
    }

    private void SetNextTarget()
    {
        var lines = _targets[_targetIndex].GetFormulaIndex();
        var times = _targets[_targetIndex].GetTimeToCollect();
        for(var i = 0; i <  _targets[_targetIndex].TargetLength(); i++)
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
                    firstGem.color = _blue;
                    secondGem.color = _blue;
                    thirdGem.color = _blue;
                    firstGem.sprite = sources[0];
                    secondGem.sprite = sources[0];
                    thirdGem.sprite = sources[0];
                    item.sprite = items[0];
                    colorName = "blue";
                    break;
                case 1:
                    firstGem.color = _green;
                    secondGem.color = _green;
                    thirdGem.color = _green;
                    firstGem.sprite = sources[1];
                    secondGem.sprite = sources[1];
                    thirdGem.sprite = sources[1];
                    item.sprite = items[1];
                    colorName = "green";
                    break;
                case 2:
                    firstGem.color = _red;
                    secondGem.color = _red;
                    thirdGem.color = _red;
                    firstGem.sprite = sources[2];
                    secondGem.sprite = sources[2];
                    thirdGem.sprite = sources[2];
                    item.sprite = items[2];
                    colorName = "red";
                    break;
                case 3:
                    firstGem.color = _brown;
                    secondGem.color = _brown;
                    thirdGem.color = _brown;
                    firstGem.sprite = sources[3];
                    secondGem.sprite = sources[3];
                    thirdGem.sprite = sources[3];
                    item.sprite = items[3];
                    item.color = _brownSaber;
                    colorName = "brown";
                    break;
            }
            _lineDict.Add(colorName, obj);
            _gemDict.Add(colorName, new List<Image>{firstGem, secondGem, thirdGem});
            // set timer
            obj.GetComponent<TargetTimer>().timeLeft = times[i];
            _timeDict.Add(colorName, times[i]);
        }
        
        _targetIndex++;
        if(_targetIndex == _targets.Length)
            _targetIndex = _targetLoopIndex;
        
    }

    private static bool IsSameColor(Color color1, Color color2)
    {
        return (Math.Round(color1.r,3)).Equals(Math.Round(color2.r,3)) 
               && (Math.Round(color1.g,3)).Equals(Math.Round(color2.g,3))
               && (Math.Round(color1.b,3)).Equals(Math.Round(color2.b,3));
    }
}

public readonly struct Target
{
    private readonly int[] _formulaIndex;
    private readonly float[] _timeToCollect;
    public Target(int[] formulaIndex, float[] timeToCollect)
    {
        this._formulaIndex = formulaIndex;
        this._timeToCollect = timeToCollect;
    }

    public int[] GetFormulaIndex()
    { 
        return _formulaIndex;
    }
    
    public float[] GetTimeToCollect()
    {
        return _timeToCollect;
    }
    
    public int TargetLength()
    {
        return _formulaIndex.Length;
    }
}
