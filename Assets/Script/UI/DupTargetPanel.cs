// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using Image = UnityEngine.UI.Image;
//
// public class TargetPanel : MonoBehaviour
// {
//     public static TargetPanel Instance;
//
//     [SerializeField] private Sprite[] items;
//     // 0: common
//     // 1: clone
//     // 2: shield
//     // 3: freeze
//
//     private Sprite _shield;
//     private Sprite _clone;
//     private Sprite _freeze;
//     private Sprite _common;
//
//     public GameObject fireDiamond;
//     public SliderBar fireBar;
//     public GameObject grassDiamond;
//     public SliderBar grassBar;
//     public GameObject waterDiamond;
//     public SliderBar waterBar;
//     public GameObject rockDiamond;
//     public SliderBar rockBar;
//
//     public int patternDamage = 20;
//
//     // [SerializeField] private Image[] gems;
//     [SerializeField] private Sprite[] sources;
//     
//     
//     public GameObject targetLine;
//
//     // colors constants
//     private readonly Color _blue = new(0.016f, 0.67f, 1f, 1.0f);
//     private readonly Color _green = new(0.43f, 1f, 0.058f, 1.0f);
//     private readonly Color _red = new(1.0f, 0.5f, 0f, 1.0f);
//     private readonly Color _yellow = new(1f, 0.76f, 0f, 1f);
//     // private readonly Color _brownSaber = new(0.58f, 0.3f, 0f, 1f);
//     
//     public enum ItemType{
//         Shield,
//         Clone,
//         Freeze,
//         Common
//     }
//     
//     private List<ObjectLine> _objectLines;
//     private Target[] _targets ;
//     private int _targetLoopIndex;
//     
//     // for statistics
//     private int _targetCounter;
//
//     // 0: red*3 = common    cd: 8
//     // 1: green*3 = clone   cd: 15
//     // 2: yellow*3 = shield cd: 15
//     // 3: blue*3 = freeze   cd: 15
//
//     private readonly Target[] _level1Target = {
//
//         new(new[] { 0,}, new[] { 8f},new[]{ItemType.Common})
//     };
//     private const int Level1LoopIndex = 0;
//
//     private readonly Target[] _level2Target = {
//         new(new[] { 0, 1}, new[] { 8f, 12f }, new[]{ItemType.Common, ItemType.Clone}),
//     };
//
//     private const int Level2LoopIndex = 0;
//     
//     private readonly Target[] _level3Target = {
//         new(new[] { 0, 1, 2 }, new[] { 8f, 12f, 12f }, new[]{ItemType.Common, ItemType.Clone, ItemType.Shield}),
//     };
//     private const int Level3LoopIndex = 0;
//     
//     private readonly Target[] _level4Target = {
//         
//         // new(new[] { 1, 1, 0, 0, 2, 2 }, new[] { 20f, 20f, 20f, 20f, 20f, 20f }, new[]{ItemType.Freeze, ItemType.Shield, ItemType.Shield, ItemType.Shield, ItemType.Shield, ItemType.Shield}), // test
//         
//         new(new[] { 0, 1, 2, 3}, new[] { 8f, 12f, 12f, 12f, }, new[]{ItemType.Common, ItemType.Clone, ItemType.Shield, ItemType.Freeze}),
//
//     };
//     private const int Level4LoopIndex = 0;
//
//     // 0: blue*3 = shield
//     // 1: green*3 = clone
//     // 2: red*3 = freeze
//     // 3: yellow*3 = shield
//     // 4: red*2 + blue*1 = clone
//     // 5: red*2 + green*1 = freeze
//     // 6: red*2 + yellow*1 = shield
//     // 7: blue*2 + green*1 = clone
//     // 8: blue*2 + yellow*1 = freeze
//     // 9: green*2 + yellow*1 = shield
//     
//     private int _targetIndex;
//     
//     //for inventory system
//     // public Inventory inventory;
//     // [SerializeField] private InventoryUI uiInventory;
//
//     private void Awake()
//     {
//         Instance = this;
//         var scene = SceneManager.GetActiveScene();
//         _common = items[0];
//         _clone = items[1];
//         _shield = items[2];
//         _freeze = items[3];
//         
//         switch (scene.name)
//         {
//             case "Level1":
//                 _targets = _level1Target;
//                 _targetLoopIndex = Level1LoopIndex;
//                 break;
//             case "Level2":
//                 _targets = _level2Target;
//                 _targetLoopIndex = Level2LoopIndex;
//                 break;
//             case "Level3":
//                 _targets = _level3Target;
//                 _targetLoopIndex = Level3LoopIndex;
//                 break;
//             case "CollectTutorial":
//                 _targets = _level1Target;
//                 _targetLoopIndex = Level1LoopIndex;
//                 break;
//             case "MoveTutorial":
//                 _targets = _level1Target;
//                 _targetLoopIndex = Level1LoopIndex;
//                 break;
//             case "AttackTutorial":
//                 _targets = _level1Target;
//                 _targetLoopIndex = Level1LoopIndex;
//                 break;
//             case "TryoutTutorial":
//                 _targets = _level1Target;
//                 _targetLoopIndex = Level1LoopIndex;
//                 break;
//             case "SampleScene":
//                 _targets = _level4Target;
//                 _targetLoopIndex = Level4LoopIndex;
//                 break;
//             default:
//                 _targets = _level4Target;
//                 _targetLoopIndex = Level4LoopIndex;
//                 break;
//         }
//         _objectLines = new List<ObjectLine>();
//         _targetCounter = 1;
//     }
//
//
//     void Start()
//     {       
//         SetNextTarget();
//     }
//
//     private void FixedUpdate()
//     {
//         for(var i = 0; i < _objectLines.Count; i++)
//         {
//             var objectLine = _objectLines[i];
//             if(objectLine.IsCoolDown){
//                 if(objectLine.UpdateCDTimer(Time.fixedDeltaTime))
//                 {
//                     // cooldown is finished
//                     UpdateAnalytics(objectLine);
//                     // objectLine.GetGameObj().GetComponent<TargetTimer>().timeLeft = objectLine.GetTotalTime(); 
//                 }
//             }
//         }
//
//     }
//
//     private void SetColor(Color color)
//     {
//         for (var i = 0; i < _objectLines.Count; i++)
//         {
//             var objL = _objectLines[i];
//             var gemIndex = objL.GetGemIndex(color);
//             if ( gemIndex != -1)
//             { 
//                 // if it is on cooldown, then skip
//                 if (!objL.IsCoolDown && objL.RemoveGem(gemIndex))
//                 {
//                     // the line is completed
//                     switch(objL.GetItemType()){
//                         case ItemType.Shield:
//                             SkillController.Instance.CallShieldSkill();
//                             break;
//                         case ItemType.Clone:
//                             if (SkillController.Instance.IsCloneState() == false) {
//                                 SkillController.Instance.CallCloneSkill();
//                             }
//                             // SkillController.Instance.CallCloneSkill();
//                             break;
//                         case ItemType.Freeze:
//                             SkillController.Instance.CallFreezeSkill();
//                             break;
//                         case ItemType.Common:
//                             if (color == _red){
//                                 FireBall.acitivated = true;
//                             } else if (color == _green){
//                                 GrassBall.acitivated = true;
//                             } else if (color == _blue){
//                                 WaterBall.acitivated = true;
//                             } else if (color == _yellow){
//                                 RockBall.acitivated = true;
//                             } else {
//                                 RainbowBall.acitivated = true;
//                             }
//                             BossHealth.Instance.TakeDamage(patternDamage);
//                             break;
//                     }
//                     UpdateAnalytics(objL);
//                     objL.ResetGem();
//                     objL.SetCDTimer();
//                     // // set timer
//                     // objL.GetGameObj().GetComponent<TargetTimer>().timeLeft = objL.GetTotalTime();
//                 }
//                 break;
//             }
//         }
//     }
//
//     private void UpdateAnalytics(ObjectLine objLine){
//         if (Application.isEditor)
//         {
//             switch (SceneManager.GetActiveScene().name)
//             {
//                 case "Level1":
//                     Level1Editor.instance.UpdateQuest(objLine.GetIndex().ToString(), 
//                         objLine.GetDescription(), objLine.GetCompleted());
//                     break;
//                 case "Level2":
//                     Level2Editor.instance.UpdateQuest(objLine.GetIndex().ToString(), 
//                         objLine.GetDescription(), objLine.GetCompleted());
//                     break;
//                 case "Level3":
//                     Level3Editor.instance.UpdateQuest(objLine.GetIndex().ToString(), 
//                         objLine.GetDescription(), objLine.GetCompleted());
//                     break;
//             }
//         }
//         else
//         {
//             switch (SceneManager.GetActiveScene().name)
//             {
//                 case "Level1":
//                     Level1Web.instance.UpdateQuest(objLine.GetIndex().ToString(), 
//                         objLine.GetDescription(), objLine.GetCompleted());
//                     break;
//                 case "Level2":
//                     Level2Web.instance.UpdateQuest(objLine.GetIndex().ToString(), 
//                         objLine.GetDescription(), objLine.GetCompleted());
//                     break;
//                 case "Level3":
//                     Level3Web.instance.UpdateQuest(objLine.GetIndex().ToString(), 
//                         objLine.GetDescription(), objLine.GetCompleted());
//                     break;
//             }
//         }
//     }
//
//     public void TargetHit(Color color)
//     {
//         SetColor(color);
//     }
//
//     private void SetNextTarget()
//     {
//         var lines = _targets[_targetIndex].GetFormulaIndex();
//         var times = _targets[_targetIndex].GetTimeToCollect();
//         var itemTypes = _targets[_targetIndex].GetItemTypes();
//         for(var i = 0; i <  _targets[_targetIndex].TargetLength(); i++)
//         {
//             GameObject obj = Instantiate(targetLine, transform);
//             var firstGem = obj.transform.Find("FirstItem").gameObject.GetComponent<Image>();
//             var secondGem = obj.transform.Find("SecondItem").gameObject.GetComponent<Image>();
//             var thirdGem = obj.transform.Find("ThirdItem").gameObject.GetComponent<Image>();
//             var item = obj.transform.Find("UpgradeItem").gameObject.GetComponent<Image>();
//             var cd1 = obj.transform.Find("CD1").gameObject.GetComponent<Image>();
//             var cd2 = obj.transform.Find("CD2").gameObject.GetComponent<Image>();
//             var cd3 = obj.transform.Find("CD3").gameObject.GetComponent<Image>();
//             var cd4 = obj.transform.Find("CD4").gameObject.GetComponent<Image>();
//             cd1.fillAmount = 0; cd2.fillAmount = 0; cd3.fillAmount = 0; cd4.fillAmount = 0;
//             var cdList = new List<Image>
//             {
//                 cd1,
//                 cd2,
//                 cd3,
//                 cd4
//             };
//             var missionDescription = "";
//
//             var redCount = 0;
//             var blueCount = 0;
//             var greenCount = 0;
//             var brownCount = 0;
//             ItemType itemType = itemTypes[i];
//             
//             switch (lines[i])
//             {
//                 case 0:
//                     firstGem.color = _red;
//                     secondGem.color = _red;
//                     thirdGem.color = _red;
//                     firstGem.sprite = sources[0];
//                     secondGem.sprite = sources[0];
//                     thirdGem.sprite = sources[0];
//                     item.sprite = _common;
//                     // if (rectTransform != null)
//                     // {
//                     //     rectTransform.Rotate(new Vector3(0, 0, 45));
//                     //     rectTransform.localScale = new Vector3(0.5f, 1, 1);
//                     // }
//                     redCount = 3;
//                     missionDescription = "red red red";
//                     break;
//                 case 1:
//                     firstGem.color = _green;
//                     secondGem.color = _green;
//                     thirdGem.color = _green;
//                     firstGem.sprite = sources[1];
//                     secondGem.sprite = sources[1];
//                     thirdGem.sprite = sources[1];
//                     item.sprite = _clone;
//                     // if (rectTransform != null)
//                     // {
//                     //     rectTransform.Rotate(new Vector3(0, 0, 45));
//                     //     rectTransform.localScale = new Vector3(0.5f, 1, 1);
//                     // }
//                     greenCount = 3;
//                     missionDescription = "green green green";
//                     break;
//                 case 2:
//                     firstGem.color = _yellow;
//                     secondGem.color = _yellow;
//                     thirdGem.color = _yellow;
//                     firstGem.sprite = sources[2];
//                     secondGem.sprite = sources[2];
//                     thirdGem.sprite = sources[2];
//                     item.sprite = _shield;
//                     // if (rectTransform != null)
//                     // {
//                     //     rectTransform.Rotate(new Vector3(0, 0, 45));
//                     //     rectTransform.localScale = new Vector3(0.5f, 1, 1);
//                     // }
//                     brownCount = 3;
//                     missionDescription = "yellow yellow yellow";
//                     break;
//                 case 3:
//                     firstGem.color = _blue;
//                     secondGem.color = _blue;
//                     thirdGem.color = _blue;
//                     firstGem.sprite = sources[3];
//                     secondGem.sprite = sources[3];
//                     thirdGem.sprite = sources[3];
//                     item.sprite = _freeze;
//                     // if (rectTransform != null)
//                     // {
//                     //     rectTransform.Rotate(new Vector3(0, 0, 45));
//                     //     rectTransform.localScale = new Vector3(0.5f, 1, 1);
//                     // }
//                     blueCount = 3;
//                     missionDescription = "blue blue blue";
//                     break;
//             }
//
//             var objectLine = new ObjectLine(new List<Image> { firstGem, secondGem, thirdGem }, 
//                 item, obj, times[i], redCount, blueCount, greenCount, brownCount);
//             objectLine.SetStats(_targetCounter, missionDescription);
//             objectLine.SetItemType(itemType);
//             objectLine.SetCDList(cdList);
//             _objectLines.Add(objectLine);
//             // set timer
//             objectLine.GetGameObj().GetComponent<TargetTimer>().timeLeft = objectLine.GetTotalTime();
//         }
//         
//         _targetIndex++;
//         _targetCounter++;
//         if(_targetIndex == _targets.Length)
//             _targetIndex = _targetLoopIndex;
//         
//     }
//
//     private static bool IsSameColor(Color color1, Color color2)
//     {
//         return (Math.Round(color1.r,3)).Equals(Math.Round(color2.r,3)) 
//                && (Math.Round(color1.g,3)).Equals(Math.Round(color2.g,3))
//                && (Math.Round(color1.b,3)).Equals(Math.Round(color2.b,3))
//                && (Math.Round(color1.a,3)).Equals(Math.Round(color2.a,3));
//     }
// }
//
// public readonly struct Target
// {
//     private readonly int[] _formulaIndex;
//     private readonly float[] _timeToCollect;
//     private readonly TargetPanel.ItemType[] _itemTypes;
//     public Target(int[] formulaIndex, float[] timeToCollect, TargetPanel.ItemType[] itemTypes)
//     {
//         _formulaIndex = formulaIndex;
//         _timeToCollect = timeToCollect;
//         _itemTypes = itemTypes;
//     }
//
//     public int[] GetFormulaIndex()
//     { 
//         return _formulaIndex;
//     }
//     
//     public float[] GetTimeToCollect()
//     {
//         return _timeToCollect;
//     }
//
//     public TargetPanel.ItemType[] GetItemTypes()
//     {
//         return _itemTypes;
//     }
//     
//     public int TargetLength()
//     {
//         return _formulaIndex.Length;
//     }
// }
//
// public class ObjectLine
// {
//     private readonly List<Image> _gemList;
//     private int _gemCount;
//     private readonly Image _upgradeItem;
//     private readonly GameObject _obj;
//     private float _totalTime;
//     private readonly int _redCount;
//     private readonly int _blueCount;
//     private readonly int _greenCount;
//     private readonly int _yellowCount;
//     // stats
//     private int _missionIndex;
//     private string _missionDescription;
//     private string _isMissionCompleted;
//     
//     private readonly Color _blue;
//     private readonly Color _green;
//     private readonly Color _red;
//     private readonly Color _yellow;
//
//     private TargetPanel.ItemType _itemType;
//
//     private List<Image> _cdList;
//     public bool IsCoolDown;
//
//
//     // constructor
//     public ObjectLine(List<Image> gemList, Image upgradeItem, GameObject obj, float totalTime, int redCount, int blueCount, int greenCount, int yellowCount)
//     {
//         this._gemList = gemList;
//         this._upgradeItem = upgradeItem;
//         this._obj = obj;
//         this._totalTime = totalTime;
//         this._redCount = redCount;
//         this._blueCount = blueCount;
//         this._greenCount = greenCount;
//         this._yellowCount = yellowCount;
//         this._blue = new Color(0.0f, 0.0f, 1.0f, 1.0f);
//         this._green = new Color(0.0f, 1.0f, 0.0f, 1.0f);
//         this._red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
//         this._yellow = new Color(1f, 0.76f, 0f, 1f);
//         IsCoolDown = false;
//         _gemCount = 3;
//     }
//     
//     public GameObject GetGameObj()
//     {
//         return _obj;
//     }
//     
//     public Image GetUpgradeItem()
//     {
//         return _upgradeItem;
//     }
//     
//     public float GetTotalTime()
//     {
//         return _totalTime;
//     }
//     // get color counts
//     public int GetRedCount() { return _redCount; }
//     public int GetBlueCount() { return _blueCount; }
//     public int GetGreenCount() { return _greenCount; }
//     public int GetYellowCount() { return _yellowCount; }
//     public List<Image> GetCDList() { return _cdList; }
//     public TargetPanel.ItemType GetItemType() { return _itemType; }
//
//     // return true if the line is completed
//     public bool RemoveGem(int index)
//     {
//         var temp = _gemList[index].color;
//         _gemList[index].color = new Color(temp.r, temp.g, temp.b, 0.0f);
//         _gemCount--;
//         // _gemList.RemoveAt(index);
//         if (_gemCount == 0)
//         {
//             _isMissionCompleted = "1";
//             return true;
//         }
//         return false;
//     }
//
//     public void SetStats(int missionIndex, string missionDescription)
//     {
//         this._missionIndex = missionIndex;
//         this._missionDescription = missionDescription;
//         this._isMissionCompleted = "0";
//     }
//
//     public void SetItemType(TargetPanel.ItemType itemType)
//     {
//         this._itemType = itemType;
//     }
//
//     public void SetCDList(List<Image> cdList)
//     {
//         this._cdList = cdList;
//     }
//
//     public void SetCDTimer(){
//         for(var i = 0; i < _cdList.Count; i++){
//             _cdList[i].fillAmount = 1;
//         }
//         IsCoolDown = true;
//     }
//
//     public bool UpdateCDTimer(float time){
//         for(var i = 0; i < _cdList.Count; i++){
//             _cdList[i].fillAmount -= time/_totalTime;
//         }
//         if(_cdList[0].fillAmount <= 0){
//             IsCoolDown = false;
//             return true;
//         }
//         return false;
//     }
//
//     // return true if the time is up
//     public bool UpdateTime(float time)
//     {
//         _totalTime -= time;
//         return _totalTime <= 0;
//     }
//
//     public void ResetGem(){
//         for(var i = 0; i < _gemList.Count; i++){
//             var temp = _gemList[i].color;
//             _gemList[i].color = new Color(temp.r, temp.g, temp.b, 1.0f);
//         }
//         _gemCount = 3;
//     }
//     
//     // return the index of the corresponding gem
//     // return -1 if the gem is not found
//     public int GetGemIndex(Color gemColor)
//     {
//         for (var i = 0; i < _gemList.Count; i++)
//         {
//             if (IsSameColor(_gemList[i].color, gemColor))
//             {
//                 return i;
//             }
//         }
//         return -1;
//     }
//
//     private bool IsSameColor(Color color1, Color color2)
//     {
//         return (Math.Round(color1.r,3)).Equals(Math.Round(color2.r,3)) 
//                && (Math.Round(color1.g,3)).Equals(Math.Round(color2.g,3))
//                && (Math.Round(color1.b,3)).Equals(Math.Round(color2.b,3))
//                && (Math.Round(color1.a,3)).Equals(Math.Round(color2.a,3));
//     }
//
//     public int GetIndex()
//     {
//         return _missionIndex;
//     }
//
//     public string GetDescription()
//     {
//         return _missionDescription;
//     }
//
//     public string GetCompleted()
//     {
//         return _isMissionCompleted;
//     }
// }
