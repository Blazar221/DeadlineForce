using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
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
        { 0.0f, 0.0f }, { 0.0f, 0.0f }, { 0.75f, 0.75f }, { 1.5f, 1.5f }, { 2.25f, 2.25f }, { 3.0f, 3.0f }, { 3.75f, 3.75f },
        { 4.5f, 4.5f }, { 5.25f, 5.25f }, { 6.0f, 6.0f }, { 6.75f, 6.75f }, { 7.5f, 7.5f }, { 8.25f, 8.25f },
        { 9.0f, 9.0f }, { 9.75f, 9.75f }, { 10.5f, 10.5f }, { 11.25f, 11.25f }, { 12.0f, 12.0f }, { 12.75f, 12.75f },
        { 13.5f, 13.5f }, { 14.25f, 14.25f }, { 15.0f, 15.0f }, { 15.75f, 15.75f }, { 16.5f, 16.5f },
        { 17.25f, 17.25f }, { 18.0f, 18.0f }, { 18.75f, 18.75f }, { 19.5f, 19.5f }, { 20.25f, 20.25f },
        { 21.0f, 22.35f }, { 22.77f, 22.77f }, { 23.67f, 23.67f }, { 24.27f, 24.27f }, { 24.87f, 24.87f },
        { 25.47f, 25.47f }, { 26.07f, 26.07f }, { 26.67f, 26.67f }, { 27.27f, 27.27f }, { 27.87f, 27.87f },
        { 28.47f, 28.47f }, { 29.07f, 29.07f }, { 29.6f, 31.64f }, { 32.07f, 32.07f }, { 33.27f, 33.27f },
        { 33.87f, 33.87f }, { 34.4f, 41.0f }, { 41.37f, 41.37f }, { 41.6f, 43.64f }, { 44.05f, 46.05f },
        { 46.47f, 46.47f }, { 46.77f, 46.77f }, { 47.07f, 47.07f }, { 47.37f, 47.37f }, { 47.67f, 47.67f },
        { 47.97f, 47.97f }, { 48.27f, 48.27f }, { 48.57f, 48.57f }, { 48.87f, 48.87f }, { 49.17f, 49.17f },
        { 49.47f, 49.47f }, { 49.77f, 49.77f }, { 50.07f, 50.07f }, { 50.37f, 50.37f }, { 50.67f, 50.67f },
        { 51.2f, 53.24f }, { 53.67f, 53.67f }, { 53.9f, 55.64f }, { 56.07f, 56.07f }, { 56.3f, 57.14f },
        { 57.42f, 57.42f }, { 58.47f, 58.47f }, { 59.07f, 59.07f }, { 59.67f, 59.67f }, { 60.87f, 60.87f },
        { 62.07f, 62.07f }, { 62.67f, 62.67f }, { 63.27f, 63.27f }, { 63.87f, 63.87f }, { 64.47f, 64.47f },
        { 65.07f, 65.07f }, { 65.67f, 65.67f }, { 66.27f, 66.27f }, { 66.87f, 66.87f }, { 67.47f, 67.47f },
        { 68.0f, 70.04f }, { 70.47f, 70.47f }, { 71.67f, 71.67f }, { 72.27f, 72.27f }, { 73.25f, 79.75f }
    };
    
    // 0 - lower, 1 - upper
    private short[] posArr = new short[]
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0
    };

    // 0 - change, 1 - short, 2 - long
    private short[] itemArr = new short[]
    {
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 0, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 2, 0, 1, 1, 2, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 0, 2, 1, 2, 0, 1, 1, 1, 0, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 0, 1, 1, 2
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
        playerX = playerHadler.transform.localScale.x;
        Debug.Log(playerX);
        StartCoroutine(SpawnNewItem());
        StartCoroutine(SpawnNewBlock());
    }

    private IEnumerator SpawnNewBlock()
    {
        int nxtGravSwitchInd = 0, minePosY = 0;
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
            if (posArr[gravSwitchIndLs[nxtGravSwitchInd]] == 0)
            {
                minePosY = 4;
            }
            else if (posArr[gravSwitchIndLs[nxtGravSwitchInd]] == 1)
            {
                minePosY = -4;
            }
            spawnPos = new Vector3(playerX+26f, minePosY, 0);
            newBlock = Instantiate(block, spawnPos, Quaternion.identity);
        }
    }

    private IEnumerator SpawnNewItem()
    {
        float yPos = 0;
        while (ind < timeArr.Length/2)
        {
            yield return new WaitForSeconds(timeArr[ind, 0]-timeArr[ind-1, 0]);
            xLen = noteHandler.transform.localScale.x;
            //Debug.Log("Note Up:" + time[ind, 0]);
            if (posArr[ind-1] == 0)
            {
                yPos = -4;
            }
            else if (posArr[ind-1] == 1)
            {
                yPos = 4;
            }
            if (timeArr[ind, 1] - timeArr[ind, 0] != 0)
            {
                xLen = (timeArr[ind, 1] - timeArr[ind, 0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }
            spawnPos = new Vector3(playerX+16f+xLen-xLen/2.46f*0.9f, yPos, 0);

            if (itemArr[ind-1] == 0)
            {
                newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
            }
            else if(itemArr[ind-1] != 0)
            {
                newItem = Instantiate(note, spawnPos, Quaternion.identity);
            }
            if (timeArr[ind, 1] - timeArr[ind, 0] != 0)
            {
                Destroy(newItem, 3f/noteHandler.transform.localScale.x * xLen);
            }
            else
            {
                Destroy(newItem, 3f);
            }

            newItem.transform.localScale = new Vector3(xLen, noteHandler.transform.localScale.y, 0);
            ind++;
        }
    }
}
