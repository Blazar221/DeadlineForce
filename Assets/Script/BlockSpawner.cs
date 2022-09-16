using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameObject block;

    private GameObject newBlock;
    private SpriteRenderer rend;
    private Vector3 spawnPos;

    private float[] time = new float[]
    {
        0, 0.31f, 1.27f, 2.24f, 3.19f, 4.15f, 5.12f, 6.07f, 7.03f, 8.00f, 8.95f, 9.91f, 10.87f, 11.82f, 12.79f, 13.75f, 14.71f, 15.67f, 16.63f, 17.60f, 18.55f, 19.52f, 20.47f, 21.44f, 22.39f, 23.35f, 24.31f, 25.27f
    };

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewBlock(time));
    }

    private IEnumerator SpawnNewBlock(float[] timeArr)
    {
        int len = timeArr.Length, ind = 0;
        while (ind < len - 1)
        {
            yield return new WaitForSeconds(timeArr[ind+1]-timeArr[ind]);
            spawnPos = new Vector3(13, -4, 0);
            newBlock = Instantiate(block, spawnPos, Quaternion.identity);
            rend = newBlock.GetComponent<SpriteRenderer>();
            ind++;
        }

        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
