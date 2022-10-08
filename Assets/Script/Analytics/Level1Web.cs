using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Web : MonoBehaviour
{
    [SerializeField] private string formURL;
    public static Level1Web instance;
    private long _sessionId;
    private float _playtime;

    private static readonly double[] checkpoint = { 22.77, 32.07, 41.37, 50.67, 53.67, 57.42, 60.87, 70.47 };
    private int[] hit = new int[9];
    private bool[] hasUpdate = new bool[9];
    private int totalHit = 0;
    private int lastTotalHit = 0;
    private float currentTime;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _sessionId = DateTime.Now.Ticks;
        for(int i=0; i<hit.Length; i++)
        {
            hit[i] = -1;
            hasUpdate[i] = false;
        }
        totalHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(index<=7){
            currentTime = Time.timeSinceLevelLoad;
            totalHit = (int)ScoreManager.instance.GetTotalHit();
            
            if (currentTime >= checkpoint[index]){
                hit[index] = totalHit - lastTotalHit;
                lastTotalHit = totalHit;
                hasUpdate[index] = true;
                index++;
            }
        }
    }

    public void Send()
    {
        _playtime = Time.timeSinceLevelLoad;
        for (int i=0; i < hasUpdate.Length; i++)
        {
            if (!hasUpdate[i])
            {
                hit[i] = (int)ScoreManager.instance.GetTotalHit() - lastTotalHit;
                break;
            }
        }
        StartCoroutine(Post(_sessionId.ToString(), _playtime.ToString(), hit));
    }

    private IEnumerator Post(string sessionId, string playtime, int[] hit)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1331702699", sessionId);
        form.AddField("entry.2001195189", playtime);
        form.AddField("entry.895654878", hit[0].ToString());
        form.AddField("entry.1101869322", hit[1].ToString());
        form.AddField("entry.476266214", hit[2].ToString());
        form.AddField("entry.1873070447", hit[3].ToString());
        form.AddField("entry.663020293", hit[4].ToString());
        form.AddField("entry.1016687746", hit[5].ToString());
        form.AddField("entry.1412185331", hit[6].ToString());
        form.AddField("entry.1687017221", hit[7].ToString());
        form.AddField("entry.712051530", hit[8].ToString());

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(formURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Level1Score Form upload complete");
            }
        }
    }
}
