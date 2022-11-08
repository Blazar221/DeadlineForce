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

public class TutorialSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject fireDiamond;
    [SerializeField] private GameObject waterDiamond;
    [SerializeField] private GameObject grassDiamond;
    [SerializeField] private GameObject rockDiamond;

    [SerializeField] private GameObject longNote;
    
    [SerializeField] private GameObject bandit;
    [SerializeField] private GameObject bgm;
    [SerializeField] private TextAsset Jsonfile;

    private GameObject newItem, newMine, newPlatform;

    private Vector3 spawnPos;

    private Bandit blockHandler;
    
    private Diamond fireDiamondHandler;
    private Diamond waterDiamondHandler;
    private Diamond grassDiamondHandler;
    private Diamond rockDiamondHandler;
    
    private PlayerMovement playerHadler;
    private BgmController _bgmHandler;

    [SerializeField]
    public float moveSpeed; 

    private float playerX, xLen;

    private int ind = 1;

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

        blockHandler = bandit.GetComponent<Bandit>();
        blockHandler.SetSpeed(moveSpeed);
        
        playerHadler = player.GetComponent<PlayerMovement>();
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
            yield return new WaitForSeconds(objArr[ind].TimeStamp[0]-objArr[ind-1].TimeStamp[0]);
            //Debug.Log("loop start:"+Time.time);

            // start spawn
               Object toSpawn = objArr[ind];

                float yPos = toSpawn.Pos switch
                {
                    0 => 4.2f,
                    1 => 1.25f,
                    2 => -1.25f,
                    3 => -4.2f,
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
                        //Debug.Log(xLen);
                        //Debug.Log(moveSpeed);
                        //Debug.Log(playerX + moveSpeed * (2f / Time.fixedDeltaTime) + xLen / 2);
                        
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
                        newLongNote.SetSpeed(moveSpeed);
                        // Destroy(newItem, 3f / 2.46f * xLen);
                        Destroy(newItem, (spawnPos.x + 12 + xLen/2) / (moveSpeed * 1/Time.fixedDeltaTime));
                        break;
                }
                ind++;
            //Debug.Log("loop end:"+Time.time);
        }
    }
}

