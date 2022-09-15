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
        0, 0.11609977324263039f, 1.8343764172335602f, 10.332879818594105f, 10.820498866213152f, 11.284897959183674f,
        11.77251700680272f, 12.097596371882085f,
        12.72453514739229f, 13.212154195011339f, 13.699773242630386f, 14.02485260770975f, 14.651791383219955f,
        15.139410430839002f, 15.603809523809524f, 15.928888888888888f, 16.579047619047618f,
        17.04344671201814f, 17.531065759637187f, 18.018684807256236f, 18.483083900226756f, 18.83138321995465f,
        19.458321995464853f, 19.783401360544218f, 20.410340136054423f, 20.897959183673468f,
        21.36235827664399f, 21.687437641723356f, 22.19827664399093f, 22.66267573696145f, 23.289614512471655f,
        23.61469387755102f, 24.264852607709752f, 24.589931972789117f, 25.21687074829932f, 25.541950113378686f,
        26.16888888888889f, 26.51718820861678f, 27.144126984126984f, 27.445986394557824f, 28.096145124716553f,
        28.560544217687074f, 29.048163265306123f, 29.373242630385487f, 30.023401360544216f, 30.34848072562358f
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
            spawnPos = new Vector3(13, -3, 0);
            newBlock = Instantiate(block, spawnPos, Quaternion.identity);
            rend = newBlock.GetComponent<SpriteRenderer>();
            rend.color = new Color(Random.Range(0,2), Random.Range(0,2), Random.Range(0,2), 1f);
            print("at time" + timeArr[ind+1]);
            ind++;
        }

        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
