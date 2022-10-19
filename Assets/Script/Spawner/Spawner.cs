using System;
using System.IO;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;
using Object = Script.Object;
using JsonHelper = Script.JsonHelper;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject gravSwitch;

    [SerializeField] private GameObject fireDiamond;
    [SerializeField] private GameObject waterDiamond;
    [SerializeField] private GameObject grassDiamond;
    [SerializeField] private GameObject rockDiamond;

    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bgm;
    [SerializeField] private TextAsset Jsonfile;

    private GameObject newItem, newMine, newPlatform;

    private Vector3 spawnPos;

    private Block blockHandler;
    
    private Diamond fireDiamondHandler;
    private Diamond waterDiamondHandler;
    private Diamond grassDiamondHandler;
    private Diamond rockDiamondHandler;
    
    private PlayerControl playerHadler;
    private BgmController _bgmHandler;

    [SerializeField]
    public float moveSpeed; 

    private float playerX, xLen;

    private int ind = 0;

    private bool normal = false;

    private Object[] objArr;

    private void Awake()
    {
        string json = Jsonfile.text;
        Debug.Log("MyJson= "+json);
        objArr= JsonHelper.FromJson<Object>(json);

        fireDiamondHandler = fireDiamond.GetComponent<Diamond>();
        waterDiamondHandler = waterDiamond.GetComponent<Diamond>();
        grassDiamondHandler = grassDiamond.GetComponent<Diamond>();
        rockDiamondHandler = rockDiamond.GetComponent<Diamond>();
        fireDiamondHandler.SetSpeed(moveSpeed);
        waterDiamondHandler.SetSpeed(moveSpeed);
        grassDiamondHandler.SetSpeed(moveSpeed);
        rockDiamondHandler.SetSpeed(moveSpeed);
        
        playerHadler = player.GetComponent<PlayerControl>();
        _bgmHandler = bgm.GetComponent<BgmController>();
        // sort the notes by time
        var watch = Stopwatch.StartNew();
        Array.Sort(objArr, new ObjectComparer());
        var elabpsedMs = watch.ElapsedMilliseconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerX = playerHadler.transform.position.x;
        StartCoroutine(SpawnNewItem());
    }
    
    private IEnumerator SpawnNewItem()
    {
        while (ind < objArr.Length)
        {
            if (normal == false)
            {
                yield return new WaitForSeconds(0.0001f);
                normal = true;
            }
            Debug.Log("time:"+ _bgmHandler.songPosition);
            Debug.Log("ind:"+ ind);
            if (objArr[ind].TimeStamp[0] > _bgmHandler.songPosition)
            {
                yield return new WaitForSeconds((objArr[ind].TimeStamp[0]-_bgmHandler.songPosition)*0.8f);
            }
            while (ind < objArr.Length && objArr[ind].TimeStamp[0] <= _bgmHandler.songPosition)
            {   
                Object toSpawn = objArr[ind];

                float yPos = toSpawn.Pos switch
                {
                    0 => 4,
                    1 => 1,
                    2 => -1,
                    3 => -4,
                    _ => 0,
                };

                // start spawn
                switch (toSpawn.Type)
                {
                    // element diamond
                    case 1:
                        Diamond curDiamondHandler = toSpawn.Color switch
                        {
                            0 => fireDiamondHandler,
                            1 => waterDiamondHandler,
                            2 => grassDiamondHandler,
                            _ => rockDiamondHandler,
                        };
                        GameObject toCopy = toSpawn.Color switch
                        {
                            0 => fireDiamond,
                            1 => waterDiamond,
                            2 => grassDiamond,
                            _ => rockDiamond,
                        }; 

                        xLen = curDiamondHandler.transform.localScale.x;
                        Debug.Log(xLen);
                        Debug.Log(moveSpeed);
                        Debug.Log(playerX + moveSpeed * (2f / Time.fixedDeltaTime) + xLen / 2);
                        
                        spawnPos = new Vector3(playerX + moveSpeed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

                        newItem = Instantiate(toCopy, spawnPos, Quaternion.identity);
                        newItem.GetComponent<Diamond>().SetSpeed(moveSpeed);
                        Destroy(newItem, 3f);
                        break;
                    // long note
                    case 2:
                        xLen = (toSpawn.TimeStamp[1] - toSpawn.TimeStamp[0]) * moveSpeed * (1 / Time.fixedDeltaTime);
                        spawnPos = new Vector3(playerX + moveSpeed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

                        newItem = Instantiate(longNote, spawnPos, Quaternion.identity);
                        var newLongNote = newItem.GetComponent<LongNote>();
                        newLongNote.SetLength(xLen);
                        // Destroy(newItem, 3f / 2.46f * xLen);
                        Destroy(newItem, (spawnPos.x + 12 + xLen/2) / (moveSpeed * 1/Time.fixedDeltaTime));
                        break;
                    //block
                    case 3:
                        newItem = Instantiate(block, spawnPos, Quaternion.identity);
                        Destroy(newItem, 3f);
                        break;
                }
                ind++;
            }
        }
    }
}


public class ObjectComparer: IComparer{
    public int Compare(object x, object y){
        if (((Object)x).TimeStamp[0] > ((Object)y).TimeStamp[0])
            return 1;
        else if (((Object)x).TimeStamp[0] < ((Object)y).TimeStamp[0])
            return -1;
        else
            return 0;
    }
}