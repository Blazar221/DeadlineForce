using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Stopwatch = System.Diagnostics.Stopwatch;
using Object = Script.Object;


public class L3Spawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;

    private GameObject newItem,newPlatform;
    private Vector3 spawnPos;
    private Note noteHandler;
    private PlayerControl playerHandler;
    private float playerX, xLen;
    private int ind = 1;
    

    private Object[] objArr = new Object[]
    {
        new Object(new float[]{ 0.0f, 0.0f }, 3,  1), // index zero doesn't spawn
        new Object(new float[]{ 1.57f, 4.85f }, 3,  2), 
        new Object(new float[]{ 5.32f, 5.32f }, 3,  1), 
        new Object(new float[]{ 7.19f, 7.19f }, 3,  1), 
        new Object(new float[]{ 8.13f, 8.13f }, 3,  0),     // grav switch
        new Object(new float[]{ 9.07f, 12.35f }, 0,  2), 
        new Object(new float[]{ 12.82f, 12.82f }, 0,  1), 
        new Object(new float[]{ 14.69f, 14.69f }, 0,  1), 
        new Object(new float[]{ 15.63f, 15.63f }, 0,  0),   // grav switch
        new Object(new float[]{ 16.57f, 17.97f }, 3,  2), 
        new Object(new float[]{ 18.44f, 18.44f }, 3,  1), 
        new Object(new float[]{ 20.32f, 21.72f }, 3,  2),
        new Object(new float[]{ 22.19f, 22.19f }, 3,  1), 
        new Object(new float[]{ 23.13f, 23.13f }, 3,  0),   // grav switch
        new Object(new float[]{ 24.07f, 24.07f }, 0,  1), 
        new Object(new float[]{ 25.0f, 25.5f }, 0,  2),     // long note
        new Object(new float[]{ 25.94f, 25.94f }, 0,  1), 
        new Object(new float[]{ 26.88f, 27.35f }, 0,  2),   // long note 
        new Object(new float[]{ 27.82f, 27.82f }, 0,  1), 
        new Object(new float[]{ 28.75f, 29.22f }, 0,  2),   // long note
        new Object(new float[]{ 29.69f, 29.69f }, 0,  1), 
        new Object(new float[]{ 30.63f, 30.63f }, 0,  0),   // grav switch
        new Object(new float[]{ 31.57f, 31.57f }, 3,  1), 
        new Object(new float[]{ 32.03f, 32.03f }, 3,  1), 
        new Object(new float[]{ 32.5f, 32.5f }, 3,  1), 
        new Object(new float[]{ 32.97f, 32.97f }, 3,  3),   // mines
        new Object(new float[]{ 33.44f, 33.44f }, 3,  1), 
        new Object(new float[]{ 33.91f, 33.91f }, 3,  1), 
        new Object(new float[]{ 34.38f, 34.38f }, 3,  1), 
        new Object(new float[]{ 34.85f, 34.85f }, 3,  3),   // mines 
        new Object(new float[]{ 35.32f, 35.32f }, 3,  1), 
        new Object(new float[]{ 35.78f, 35.78f }, 3,  1), 
        new Object(new float[]{ 36.25f, 36.25f }, 3,  1), 
        new Object(new float[]{ 36.72f, 36.72f }, 3,  3),   // mines  
        new Object(new float[]{ 37.19f, 37.19f }, 3,  1), 
        new Object(new float[]{ 37.66f, 37.66f }, 3,  1), 
        new Object(new float[]{ 38.13f, 38.13f }, 3,  0),   // grav switch
        new Object(new float[]{ 39.07f, 39.07f }, 0,  1), 
        new Object(new float[]{ 39.3f, 39.3f }, 0,  1), 
        new Object(new float[]{ 39.53f, 39.53f }, 0,  1), 
        new Object(new float[]{ 39.77f, 39.77f }, 0,  1), 
        new Object(new float[]{ 40.0f, 40.0f }, 0,  1), 
        new Object(new float[]{ 40.2f, 40.2f }, 0,  1), 
        new Object(new float[]{ 40.47f, 40.47f }, 0,  1), 
        new Object(new float[]{ 40.71f, 40.71f }, 0,  1), 
        new Object(new float[]{ 40.94f, 40.94f }, 0,  1), 
        new Object(new float[]{ 41.17f, 41.17f }, 0,  1), 
        new Object(new float[]{ 41.41f, 41.41f }, 0,  1), 
        new Object(new float[]{ 41.64f, 41.64f }, 0,  1), 
        new Object(new float[]{ 41.88f, 41.88f }, 0,  3),   // mines 
        new Object(new float[]{ 42.11f, 42.11f }, 0,  1), 
        new Object(new float[]{ 42.35f, 42.35f }, 0,  1), 
        new Object(new float[]{ 42.58f, 42.58f }, 0,  1), 
        new Object(new float[]{ 42.82f, 44.69f }, 0,  2),   // long note
        new Object(new float[]{ 45.16f, 45.16f }, 0,  0),   // grav switch
        new Object(new float[]{ 46.57f, 46.57f }, 3,  1), 
        new Object(new float[]{ 46.8f, 46.8f }, 3,  1), 
        new Object(new float[]{ 47.03f, 47.03f }, 3,  1), 
        new Object(new float[]{ 47.27f, 47.27f }, 3,  1), 
        new Object(new float[]{ 47.5f, 47.5f }, 3,  1), 
        new Object(new float[]{ 47.97f, 47.97f }, 3,  1), 
        new Object(new float[]{ 48.21f, 48.21f }, 3,  1), 
        new Object(new float[]{ 48.44f, 48.44f }, 3,  1), 
        new Object(new float[]{ 48.67f, 48.67f }, 3,  1), 
        new Object(new float[]{ 48.91f, 48.91f }, 3,  1), 
        new Object(new float[]{ 49.14f, 49.14f }, 3,  1), 
        new Object(new float[]{ 49.38f, 49.38f }, 3,  1), 
        new Object(new float[]{ 49.61f, 49.61f }, 3,  1), 
        new Object(new float[]{ 49.85f, 49.85f }, 3,  1), 
        new Object(new float[]{ 50.08f, 50.08f }, 3,  1), 
        new Object(new float[]{ 50.55f, 50.55f }, 3,  1), 
        new Object(new float[]{ 51.02f, 51.02f }, 3,  1), 
        new Object(new float[]{ 51.49f, 51.49f }, 3,  1), 
        new Object(new float[]{ 51.96f, 51.96f }, 3,  1), 
        new Object(new float[]{ 52.42f, 52.42f }, 3,  1), 
        new Object(new float[]{ 52.89f, 52.89f }, 3,  1), 
        new Object(new float[]{ 53.13f, 53.36f }, 3,  2),   // long note
        new Object(new float[]{ 53.83f, 53.83f }, 3,  1), 
        new Object(new float[]{ 54.3f, 54.3f }, 3,  1), 
        new Object(new float[]{ 54.77f, 54.77f }, 3,  1), 
        new Object(new float[]{ 55.24f, 55.24f }, 3,  1), 
        new Object(new float[]{ 55.71f, 55.71f }, 3,  1), 
        new Object(new float[]{ 56.17f, 56.17f }, 3,  1), 
        new Object(new float[]{ 56.64f, 56.64f }, 3,  1), 
        new Object(new float[]{ 57.11f, 57.11f }, 3,  1), 
        new Object(new float[]{ 57.35f, 57.35f }, 3,  3),   // mines 
        new Object(new float[]{ 57.58f, 57.58f }, 3,  1), 
        new Object(new float[]{ 58.05f, 58.05f }, 3,  1), 
        new Object(new float[]{ 58.52f, 58.52f }, 3,  1), 
        new Object(new float[]{ 58.75f, 58.75f }, 3,  1), 
        new Object(new float[]{ 58.99f, 58.99f }, 3,  1), 
        new Object(new float[]{ 59.46f, 59.46f }, 3,  1), 
        new Object(new float[]{ 59.92f, 59.92f }, 3,  1), 
        new Object(new float[]{ 60.16f, 60.16f }, 3,  1), 
        new Object(new float[]{ 60.39f, 60.39f }, 3,  1), 
        new Object(new float[]{ 60.63f, 60.63f }, 3,  0),   // grav switch
        new Object(new float[]{ 61.57f, 61.57f }, 0,  1), 
        new Object(new float[]{ 61.8f, 61.8f }, 0,  1), 
        new Object(new float[]{ 62.03f, 62.03f }, 0,  1), 
        new Object(new float[]{ 62.27f, 62.27f }, 0,  1), 
        new Object(new float[]{ 62.5f, 62.5f }, 0,  1), 
        new Object(new float[]{ 62.97f, 62.97f }, 0,  1), 
        new Object(new float[]{ 63.21f, 63.21f }, 0,  1), 
        new Object(new float[]{ 63.44f, 63.44f }, 0,  1), 
        new Object(new float[]{ 63.67f, 63.67f }, 0,  1), 
        new Object(new float[]{ 63.91f, 63.91f }, 0,  1), 
        new Object(new float[]{ 64.14f, 64.14f }, 0,  1), 
        new Object(new float[]{ 64.38f, 64.61f }, 0,  2),   // long note
        new Object(new float[]{ 64.85f, 65.32f }, 0,  2), 

        new Object(new float[]{ 65.78f, 65.78f }, 0,  1), 
        new Object(new float[]{ 66.25f, 66.25f }, 0,  1), 
        new Object(new float[]{ 66.72f, 66.72f }, 0,  1), 
        new Object(new float[]{ 67.19f, 67.19f }, 0,  1), 
        new Object(new float[]{ 67.66f, 67.66f }, 0,  1), 
        new Object(new float[]{ 68.13f, 68.13f }, 0,  0),   // grav switch
        new Object(new float[]{ 69.07f, 69.07f }, 3,  1), 
        new Object(new float[]{ 69.53f, 69.53f }, 3,  1), 
        new Object(new float[]{ 70.0f, 70.0f }, 3,  1), 
        new Object(new float[]{ 70.47f, 70.47f }, 3,  1), 
        new Object(new float[]{ 70.94f, 70.94f }, 3,  1), 
        new Object(new float[]{ 71.41f, 71.41f }, 3,  1), 
        new Object(new float[]{ 71.88f, 71.88f }, 3,  1), 
        new Object(new float[]{ 72.35f, 72.35f }, 3,  1), 
        new Object(new float[]{ 72.82f, 72.82f }, 3,  1), 
        new Object(new float[]{ 73.28f, 73.28f }, 3,  1), 
        new Object(new float[]{ 73.75f, 73.75f }, 3,  1), 
        new Object(new float[]{ 74.22f, 74.22f }, 3,  1), 
        new Object(new float[]{ 74.69f, 74.69f }, 3,  1), 
        new Object(new float[]{ 75.39f, 75.39f }, 3,  1), 
        new Object(new float[]{ 75.63f, 75.63f }, 3,  1), 
        new Object(new float[]{ 76.1f, 76.1f }, 3,  1), 

        // second lane
        new Object(new float[]{ 16.57f, 16.57f }, 0,  1), 
        new Object(new float[]{ 18.44f, 18.44f }, 0,  1), 
        new Object(new float[]{ 20.32f, 20.32f }, 0,  1), 
        new Object(new float[]{ 22.19f, 22.19f }, 0,  1), 
        new Object(new float[]{ 24.07f, 24.07f }, 3,  1), 
        new Object(new float[]{ 25.94f, 25.94f }, 3,  1), 
        new Object(new float[]{ 27.82f, 27.82f }, 3,  1), 
        new Object(new float[]{ 29.69f, 29.69f }, 3,  1), 
        new Object(new float[]{ 32.97f, 32.97f }, 0,  1), 
        new Object(new float[]{ 33.91f, 33.91f }, 0,  1), 
        new Object(new float[]{ 35.32f, 35.32f }, 0,  1), 
        new Object(new float[]{ 37.19f, 37.19f }, 0,  1), 
        new Object(new float[]{ 39.07f, 39.07f }, 2,  1), 
        new Object(new float[]{ 40.0f, 40.0f }, 2,  1), 
        new Object(new float[]{ 40.94f, 40.94f }, 2,  1), 
        new Object(new float[]{ 41.88f, 41.88f }, 2,  1), 
        new Object(new float[]{ 46.57f, 46.57f }, 0,  1), 
        new Object(new float[]{ 47.5f, 47.5f }, 0,  1), 
        new Object(new float[]{ 48.44f, 48.44f }, 0,  1), 
        new Object(new float[]{ 48.67f, 48.67f }, 0,  1), 
        new Object(new float[]{ 48.91f, 48.91f }, 0,  1), 
        new Object(new float[]{ 49.14f, 49.14f }, 0,  1), 
        new Object(new float[]{ 50.78f, 50.78f }, 0,  1), 
        new Object(new float[]{ 51.72f, 51.72f }, 0,  1), 
        new Object(new float[]{ 52.66f, 52.66f }, 0,  1), 
        new Object(new float[]{ 53.6f, 53.6f }, 0,  1), 
        new Object(new float[]{ 54.53f, 54.53f }, 0,  1), 
        new Object(new float[]{ 55.47f, 55.47f }, 0,  1), 
        new Object(new float[]{ 56.41f, 56.41f }, 0,  1), 
        new Object(new float[]{ 57.35f, 57.35f }, 0,  1), 
        new Object(new float[]{ 58.28f, 58.28f }, 0,  1), 
        new Object(new float[]{ 59.22f, 59.22f }, 0,  1), 
        new Object(new float[]{ 60.16f, 60.16f }, 0,  1), 
        new Object(new float[]{ 61.57f, 61.57f }, 2,  1), 
        new Object(new float[]{ 63.44f, 63.44f }, 2,  1), 
        new Object(new float[]{ 65.32f, 65.32f }, 2,  1), 
        new Object(new float[]{ 67.19f, 67.19f }, 2,  1),
        new Object(new float[]{ 69.07f, 69.07f }, 0,  1), 
        new Object(new float[]{ 70.94f, 70.94f }, 0,  1), 
        new Object(new float[]{ 72.82f, 72.82f }, 0,  1), 
        new Object(new float[]{ 74.69f, 74.69f }, 0,  1), 

        // third lane
        new Object(new float[]{ 18.44f, 18.44f }, 2,  1), 
        new Object(new float[]{ 22.19f, 22.19f }, 2,  1), 
        new Object(new float[]{ 25.94f, 25.94f }, 2,  1), 
        new Object(new float[]{ 27.82f, 27.82f }, 2,  1), 
        new Object(new float[]{ 31.57f, 31.57f }, 2,  1), 
        new Object(new float[]{ 33.44f, 33.44f }, 2,  1), 
        new Object(new float[]{ 35.32f, 35.32f }, 2,  1), 
        new Object(new float[]{ 37.19f, 37.19f }, 2,  1), 
        new Object(new float[]{ 39.07f, 39.07f }, 1,  1), 
        new Object(new float[]{ 40.94f, 40.94f }, 1,  1), 
        new Object(new float[]{ 42.82f, 42.82f }, 1,  1), 
        new Object(new float[]{ 46.57f, 46.57f }, 2,  1), 
        new Object(new float[]{ 47.5f, 47.5f }, 2,  1), 
        new Object(new float[]{ 48.44f, 48.44f }, 2,  1), 
        new Object(new float[]{ 48.67f, 48.67f }, 2,  1), 
        new Object(new float[]{ 48.91f, 48.91f }, 2,  1), 
        new Object(new float[]{ 49.14f, 49.14f }, 2,  1), 
        new Object(new float[]{ 50.55f, 50.55f }, 2,  1), 
        new Object(new float[]{ 51.49f, 51.49f }, 2,  1), 
        new Object(new float[]{ 52.42f, 52.42f }, 2,  1), 
        new Object(new float[]{ 53.36f, 53.36f }, 2,  1), 
        new Object(new float[]{ 54.07f, 54.07f }, 2,  1), 
        new Object(new float[]{ 55.0f, 55.0f }, 2,  1), 
        new Object(new float[]{ 55.94f, 55.94f }, 2,  1), 
        new Object(new float[]{ 56.88f, 56.88f }, 2,  1), 
        new Object(new float[]{ 57.82f, 57.82f }, 2,  1), 
        new Object(new float[]{ 62.5f, 62.5f }, 2,  1), 
        new Object(new float[]{ 64.38f, 64.38f }, 2,  1), 
        new Object(new float[]{ 66.25f, 66.25f }, 2,  1), 
        new Object(new float[]{ 70.0f, 70.0f }, 2, 1), 
        new Object(new float[]{ 71.88f, 71.88f }, 2, 1), 
        new Object(new float[]{ 73.75f, 73.75f }, 2, 1), 

        // fourth lane
        new Object(new float[]{ 20.32f, 20.32f }, 1, 1), 
        new Object(new float[]{ 24.07f, 24.07f }, 1, 1), 
        new Object(new float[]{ 27.82f, 27.82f }, 1, 1), 
        new Object(new float[]{ 32.5f, 32.5f }, 1, 1), 
        new Object(new float[]{ 34.38f, 34.38f }, 1, 1), 
        new Object(new float[]{ 36.25f, 36.25f }, 1, 1), 
        new Object(new float[]{ 40.0f, 40.0f }, 2, 1), 
        new Object(new float[]{ 41.88f, 41.88f }, 2, 1), 
        new Object(new float[]{ 43.75f, 43.75f }, 2, 1), 
        new Object(new float[]{ 46.57f, 46.57f }, 1, 1), 
        new Object(new float[]{ 47.5f, 47.5f }, 1, 1), 
        new Object(new float[]{ 48.44f, 48.44f }, 1, 1), 
        new Object(new float[]{ 48.67f, 48.67f }, 1, 1), 
        new Object(new float[]{ 48.91f, 48.91f }, 1, 1), 
        new Object(new float[]{ 49.14f, 49.14f }, 1, 1), 
        new Object(new float[]{ 51.02f, 51.02f }, 1, 1), 
        new Object(new float[]{ 51.96f, 51.96f }, 1, 1), 
        new Object(new float[]{ 52.89f, 52.89f }, 1, 1), 
        new Object(new float[]{ 53.83f, 53.83f }, 1, 1), 
        new Object(new float[]{ 54.07f, 54.07f }, 1, 1), 
        new Object(new float[]{ 55.94f, 55.94f }, 1, 1), 
        new Object(new float[]{ 57.82f, 57.82f }, 1, 1), 
        new Object(new float[]{ 61.57f, 61.57f }, 1, 1), 
        new Object(new float[]{ 62.5f, 62.5f }, 1, 1), 
        new Object(new float[]{ 65.32f, 65.32f }, 1, 1), 
        new Object(new float[]{ 66.25f, 66.25f }, 1, 1), 
        new Object(new float[]{ 70.0f, 70.0f }, 1, 1), 
        new Object(new float[]{ 71.88f, 71.88f }, 1, 1), 
        new Object(new float[]{ 73.75f, 73.75f }, 1, 1), 

    };


    private void Awake()
    {
        noteHandler = note.GetComponent<Note>();
        playerHandler = player.GetComponent<PlayerControl>();
        // sort the notes by time
        var watch = Stopwatch.StartNew();
        Array.Sort(objArr, new ObjectComparer());
        var elabpsedMs = watch.ElapsedMilliseconds;
        Debug.Log("Sorting took " + elabpsedMs + "ms");
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
            yield return new WaitForSeconds(objArr[ind].TimeStamp[0]-objArr[ind-1].TimeStamp[0]);
            xLen = noteHandler.transform.localScale.x;

            float yPos = objArr[ind].Pos switch
            {
                0 => 4,
                1 => 2,
                2 => -2,
                _ => -4,
            };

            if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
            {
                xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

            Debug.Log("obj: " + ind + " " + objArr[ind].TimeStamp[0] + " " + objArr[ind].TimeStamp[1] + " " + objArr[ind].Pos + " " + objArr[ind].Type);
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
                case 4:
                    newPlatform = Instantiate(platform, spawnPos, Quaternion.identity);
                    var newPlatform_ = newPlatform.GetComponent<platform>();
                    newPlatform_.SetLength(xLen);
                    Destroy(newPlatform, 3f/2.46f*xLen);
                    break;
            }
            ind++;
        }
    }
}

class ObjectComparer: IComparer{
    public int Compare(object x, object y){
        if (((Object)x).TimeStamp[0] > ((Object)y).TimeStamp[0])
            return 1;
        else if (((Object)x).TimeStamp[0] < ((Object)y).TimeStamp[0])
            return -1;
        else
            return 0;
    }
}