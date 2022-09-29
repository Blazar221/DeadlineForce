using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = Script.Object;

public class L1Spawner : MonoBehaviour
{
    [SerializeField] private GameObject gravSwitch;
    [SerializeField] private GameObject note;
    [SerializeField] private GameObject longNote;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject block;

    private GameObject newItem, newMine;
    private Vector3 spawnPos;
    private Block blockHandler;
    private Note noteHandler;
    private PlayerControl playerHadler;
    private float playerX, xLen;

    private Object[] objArr = new Object[]
    {
        new Object(new float[]{0.0f, 0.0f}, 0, 1), new Object(new float[]{1.5f, 1.5f}, 0, 1),
        new Object(new float[]{1.8f, 1.8f}, 0, 1), new Object(new float[]{2.1f, 2.1f}, 0, 1 ),
        new Object(new float[]{2.4f, 3.45f}, 0, 2), new Object(new float[]{3.6f, 3.6f}, 0, 1),
        new Object(new float[]{4.2f, 4.2f}, 0, 1), new Object(new float[]{4.5f, 5.85f}, 0, 2),
        new Object(new float[]{6.0f, 6.0f}, 0, 1), new Object(new float[]{6.6f, 6.6f}, 0, 1),
        new Object(new float[]{6.9f, 9.3f}, 0, 2), new Object(new float[]{9.6f, 10.65f}, 0, 2),
        new Object(new float[]{10.8f, 13.0f}, 0, 2), new Object(new float[]{13.5f, 13.5f}, 0, 0),
        new Object(new float[]{13.616f, 13.616f}, 0, 3), new Object(new float[]{13.8f, 13.8f}, 1, 1),
        new Object(new float[]{14.1f, 14.1f}, 1, 1), new Object(new float[]{14.4f, 15.45f}, 1, 2),
        new Object(new float[]{15.6f, 15.6f}, 1, 1), new Object(new float[]{15.606f, 15.606f}, 0, 3),
        new Object(new float[]{16.2f, 16.2f}, 1, 1), new Object(new float[]{16.5f, 17.85f}, 1, 2),
        new Object(new float[]{17.807f, 17.807f}, 0, 3), new Object(new float[]{18.0f, 18.0f}, 1, 1),
        new Object(new float[]{18.6f, 18.6f}, 1, 1), new Object(new float[]{18.9f, 21.3f}, 1, 2),
        new Object(new float[]{20.142f, 20.142f}, 0, 3), new Object(new float[]{21.6f, 22.65f}, 1, 2),
        new Object(new float[]{22.321f, 22.321f}, 0, 3), new Object(new float[]{22.8f, 25.2f}, 1, 2),
        new Object(new float[]{23.711f, 23.711f}, 0, 3), new Object(new float[]{25.8f, 25.8f}, 1, 0),
        new Object(new float[]{26.4f, 27.15f}, 0, 2), new Object(new float[]{27.3f, 28.2f}, 0, 2),
        new Object(new float[]{28.5f, 29.25f}, 0, 2), new Object(new float[]{28.068f, 28.068f}, 1, 3),
        new Object(new float[]{29.4f, 30.6f}, 0, 2), new Object(new float[]{29.671f, 29.671f}, 1, 3),
        new Object(new float[]{30.9f, 33.0f}, 0, 2), new Object(new float[]{31.652f, 31.652f}, 1, 3),
        new Object(new float[]{33.6f, 33.6f}, 0, 0), new Object(new float[]{34.2f, 34.2f}, 1, 1),
        new Object(new float[]{34.8f, 37.2f}, 1, 2), new Object(new float[]{35.728f, 35.728f}, 0, 3),
        new Object(new float[]{37.5f, 38.25f}, 1, 2), new Object(new float[]{38.4f, 38.4f}, 1, 0),
        new Object(new float[]{38.454f, 38.454f}, 1, 3), new Object(new float[]{39.0f, 39.0f}, 0, 1),
        new Object(new float[]{39.6f, 39.6f}, 0, 1), new Object(new float[]{40.2f, 45.3f}, 0, 2),
        new Object(new float[]{42.013f, 42.013f}, 1, 3), new Object(new float[]{43.902f, 43.902f}, 1, 3),
        new Object(new float[]{45.6f, 49.05f}, 0, 2), new Object(new float[]{45.686f, 45.686f}, 1, 3),
        new Object(new float[]{48.467f, 48.467f}, 1, 3), new Object(new float[]{49.5f, 49.5f}, 0, 0),
        new Object(new float[]{50.4f, 50.4f}, 1, 1), new Object(new float[]{51.0f, 51.0f}, 1, 1),
        new Object(new float[]{51.6f, 51.6f}, 1, 1), new Object(new float[]{51.875f, 51.875f}, 0, 3),
        new Object(new float[]{52.2f, 53.85f}, 1, 2), new Object(new float[]{53.674f, 53.674f}, 0, 3),
        new Object(new float[]{54.0f, 54.0f}, 1, 1), new Object(new float[]{54.6f, 57.15f}, 1, 2),
        new Object(new float[]{55.921f, 55.921f}, 0, 3), new Object(new float[]{57.6f, 61.5f}, 1, 2),
        new Object(new float[]{57.92f, 57.92f}, 0, 3), new Object(new float[]{60.082f, 60.082f}, 0, 3)
    };

    private void Awake()
    {
        noteHandler = note.GetComponent<Note>();
        playerHadler = player.GetComponent<PlayerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerX = playerHadler.transform.position.x;
        StartCoroutine(SpawnNewItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator SpawnNewItem()
    {
        var ind = 1;
        while (ind < objArr.Length)
        {
            yield return new WaitForSeconds(objArr[ind].TimeStamp[0]-objArr[ind-1].TimeStamp[0]);
            xLen = noteHandler.transform.localScale.x;

            float yPos = objArr[ind].Pos switch
            {
                0 => -4,
                1 => 4,
                _ => 0,
            };

            if (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0] != 0)
            {
                xLen = (objArr[ind].TimeStamp[1] - objArr[ind].TimeStamp[0]) * noteHandler.speed * (1 / Time.fixedDeltaTime);
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
                    Destroy(newItem, 3f/2.46f*xLen);
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
