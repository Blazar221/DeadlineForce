using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    

    // private Object[] objArr = new Object[]
    // {
    //     new Object(new float[]{ 0.0f, 0.0f }, 0, 0), new Object(new float[]{ 0.0f, 22.5f }, 2, 4),
    //     new Object(new float[]{ 0.0f, 0.0f }, 0, 1),
    //     new Object(new float[]{ 0.75f, 0.75f },0, 1), new Object(new float[]{ 1.5f, 1.5f }, 0,1), 
    //     new Object(new float[]{ 2.25f, 2.25f }, 0,1), new Object(new float[]{ 3.0f, 3.0f }, 0,1), 
    //     new Object(new float[]{ 3.75f, 3.75f },0,1), new Object(new float[]{ 4.5f, 4.5f }, 0,1), 
    //     new Object(new float[]{ 5.25f, 5.25f }, 0,1), new Object(new float[]{5.5f, 5.5f}, 1, 3),
    //     new Object(new float[]{ 6.0f, 6.0f }, 0,1), 
    //     new Object(new float[]{ 6.75f, 6.75f }, 0,1), new Object(new float[]{ 7.5f, 7.5f }, 0,1), 
    //     new Object(new float[]{ 8.25f, 8.25f },0,1), new Object(new float[]{ 9.0f, 9.0f }, 0,1), 
    //     new Object(new float[]{ 9.75f, 9.75f }, 0,1), new Object(new float[]{ 10.5f, 10.5f }, 0,1), 
    //     new Object(new float[]{10.5f, 10.5f}, 1, 3), new Object(new float[]{ 11.25f, 11.25f }, 0,1), 
    //     new Object(new float[]{ 12.0f, 12.0f }, 0,1), new Object(new float[]{ 12.75f, 12.75f },0,1), 
    //     new Object(new float[]{ 13.5f, 13.5f }, 0,1), new Object(new float[]{ 14.25f, 14.25f }, 0,1), 
    //     new Object(new float[]{ 15.0f, 15.0f }, 0,1), new Object(new float[]{15.5f, 15.5f}, 1, 3),
    //     new Object(new float[]{ 15.75f, 15.75f }, 0,1), new Object(new float[]{ 16.5f, 16.5f },0,1), 
    //     new Object(new float[]{ 17.25f, 17.25f }, 0,1), new Object(new float[]{ 18.0f, 18.0f }, 0,1), 
    //     new Object(new float[]{ 18.75f, 18.75f }, 0,1), new Object(new float[]{ 19.5f, 19.5f }, 0,1), 
    //     new Object(new float[]{ 20.25f, 20.25f },0,1), new Object(new float[]{20.5f, 20.5f}, 1, 3),
    //     new Object(new float[]{ 21.0f, 22.35f }, 0,2), 
    //     new Object(new float[]{ 22.77f, 22.77f }, 0,0), new Object(new float[]{23.0f, 23.0f}, 0, 3),
    //     new Object(new float[]{ 23.67f, 23.67f }, 1,1),new Object(new float[]{ 24.0f, 31.5f }, 2, 4), 
    //     new Object(new float[]{ 24.27f, 24.27f }, 1,1), new Object(new float[]{ 24.87f, 24.87f },1,1), 
    //     new Object(new float[]{ 25.47f, 25.47f }, 1,1), new Object(new float[]{25.5f, 25.5f}, 0, 3),
    //     new Object(new float[]{ 26.07f, 26.07f }, 1,1), 
    //     new Object(new float[]{ 26.67f, 26.67f }, 1,1), new Object(new float[]{ 27.27f, 27.27f }, 1,1), 
    //     new Object(new float[]{ 27.87f, 27.87f },1,1), new Object(new float[]{28.0f, 28.0f}, 0, 3),
    //     new Object(new float[]{ 28.47f, 28.47f }, 1,1), 
    //     new Object(new float[]{ 29.07f, 29.07f }, 1,1), new Object(new float[]{ 29.6f, 31.64f }, 1,2), 
    //     new Object(new float[]{30.5f, 30.5f}, 0, 3),
    //     new Object(new float[]{ 32.07f, 32.07f }, 1,0), new Object(new float[]{ 33.0f, 41f }, 2, 4),
    //     new Object(new float[]{ 33.27f, 33.27f },0,1), 
    //     new Object(new float[]{ 33.87f, 33.87f }, 0,1), new Object(new float[]{ 34.4f, 41.0f }, 0,2), 
    //     new Object(new float[]{35.5f, 35.5f}, 1, 3), new Object(new float[]{38.0f, 38.0f}, 1, 3),
    //     new Object(new float[]{ 41.37f, 41.37f }, 0,0), new Object(new float[]{ 42.5f, 50.0f }, 2, 4),
    //     new Object(new float[]{ 41.6f, 43.64f }, 1,2), 
    //     new Object(new float[]{43.0f, 43.0f}, 0, 3),
    //     new Object(new float[]{ 44.05f, 46.05f },1,2), new Object(new float[]{ 46.47f, 46.47f }, 1,1), 
    //     new Object(new float[]{ 46.77f, 46.77f }, 1,1), new Object(new float[]{ 47.07f, 47.07f }, 1,1), 
    //     new Object(new float[]{ 47.37f, 47.37f }, 1,1), new Object(new float[]{ 47.67f, 47.67f },1,1), 
    //     new Object(new float[]{ 47.97f, 47.97f }, 1,1), new Object(new float[]{48.0f, 48.0f}, 0, 3),
    //     new Object(new float[]{ 48.27f, 48.27f }, 1,1), 
    //     new Object(new float[]{ 48.57f, 48.57f }, 1,1), new Object(new float[]{ 48.87f, 48.87f }, 1,1), 
    //     new Object(new float[]{ 49.17f, 49.17f },1,1), new Object(new float[]{ 49.47f, 49.47f }, 1,1), 
    //     new Object(new float[]{ 49.77f, 49.77f }, 1,1), new Object(new float[]{ 50.07f, 50.07f }, 1,1), 
    //     new Object(new float[]{ 50.37f, 50.37f }, 1,1), new Object(new float[]{ 50.67f, 50.67f },1,0), 
    //     new Object(new float[]{ 51.0f, 53.0f }, 2, 4),
    //     new Object(new float[]{ 51.2f, 53.24f }, 0,2), new Object(new float[]{52.5f, 52.5f}, 1, 3),
    //     new Object(new float[]{ 53.67f, 53.67f }, 0,0), new Object(new float[]{ 54.0f, 57.0f }, 2, 4),
    //     new Object(new float[]{ 53.9f, 55.64f }, 1,2), new Object(new float[]{56.0f, 56.0f}, 0, 3),
    //     new Object(new float[]{ 56.07f, 56.07f }, 1,1), 
    //     new Object(new float[]{ 56.3f, 57.14f },1,2), new Object(new float[]{ 57.42f, 57.42f }, 1,0), 
    //     new Object(new float[]{ 58.0f, 60.0f }, 2, 4),
    //     new Object(new float[]{ 58.47f, 58.47f }, 0,1), new Object(new float[]{59.0f, 59.0f}, 1, 3),
    //     new Object(new float[]{ 59.07f, 59.07f }, 0,1), 
    //     new Object(new float[]{ 59.67f, 59.67f }, 0,1), new Object(new float[]{ 60.87f, 60.87f },0,0), 
    //     new Object(new float[]{ 61.6f, 70.0f }, 2, 4),
    //     new Object(new float[]{ 62.07f, 62.07f }, 1,1), new Object(new float[]{62.5f, 62.5f}, 0, 3),
    //     new Object(new float[]{ 62.67f, 62.67f }, 1,1), 
    //     new Object(new float[]{ 63.27f, 63.27f }, 1,1), new Object(new float[]{ 63.87f, 63.87f }, 1,1), 
    //     new Object(new float[]{ 64.47f, 64.47f },1,1), new Object(new float[]{65.0f, 65.0f}, 0, 3),
    //     new Object(new float[]{ 65.07f, 65.07f }, 1,1), 
    //     new Object(new float[]{ 65.67f, 65.67f }, 1,1), new Object(new float[]{ 66.27f, 66.27f }, 1,1), 
    //     new Object(new float[]{ 66.87f, 66.87f }, 1,1), new Object(new float[]{ 67.47f, 67.47f },1,1), 
    //     new Object(new float[]{67.5f, 67.5f}, 0, 3),
    //     new Object(new float[]{ 68.0f, 70.04f }, 1,2), new Object(new float[]{ 70.47f, 70.47f }, 1,0), 
    //     new Object(new float[]{ 71.0f, 90.0f }, 2, 4),
    //     new Object(new float[]{ 71.67f, 71.67f }, 0,1), 
    //     new Object(new float[]{ 72.27f, 72.27f }, 0,1), new Object(new float[]{72.5f, 72.5f}, 1, 3),
    //     new Object(new float[]{ 73.25f, 79.75f },0,2), new Object(new float[]{75.0f, 75.0f}, 1, 3), 
    //     new Object(new float[]{77.5f, 77.5f}, 1, 3),
    // };

    private Object[] objArr = new Object[]
    {
        new Object(new float[]{ 0f, 0f }, 0,  1), // index zero doesn't spawn
new Object(new float[]{ 1.57f, 1.57f }, 0,  1), 
new Object(new float[]{ 3.44f, 3.44f }, 0,  1), 
new Object(new float[]{ 5.32f, 5.32f }, 0,  1), 
new Object(new float[]{ 7.19f, 7.19f }, 0,  1), 
new Object(new float[]{ 8.13f, 8.13f }, 0,  0),
new Object(new float[]{ 9.07f, 9.07f }, 1,  1), 
new Object(new float[]{ 10.94f, 10.94f }, 1,  1), 
new Object(new float[]{ 12.82f, 12.82f }, 1,  1), 
new Object(new float[]{ 14.69f, 14.69f }, 1,  1), 
new Object(new float[]{ 15.63f, 15.63f }, 1,  0),   // grav switch
new Object(new float[]{ 16.57f, 16.57f }, 0,  1), 
new Object(new float[]{ 18.44f, 18.44f }, 0,  1), 
new Object(new float[]{ 20.32f, 20.32f }, 0,  1), 
new Object(new float[]{ 22.19f, 22.19f }, 0,  1), 
new Object(new float[]{ 23.13f, 23.13f }, 0,  0),   // grav switch
new Object(new float[]{ 24.07f, 24.07f }, 1,  1), 
new Object(new float[]{ 25.0f, 25.0f }, 1,  1), 
new Object(new float[]{ 25.94f, 25.94f }, 1,  1), 
new Object(new float[]{ 26.88f, 26.88f }, 1,  1), 
new Object(new float[]{ 27.82f, 27.82f }, 1,  1), 
new Object(new float[]{ 28.75f, 28.75f }, 1,  1), 
new Object(new float[]{ 29.69f, 29.69f }, 1,  1), 
new Object(new float[]{ 30.63f, 30.63f }, 1,  0),   // grav switch
new Object(new float[]{ 31.57f, 31.57f }, 0,  1), 
new Object(new float[]{ 32.03f, 32.03f }, 0,  1), 
new Object(new float[]{ 32.5f, 32.5f }, 0,  1), 
new Object(new float[]{ 32.97f, 32.97f }, 0,  1), 
new Object(new float[]{ 33.44f, 33.44f }, 0,  1), 
new Object(new float[]{ 33.91f, 33.91f }, 0,  1), 
new Object(new float[]{ 34.38f, 34.38f }, 0,  1), 
new Object(new float[]{ 34.85f, 34.85f }, 0,  1), 
new Object(new float[]{ 35.32f, 35.32f }, 0,  1), 
new Object(new float[]{ 35.78f, 35.78f }, 0,  1), 
new Object(new float[]{ 36.25f, 36.25f }, 0,  1), 
new Object(new float[]{ 36.72f, 36.72f }, 0,  1), 
new Object(new float[]{ 37.19f, 37.19f }, 0,  1), 
new Object(new float[]{ 37.66f, 37.66f }, 0,  1), 
new Object(new float[]{ 38.13f, 38.13f }, 0,  1), 
new Object(new float[]{ 38.6f, 38.6f }, 0,  0),   // grav switch
new Object(new float[]{ 39.07f, 39.07f }, 1,  1), 
new Object(new float[]{ 39.3f, 39.3f }, 1,  1), 
new Object(new float[]{ 39.53f, 39.53f }, 1,  1), 
new Object(new float[]{ 39.77f, 39.77f }, 1,  1), 
new Object(new float[]{ 40.0f, 40.0f }, 1,  1), 
new Object(new float[]{ 40.2f, 40.2f }, 1,  1), 
new Object(new float[]{ 40.47f, 40.47f }, 1,  1), 
new Object(new float[]{ 40.71f, 40.71f }, 1,  1), 
new Object(new float[]{ 40.94f, 40.94f }, 1,  1), 
new Object(new float[]{ 41.17f, 41.17f }, 1,  1), 
new Object(new float[]{ 41.41f, 41.41f }, 1,  1), 
new Object(new float[]{ 41.64f, 41.64f }, 1,  1), 
new Object(new float[]{ 41.88f, 41.88f }, 1,  1), 
new Object(new float[]{ 42.11f, 42.11f }, 1,  1), 
new Object(new float[]{ 42.35f, 42.35f }, 1,  1), 
new Object(new float[]{ 42.58f, 42.58f }, 1,  1), 

new Object(new float[]{ 42.82f, 44.69f }, 1,  2),   // long note

new Object(new float[]{ 45.16f, 45.16f }, 1,  0),   // grav switch

new Object(new float[]{ 46.57f, 46.57f }, 0,  1), 
new Object(new float[]{ 46.8f, 46.8f }, 0,  1), 
new Object(new float[]{ 47.03f, 47.03f }, 0,  1), 
new Object(new float[]{ 47.27f, 47.27f }, 0,  1), 
new Object(new float[]{ 47.5f, 47.5f }, 0,  1), 
new Object(new float[]{ 47.97f, 47.97f }, 0,  1), 
new Object(new float[]{ 48.21f, 48.21f }, 0,  1), 
new Object(new float[]{ 48.44f, 48.44f }, 0,  1), 
new Object(new float[]{ 48.67f, 48.67f }, 0,  1), 
new Object(new float[]{ 48.91f, 48.91f }, 0,  1), 
new Object(new float[]{ 49.14f, 49.14f }, 0,  1), 
new Object(new float[]{ 49.38f, 49.38f }, 0,  1), 
new Object(new float[]{ 49.61f, 49.61f }, 0,  1), 
new Object(new float[]{ 49.85f, 49.85f }, 0,  1), 
new Object(new float[]{ 50.08f, 50.08f }, 0,  1), 
new Object(new float[]{ 50.55f, 50.55f }, 0,  1), 
new Object(new float[]{ 51.02f, 51.02f }, 0,  1), 
new Object(new float[]{ 51.49f, 51.49f }, 0,  1), 
new Object(new float[]{ 51.96f, 51.96f }, 0,  1), 
new Object(new float[]{ 52.42f, 52.42f }, 0,  1), 
new Object(new float[]{ 52.89f, 52.89f }, 0,  1), 
new Object(new float[]{ 53.36f, 53.36f }, 0,  1), 
new Object(new float[]{ 53.83f, 53.83f }, 0,  1), 
new Object(new float[]{ 54.3f, 54.3f }, 0,  1), 
new Object(new float[]{ 54.77f, 54.77f }, 0,  1), 
new Object(new float[]{ 55.24f, 55.24f }, 0,  1), 
new Object(new float[]{ 55.71f, 55.71f }, 0,  1), 
new Object(new float[]{ 56.17f, 56.17f }, 0,  1), 
new Object(new float[]{ 56.64f, 56.64f }, 0,  1), 
new Object(new float[]{ 57.11f, 57.11f }, 0,  1), 
new Object(new float[]{ 57.35f, 57.35f }, 0,  1), 
new Object(new float[]{ 57.58f, 57.58f }, 0,  1), 
new Object(new float[]{ 58.05f, 58.05f }, 0,  1), 
new Object(new float[]{ 58.52f, 58.52f }, 0,  1), 
new Object(new float[]{ 58.75f, 58.75f }, 0,  1), 
new Object(new float[]{ 58.99f, 58.99f }, 0,  1), 
new Object(new float[]{ 59.46f, 59.46f }, 0,  1), 
new Object(new float[]{ 59.92f, 59.92f }, 0,  1), 
new Object(new float[]{ 60.16f, 60.16f }, 0,  1), 
new Object(new float[]{ 60.63f, 60.63f }, 0,  1), 

new Object(new float[]{ 61.1f, 61.1f }, 0,  0),   // grav switch

new Object(new float[]{ 61.57f, 65.08f }, 1,  2),   // long note

new Object(new float[]{ 65.32f, 65.32f }, 1,  1), 
new Object(new float[]{ 65.78f, 65.78f }, 1,  1), 
new Object(new float[]{ 66.25f, 66.25f }, 1,  1), 
new Object(new float[]{ 66.72f, 66.72f }, 1,  1), 
new Object(new float[]{ 67.19f, 67.19f }, 1,  1), 
new Object(new float[]{ 67.66f, 67.66f }, 1,  1), 
new Object(new float[]{ 68.13f, 68.13f }, 1,  0),   // grav switch
new Object(new float[]{ 69.07f, 69.07f }, 0,  1), 
new Object(new float[]{ 69.53f, 69.53f }, 0,  1), 
new Object(new float[]{ 70.0f, 70.0f }, 0,  1), 
new Object(new float[]{ 70.47f, 70.47f }, 0,  1), 
new Object(new float[]{ 70.94f, 70.94f }, 0,  1), 
new Object(new float[]{ 71.41f, 71.41f }, 0,  1), 
new Object(new float[]{ 71.88f, 71.88f }, 0,  1), 
new Object(new float[]{ 72.35f, 72.35f }, 0,  1), 
new Object(new float[]{ 72.82f, 72.82f }, 0,  1), 
new Object(new float[]{ 73.28f, 73.28f }, 0,  1), 
new Object(new float[]{ 73.75f, 73.75f }, 0,  1), 
new Object(new float[]{ 74.22f, 74.22f }, 0,  1), 
new Object(new float[]{ 74.69f, 74.69f }, 0,  1), 
new Object(new float[]{ 75.39f, 75.39f }, 0,  1), 
new Object(new float[]{ 75.63f, 75.63f }, 0,  1), 
new Object(new float[]{ 76.1f, 76.1f }, 0,  1), 

    };


    private void Awake()
    {
        noteHandler = note.GetComponent<Note>();
        playerHandler = player.GetComponent<PlayerControl>();
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
                0 => -4,
                1 => 4,
                2 => 0,
            };

            if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
            {
                xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
            }

            spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

            Debug.Log("obj: " + ind + " " + objArr[ind].TimeStamp[0] + " " + objArr[ind].TimeStamp[1] + " " + objArr[ind].Pos);
            // start spawn
            switch (objArr[ind].Type)
            {
                case 0:
                    newItem = Instantiate(gravSwitch, spawnPos, Quaternion.identity);
                    Destroy(newItem, 3f);
                    if (objArr[ind].Pos == 1)
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
    // private IEnumerator SpawnNewPlatform()
    // {
    //     while (ind_platform < PlatformArr.Length/2)
    //     {
    //         yield return new WaitForSeconds(PlatformArr[ind_platform, 0]-PlatformArr[ind_platform-1, 0]);

    //         float yPos = 0;
            

    //         if (PlatformArr[ind_platform, 1] - PlatformArr[ind_platform, 0] != 0)
    //         {
    //             xLen = (PlatformArr[ind_platform, 1] - PlatformArr[ind_platform, 0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
    //         }

    //         spawnPos = new Vector3(playerX + noteHandler.speed * (2f / Time.fixedDeltaTime) + xLen / 2, yPos, 0);

    //         newPlatform = Instantiate(platform, spawnPos, Quaternion.identity);
    //         Debug.Log(Time.)
    //         var newPlatform_ = newPlatform.GetComponent<platform>();
    //         newPlatform_.SetLength(xLen);
    //         Destroy(newPlatform, 3f/2.46f*xLen);
            
            
    //         ind_platform++;
    //     }
    // }
}
