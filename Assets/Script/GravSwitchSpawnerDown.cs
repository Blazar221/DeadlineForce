using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravSwitchSpawnerDown : MonoBehaviour
{
    [SerializeField] 
    private GameObject gravDown;

    private GameObject newGravSwitch;
    private SpriteRenderer rend;
    private Vector3 spawnPos;

    private float[] time = new float[]
    {
        0, 12.3f, 30.6f, 48.6f, 69.0f, 88.2f, 107.4f, 126.6f
    };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewBlock(time));
    }

    private IEnumerator SpawnNewBlock(float[] timeArr)
    {
        int len = timeArr.Length, ind = 1;
        while (ind < len)
        {
            yield return new WaitForSeconds(timeArr[ind]-timeArr[ind-1]);
            Debug.Log("Switch Down:" + time[ind]);
            spawnPos = new Vector3(13, -4, 0);
            newGravSwitch = Instantiate(gravDown, spawnPos, Quaternion.identity);
            rend = newGravSwitch.GetComponent<SpriteRenderer>();
            ind++;
        }

        
    }
}
