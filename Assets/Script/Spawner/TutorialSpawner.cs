using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;

    private GameObject newItem, newBlock;
    private Vector3 spawnPos;
    private Block spawnedObjHandler;
    private Note noteHandler;
    private PlayerControl playerHadler;
    private float playerX, xLen;
    private int ind = 1, i;
    private List<int> gravSwitchIndLs = new List<int>();

    //IMPORTANT: length of 2d time will have one more element {0, 0}, be aware of index!
    private float[,] timeArr = new float[,]
    {
        { 0.0f, 0.0f }, { 0.0f, 0.0f }, { 1.75f, 3.75f }, { 5.5f, 5.5f }, { 7.0f, 7.0f }, { 8.75f, 8.75f },
        { 9.25f, 9.25f }, { 10.0f, 10.0f }, { 10.5f, 10.5f }
    };
    
    // 0 - lower, 1 - upper
    private short[] posArr = new short[]
    {
        0, 0, 0, 0, 1, 1, 1, 0
    };

    // 0 - change, 1 - short, 2 - long, 3 - block
    private short[] itemArr = new short[]
    {
        1, 2, 0, 3, 1, 0, 3, 1
    };

    private void Awake()
    {
        noteHandler = note.GetComponent<Note>();
        playerHadler = player.GetComponent<PlayerControl>();
        for (i = 0; i < itemArr.Length; i++)
        {
            if (itemArr[i] == 0)
            {
                gravSwitchIndLs.Add(i);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerX = playerHadler.transform.position.x;
        StartCoroutine(SpawnNewItem());
        //StartCoroutine(SpawnNewBlock());
    }

    private IEnumerator SpawnNewBlock()
    {
        var nxtGravSwitchInd = 0;
        while (ind < timeArr.Length)
        {
            yield return new WaitForSeconds(Random.Range(1f, 4f));
            //当前重力转换点已被generate，要在另一平面生成障碍
            if (ind-1 > gravSwitchIndLs[nxtGravSwitchInd])
            {
                nxtGravSwitchInd++;
            }

            if (nxtGravSwitchInd == gravSwitchIndLs.Count)
            {
                break;
            }

            var minePosY = posArr[gravSwitchIndLs[nxtGravSwitchInd]] switch
            {
                0 => 4,
                1 => -4,
                _ => 0,
            };
            
            spawnPos = new Vector3(playerX+26f, minePosY, 0);
            newBlock = Instantiate(block, spawnPos, Quaternion.identity);
            Destroy(newBlock, 3f);
        }
    }

    private IEnumerator SpawnNewItem()
    {
        while (ind < timeArr.Length/2)
        {
            yield return new WaitForSeconds(timeArr[ind, 0]-timeArr[ind-1, 0]);
            xLen = noteHandler.transform.localScale.x;

            float yPos = posArr[ind - 1] switch
            {
                0 => -4,
                1 => 4,
                _ => 0,
            };

            if (timeArr[ind, 1] - timeArr[ind, 0] != 0)
            {
                xLen = (timeArr[ind, 1] - timeArr[ind, 0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

            // start spawn
            switch (itemArr[ind - 1])
            {
                case 0:
                    newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    if (posArr[ind - 1] == 1)
                    {
                        newItem.transform.localScale = new Vector3(1, -1, 1);
                    }
                    break;
                case 1:
                    newItem = Instantiate(note, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
                case 2:
                    newItem = Instantiate(longNote, spawnPos, Quaternion.identity);
                    var newLongNote = newItem.GetComponent<LongNote>();
                    newLongNote.SetLength(xLen);
                    Destroy(newItem, 3f/2.46f*xLen);
                    break;
                case 3:
                    newItem = Instantiate(block, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    break;
            }
            ind++;
        }
    }
}
