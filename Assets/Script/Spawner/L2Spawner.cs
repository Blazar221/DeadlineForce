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



public class L2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject bgm;

    private GameObject newItem,newPlatform;
    private Vector3 spawnPos;
    private Note noteHandler;
    private PlayerControl playerHandler;
    private float playerX, xLen;
    private int ind = 1;
    private Object[] objArr;

    


    private void Awake()
    {
        string json = File.ReadAllText(Application.dataPath + "/Resources/L2Spawner.json");
        Debug.Log("MyJson= "+json);
        objArr= JsonHelper.FromJson<Object>(json);
        noteHandler = note.GetComponent<Note>();
        playerHandler = player.GetComponent<PlayerControl>();
        // sort the notes by time
        var watch = Stopwatch.StartNew();
        Array.Sort(objArr, new ObjectComparer());
        var elabpsedMs = watch.ElapsedMilliseconds;
        //Debug.Log("Sorting took " + elabpsedMs + "ms");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerX = playerHandler.transform.position.x;
        StartCoroutine(SpawnNewItem());
        // StartCoroutine(SpawnNewPlatform());
    }

    private IEnumerator SpawnNewItem()
    {
        while (ind < objArr.Length)
        {
            if(objArr[ind].TimeStamp[0]-objArr[ind-1].TimeStamp[0] != 0){
                yield return new WaitForSeconds(objArr[ind].TimeStamp[0]-objArr[ind-1].TimeStamp[0]);
            }
            xLen = noteHandler.transform.localScale.x;

            float yPos = objArr[ind].Pos switch
            {
                0 => 4,
                1 => 1,
                2 => -1,
                4 => 0,
                _ => -4,
            };

            if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
            {
                xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

            //Debug.Log("obj: " + ind + " " + objArr[ind].TimeStamp[0] + " " + objArr[ind].TimeStamp[1] + " " + objArr[ind].Pos + " " + objArr[ind].Type);
            // start spawn
            switch (objArr[ind].Type)
            {
                case 0:
                    newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    if (objArr[ind].Pos == 0)
                    {
                        newItem.transform.localScale = new Vector3(1, -1, 1);
                    }
                    break;
                case 1:
                    newItem = Instantiate(note, spawnPos, Quaternion.identity);
                    if(objArr[ind].IsMain){
                        newItem.GetComponent<SpriteRenderer>().color = new Color32(6,248,230,255);
                        newItem.transform.localScale = new Vector3(1.2f, 1.2f);
                    }
                    Destroy(newItem, 3f);
                    break;
                case 2:
                    newItem = Instantiate(longNote, spawnPos, Quaternion.identity);
                    var newLongNote = newItem.GetComponent<LongNote>();
                    newLongNote.SetLength(xLen);
                    Destroy(newItem, (spawnPos.x + 12 + xLen/2) / (noteHandler.speed * 1/Time.fixedDeltaTime));
                    // Destroy(newItem, 3f/2.46f*xLen);
                    break;
                case 3:
                    newItem = Instantiate(block, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
                case 4:
                    newPlatform = Instantiate(platform, spawnPos, Quaternion.identity);
                    var newPlatform_ = newPlatform.GetComponent<platform>();
                    newPlatform_.SetLength(xLen);
                    Destroy(newPlatform, (spawnPos.x + 12 + xLen/2) / (noteHandler.speed * 1/Time.fixedDeltaTime));
                    // Destroy(newPlatform, 3f/2.46f*xLen);
                    break;
            }
            ind++;
        }
    }
}