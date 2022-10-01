using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Editor : MonoBehaviour
{
    [SerializeField] private string formURL;
    public static Level1Editor instance;
    private long _sessionId;
    private float _playtime;

    private static readonly double[] checkpoint = { 22.77, 32.07, 41.37, 50.67, 53.67, 57.42, 60.87, 70.47 };
    private int[] hit = new int[9];
    private bool[] hasUpdate = new bool[9];
    private int totalHit = 0;
    private int lastTotalHit = 0;
    private float currentTime;

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
        currentTime = Time.timeSinceLevelLoad;
        totalHit = (int)ScoreManager.instance.GetTotalHit();
        if (!hasUpdate[0] && currentTime >= checkpoint[0])
        {
            hit[0] = totalHit;
            hasUpdate[0] = true;
        }
        else if(!hasUpdate[1] && currentTime >= checkpoint[1])
        {
            hit[1] = totalHit - lastTotalHit;
            lastTotalHit = totalHit;
            hasUpdate[1] = true;
        }
        else if (!hasUpdate[2] && currentTime >= checkpoint[2])
        {
            hit[2] = totalHit - lastTotalHit;
            lastTotalHit = totalHit;
            hasUpdate[2] = true;
        }
        else if (!hasUpdate[3] && currentTime >= checkpoint[3])
        {
            hit[3] = totalHit - lastTotalHit;
            lastTotalHit = totalHit;
            hasUpdate[3] = true;
        }
        else if (!hasUpdate[4] && currentTime >= checkpoint[4])
        {
            hit[4] = totalHit - lastTotalHit;
            lastTotalHit = totalHit;
            hasUpdate[4] = true;
        }
        else if (!hasUpdate[5] && currentTime >= checkpoint[5])
        {
            hit[5] = totalHit - lastTotalHit;
            lastTotalHit = totalHit;
            hasUpdate[5] = true;
        }
        else if (!hasUpdate[6] && currentTime >= checkpoint[6])
        {
            hit[6] = totalHit - lastTotalHit;
            lastTotalHit = totalHit;
            hasUpdate[6] = true;
        }
        else if (!hasUpdate[7] && currentTime >= checkpoint[7])
        {
            hit[7] = totalHit - lastTotalHit;
            lastTotalHit = totalHit;
            hasUpdate[7] = true;
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
        form.AddField("entry.1448716951", sessionId);
        form.AddField("entry.1190083204", playtime);
        form.AddField("entry.39244634", hit[0].ToString());
        form.AddField("entry.349395048", hit[1].ToString());
        form.AddField("entry.1189285148", hit[2].ToString());
        form.AddField("entry.17082960", hit[3].ToString());
        form.AddField("entry.244455843", hit[4].ToString());
        form.AddField("entry.1535585891", hit[5].ToString());
        form.AddField("entry.541977426", hit[6].ToString());
        form.AddField("entry.98738214", hit[7].ToString());
        form.AddField("entry.1750407743", hit[8].ToString());

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
