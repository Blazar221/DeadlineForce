using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravSwitchSpawnerUp : MonoBehaviour
{
    [SerializeField] 
    private GameObject gravUp;

    private GameObject newGravSwitch;
    private SpriteRenderer rend;
    private Vector3 spawnPos;

    private float[] time = new float[]
    {
        0, 21.3f, 40.8f, 60.6f, 78.9f, 98.4f, 115.5f, 135.3f
    };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnnewGravSwitch(time));
    }

    private IEnumerator SpawnnewGravSwitch(float[] timeArr)
    {
        int len = timeArr.Length, ind = 1;
        while (ind < len)
        {
            yield return new WaitForSeconds(timeArr[ind]-timeArr[ind-1]);
            Debug.Log("Switch Up:" + time[ind]);
            spawnPos = new Vector3(13, 4, 0);
            newGravSwitch = Instantiate(gravUp, spawnPos, Quaternion.identity);
            rend = newGravSwitch.GetComponent<SpriteRenderer>();
            ind++;
        }

        
    }
}
