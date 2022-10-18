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
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bgm;
    [SerializeField] private TextAsset Jsonfile;

    private GameObject newItem, newMine, newPlatform;
    private Vector3 spawnPos;
    private Block blockHandler;
    private Note noteHandler;
    private PlayerControl playerHadler;
    private BgmController _bgmHandler;
    private float playerX, xLen;
    private int ind = 0;
    private bool normal = false;

    private Object[] objArr;

    private void Awake()
    {
        string json = Jsonfile.text;
        Debug.Log("MyJson= "+json);
        objArr= JsonHelper.FromJson<Object>(json);
        noteHandler = note.GetComponent<Note>();
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
                xLen = noteHandler.transform.localScale.x;

                float yPos = objArr[ind].Pos switch
                {
                    0 => 4,
                    1 => 1,
                    2 => -1,
                    3 => -4,
                    _ => 0,
                };

                if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
                {
                    xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed *
                           (1 / Time.fixedDeltaTime);
                }

                spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

                // start spawn
                switch (objArr[ind].Type)
                {
                    // gravSwitch
                    case 0:
                        newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
                        Destroy(newItem, 3f);
                        if (objArr[ind].Pos == 1)
                        {
                            newItem.transform.localScale = new Vector3(1, -1, 1);
                        }

                        break;
                    // short note
                    case 1:
                        newItem = Instantiate(note, spawnPos, Quaternion.identity);
                        Destroy(newItem, 3f);
                        break;
                    // long note
                    case 2:
                        newItem = Instantiate(longNote, spawnPos, Quaternion.identity);
                        var newLongNote = newItem.GetComponent<LongNote>();
                        newLongNote.SetLength(xLen);
                        Destroy(newItem, 3f / 2.46f * xLen);
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