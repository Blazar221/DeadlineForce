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
        0, 10.31f, 11.27f, 12.24f, 13.19f, 14.15f, 15.12f, 16.07f, 17.03f, 18.00f, 18.95f, 19.91f, 20.87f, 21.82f, 
        22.79f, 23.75f, 24.71f, 25.67f, 26.63f, 27.60f, 28.55f, 29.52f, 30.47f, 31.44f, 32.39f, 33.35f, 34.31f, 35.27f
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
