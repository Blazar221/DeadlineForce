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

    // colors constants
    private readonly Color _blue = new(0.016f, 0.67f, 1f, 1.0f);
    private readonly Color _green = new(0.43f, 1f, 0.058f, 1.0f);
    private readonly Color _red = new(1.0f, 0.5f, 0f, 1.0f);
    private readonly Color _yellow = new(1f, 0.76f, 0f, 1f);
    private readonly Color _brownSaber = new(0.58f, 0.3f, 0f, 1f);
    
    
    private List<ObjectLine> _objectLines;
    private Target[] _targets ;
    private int _targetLoopIndex;
    
    // for statistics
    private int _targetCounter;

    private readonly Target[] _level1Target = {

        new(new[] { 0, 1 }, new[] { 6f, 10f }),
        new(new[] { 1, 0 }, new[] { 6f, 10f }),
        // loop
        new(new[] { 0, 1 }, new[] { 5.5f, 9f }),
        new(new[] { 1, 0 }, new[] { 5.5f, 9f }),
    };
    private const int Level1LoopIndex = 2;

    private readonly Target[] _level2Target = {
        new(new[] { 0, 3}, new[] { 15f, 20f }),
        new(new[] { 1, 2}, new[] { 15f, 20f }),
        new(new[] { 0, 1, 3}, new[] { 15f, 20f, 25f }),
        new(new[] { 1, 3}, new[] { 15f, 20f }),
        new(new[] { 0, 2}, new[] { 15f, 20f }),
        new(new[] { 1, 2, 3}, new[] { 15f, 20f, 25f }),
    };

    private const int Level2LoopIndex = 0;
    
    private readonly Target[] _level3Target = {
        
        // new(new[] { 0, 1,2,3,4 }, new[] { 5f,5f,5f,5f,5f}), // for test
        // new(new[] { 5,6,7,8,9 }, new[] { 5f,5f,5f,5f,5f }), // for test
        new(new[] { 9, 0 }, new[] { 15f, 20f }),
        new(new[] { 5, 3, 8 }, new[] { 15f, 20f, 25f }),
        new(new[] { 6, 1 }, new[] { 15f, 20f }),
        new(new[] { 8, 4, 9}, new[] { 15f, 20f, 25f }),
        new(new[] { 7, 2}, new[] { 15f, 20f }),
        new(new[] { 6, 1, 8}, new[] { 15f, 20f, 25f }),
        new(new[] { 5, 3 }, new[] { 15f, 20f }),
        new(new[] { 9, 0, 5}, new[] { 15f, 20f, 25f }),
        new(new[] { 8, 4 }, new[] { 15f, 20f }),
    };
    private const int Level3LoopIndex = 0;

    // 0: blue*3 = waterWeapon
    // 1: green*3 = grassWeapon
    // 2: red*3 = fireWeapon
    // 3: yellow*3 = yellowWeapon
    // 4: red*2 + blue*1 = RB
    // 5: red*2 + green*1 = RG
    // 6: red*2 + yellow*1 = RY
    // 7: blue*2 + green*1 = BG
    // 8: blue*2 + yellow*1 = BY
    // 9: green*2 + yellow*1 = GY
    
    private int _targetIndex;
    
    //for inventory system
    // public Inventory inventory;
    // [SerializeField] private InventoryUI uiInventory;

    private void Awake()
    {
        Instance = this;
        var scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Level1":
                _targets = _level1Target;
                _targetLoopIndex = Level1LoopIndex;
                break;
            case "Level2":
                _targets = _level2Target;
                _targetLoopIndex = Level2LoopIndex;
                break;
            case "Level3":
                _targets = _level3Target;
                _targetLoopIndex = Level3LoopIndex;
                break;
            case "Tutorial":
                _targets = _level1Target;
                _targetLoopIndex = Level1LoopIndex;
                break;
        }
        // targetLine = transform.Find("TargetLine").gameObject;
        
        //for inventory system
        // inventory = new Inventory();
        // uiInventory.SetInventory(inventory);
        _objectLines = new List<ObjectLine>();
        _targetCounter = 1;
    }


    void Start()
    {        

        SetNextTarget();
    }

    private void FixedUpdate()
    {
        for(var i = 0; i < _objectLines.Count; i++)
        {
            var objectLine = _objectLines[i];
            if (objectLine.UpdateTime(Time.fixedDeltaTime))
            {
                // the time is up
                UpdateAnalytics(objectLine);
                var toDestroy = objectLine.GetGameObj();
                Destroy(toDestroy);
                _objectLines.RemoveAt(i);
                if(_objectLines.Count == 0)
                {
                    SetNextTarget();
                }
            }
        }

    }

    private void SetColor(Color color)
    {
        for (var i = 0; i < _objectLines.Count; i++)
        {
            var objL = _objectLines[i];
            if (IsSameColor(color, objL.GetFirstColor()))
            {
                if (objL.RemoveFirstGem())
                {
                    // the line is completed
                    Boss.instance.TakeDamage(20);
                    UpdateAnalytics(objL);
                    var toDestroy = objL.GetGameObj();
                    Destroy(toDestroy);
                    // if (inventory.GetItemList().Count == 5) inventory.RemoveFirst();
                    // inventory.AddSprite(objL.GetUpgradeItem().sprite);
                    _objectLines.RemoveAt(i);
                }
                break;
            }
        }
        // check if there is no more elements in objectLines
        if (_objectLines.Count == 0)
        {
            SetNextTarget();
        }
    }

    private void UpdateAnalytics(ObjectLine objLine){
        if (Application.isEditor)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    Level1Editor.instance.UpdateQuest(objLine.GetIndex().ToString(), 
                        objLine.GetDescription(), objLine.GetCompleted());
                    break;
                case "Level2":
                    Level2Editor.instance.UpdateQuest(objLine.GetIndex().ToString(), 
                        objLine.GetDescription(), objLine.GetCompleted());
                    break;
                case "Level3":
                    Level3Editor.instance.UpdateQuest(objLine.GetIndex().ToString(), 
                        objLine.GetDescription(), objLine.GetCompleted());
                    break;
            }
        }
        else
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    Level1Web.instance.UpdateQuest(objLine.GetIndex().ToString(), 
                        objLine.GetDescription(), objLine.GetCompleted());
                    break;
                case "Level2":
                    Level2Web.instance.UpdateQuest(objLine.GetIndex().ToString(), 
                        objLine.GetDescription(), objLine.GetCompleted());
                    break;
                case "Level3":
                    Level3Web.instance.UpdateQuest(objLine.GetIndex().ToString(), 
                        objLine.GetDescription(), objLine.GetCompleted());
                    break;
            }
        }
    }

    public void TargetHit(Color color)
    {
        SetColor(color);
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

            RectTransform rectTransform = item.transform as RectTransform;
            var missionDescription = "";

            var redCount = 0;
            var blueCount = 0;
            var greenCount = 0;
            var brownCount = 0;
            
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
                    if (rectTransform != null)
                    {
                        rectTransform.Rotate(new Vector3(0, 0, 45));
                        rectTransform.localScale = new Vector3(0.5f, 1, 1);
                    }
                    blueCount = 3;
                    missionDescription = "blue blue blue";
                    break;
                case 1:
                    firstGem.color = _green;
                    secondGem.color = _green;
                    thirdGem.color = _green;
                    firstGem.sprite = sources[1];
                    secondGem.sprite = sources[1];
                    thirdGem.sprite = sources[1];
                    item.sprite = items[1];
                    if (rectTransform != null)
                    {
                        rectTransform.Rotate(new Vector3(0, 0, 45));
                        rectTransform.localScale = new Vector3(0.5f, 1, 1);
                    }
                    greenCount = 3;
                    missionDescription = "green green green";
                    break;
                case 2:
                    firstGem.color = _red;
                    secondGem.color = _red;
                    thirdGem.color = _red;
                    firstGem.sprite = sources[2];
                    secondGem.sprite = sources[2];
                    thirdGem.sprite = sources[2];
                    item.sprite = items[2];
                    if (rectTransform != null)
                    {
                        rectTransform.Rotate(new Vector3(0, 0, 45));
                        rectTransform.localScale = new Vector3(0.5f, 1, 1);
                    }
                    redCount = 3;
                    missionDescription = "red red red";
                    break;
                case 3:
                    firstGem.color = _yellow;
                    secondGem.color = _yellow;
                    thirdGem.color = _yellow;
                    firstGem.sprite = sources[3];
                    secondGem.sprite = sources[3];
                    thirdGem.sprite = sources[3];
                    item.sprite = items[3];
                    if (rectTransform != null)
                    {
                        rectTransform.Rotate(new Vector3(0, 0, 45));
                        rectTransform.localScale = new Vector3(0.5f, 1, 1);
                    }
                    item.color = _brownSaber;
                    brownCount = 3;
                    missionDescription = "yellow yellow yellow";
                    break;
                case 4:
                    // red red blue
                    firstGem.color = _red;
                    secondGem.color = _red;
                    thirdGem.color = _blue;
                    firstGem.sprite = sources[2];
                    secondGem.sprite = sources[2];
                    thirdGem.sprite = sources[0];
                    item.sprite = items[4];
                    if (rectTransform != null)
                    {
                        rectTransform.Rotate(new Vector3(0, 0, 45));
                        rectTransform.localScale = new Vector3(0.5f, 1, 1);
                    }
                    redCount = 2;
                    blueCount = 1;
                    missionDescription = "red red blue";
                    break;
                case 5:
                    // red red green
                    firstGem.color = _red;
                    secondGem.color = _red;
                    thirdGem.color = _green;
                    firstGem.sprite = sources[2];
                    secondGem.sprite = sources[2];
                    thirdGem.sprite = sources[1];
                    item.sprite = items[5];
                    redCount = 2;
                    greenCount = 1;
                    missionDescription = "red red green";
                    break;
                case 6:
                    // red red yellow
                    firstGem.color = _red;
                    secondGem.color = _red; 
                    thirdGem.color = _yellow;
                    firstGem.sprite = sources[2];
                    secondGem.sprite = sources[2];
                    thirdGem.sprite = sources[3];
                    item.sprite = items[6];
                    redCount = 2;
                    brownCount = 1;
                    missionDescription = "red red yellow";
                    break;
                case 7:
                    // blue blue green
                    firstGem.color = _blue;
                    secondGem.color = _blue;
                    thirdGem.color = _green;
                    firstGem.sprite = sources[0];
                    secondGem.sprite = sources[0];
                    thirdGem.sprite = sources[1];
                    item.sprite = items[7];
                    blueCount = 2;
                    greenCount = 1;
                    missionDescription = "blue blue green";
                    break;
                case 8:
                    // blue blue yellow
                    firstGem.color = _blue;
                    secondGem.color = _blue;
                    thirdGem.color = _yellow;
                    firstGem.sprite = sources[0];
                    secondGem.sprite = sources[0];
                    thirdGem.sprite = sources[3];
                    item.sprite = items[8];
                    blueCount = 2;
                    brownCount = 1;
                    missionDescription = "blue blue yellow";
                    break;
                case 9:
                    // green green yellow
                    firstGem.color = _green;
                    secondGem.color = _green;
                    thirdGem.color = _yellow;
                    firstGem.sprite = sources[1];
                    secondGem.sprite = sources[1];
                    thirdGem.sprite = sources[3];
                    item.sprite = items[9];
                    greenCount = 2;
                    brownCount = 1;
                    missionDescription = "green green yellow";
                    break;

            }

            var objectLine = new ObjectLine(new List<Image> { firstGem, secondGem, thirdGem }, 
                item, obj, times[i], redCount, blueCount, greenCount, brownCount);
            objectLine.SetStats(_targetCounter, missionDescription);
            _objectLines.Add(objectLine);
            // set timer
            obj.GetComponent<TargetTimer>().timeLeft = times[i];
        }
        
        _targetIndex++;
        _targetCounter++;
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

public class ObjectLine
{
    private readonly List<Image> _gemList;
    private readonly Image _upgradeItem;
    private readonly GameObject _obj;
    private float _timeLeft;
    private readonly int _redCount;
    private readonly int _blueCount;
    private readonly int _greenCount;
    private readonly int _yellowCount;
    // stats
    private int _missionIndex;
    private string _missionDescription;
    private string _isMissionCompleted;
    
    private readonly Color _blue;
    private readonly Color _green;
    private readonly Color _red;
    private readonly Color _yellow;

    private Color _firstColor;

    // constructor
    public ObjectLine(List<Image> gemList, Image upgradeItem, GameObject obj, float timeLeft, int redCount, int blueCount, int greenCount, int yellowCount)
    {
        this._gemList = gemList;
        this._upgradeItem = upgradeItem;
        this._obj = obj;
        this._timeLeft = timeLeft;
        this._redCount = redCount;
        this._blueCount = blueCount;
        this._greenCount = greenCount;
        this._yellowCount = yellowCount;
        this._blue = new Color(0.0f, 0.0f, 1.0f);
        this._green = new Color(0.0f, 1.0f, 0.0f);
        this._red = new Color(1.0f, 0.0f, 0.0f);
        this._yellow = new Color(1f, 0.76f, 0f, 1f);;
        _firstColor = gemList[0].color;
    }

    public Color GetFirstColor()
    {
        return _firstColor;
    }
    
    public GameObject GetGameObj()
    {
        return _obj;
    }
    
    public Image GetUpgradeItem()
    {
        return _upgradeItem;
    }
    // get color counts
    public int GetRedCount() { return _redCount; }
    public int GetBlueCount() { return _blueCount; }
    public int GetGreenCount() { return _greenCount; }
    public int GetYellowCount() { return _yellowCount; }

    // return true if the line is completed
    public bool RemoveFirstGem()
    {
        _gemList[0].color = new Color(0f, 0f, 0f, 0.0f);
        _gemList.RemoveAt(0);
        if (_gemList.Count == 0)
        {
            _isMissionCompleted = "1";
            return true;
        }
        _firstColor = _gemList[0].color;
        return false;
    }

    public void SetStats(int missionIndex, string missionDescription)
    {
        this._missionIndex = missionIndex;
        this._missionDescription = missionDescription;
        this._isMissionCompleted = "0";
    }

    // return true if the time is up
    public bool UpdateTime(float time)
    {
        _timeLeft -= time;
        return _timeLeft <= 0;
    }

    private bool IsSameColor(Color color1, Color color2)
    {
        return (Math.Round(color1.r,3)).Equals(Math.Round(color2.r,3)) 
               && (Math.Round(color1.g,3)).Equals(Math.Round(color2.g,3))
               && (Math.Round(color1.b,3)).Equals(Math.Round(color2.b,3));
    }

    public int GetIndex()
    {
        return _missionIndex;
    }

    public string GetDescription()
    {
        return _missionDescription;
    }

    public string GetCompleted()
    {
        return _isMissionCompleted;
    }
}
