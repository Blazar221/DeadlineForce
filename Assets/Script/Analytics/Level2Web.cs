using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Level2Web : MonoBehaviour
{
    [SerializeField] private string formURL;

    public static Level2Web instance;
    private GameObject player;
    private long _sessionId;
    private string[] pathOption = new string[4];
    private int _perfect;
    private int _good;

    private static readonly float[] platformStart = { 0.0f, 18.5f, 36.00f, 53.50f };
    private static readonly float[] platformEnd = { 17.45f, 34.909f, 52.00f, 70.00f };
    private float[,] onPathTime = new float[4, 4];
    private float playerPos;
    private float currentTime;


    void Start()
    {
        instance = this;
        _sessionId = DateTime.Now.Ticks;
        for (int i = 0; i < onPathTime.GetLength(0); i++)
            for (int j = 0; j < onPathTime.GetLength(1); j++)
                onPathTime[i, j] = 0;
        for (int i = 0; i < pathOption.GetLength(0); i++)
            pathOption[i] = "";
        player = GameObject.Find("DinasourRunner");
    }

    void Update()
    {
        currentTime = Time.timeSinceLevelLoad;
        playerPos = Mathf.Round(player.transform.position.y);
        if (currentTime <= platformEnd[0])
            UpdatePathTime(0, playerPos);
        else if (currentTime >= platformStart[1] && currentTime <= platformEnd[1])
            UpdatePathTime(1, playerPos);
        else if (currentTime >= platformStart[2] && currentTime <= platformEnd[2])
            UpdatePathTime(2, playerPos);
        else if (currentTime >= platformStart[3] && currentTime <= platformEnd[3])
            UpdatePathTime(3, playerPos);
    }

    private void UpdatePathTime(int a, float pos)
    {
        switch (pos)
        {
            case 4:
                onPathTime[a, 0] += 1f * Time.deltaTime;
                break;
            case 1:
                onPathTime[a, 1] += 1f * Time.deltaTime;
                break;
            case -1:
                onPathTime[a, 2] += 1f * Time.deltaTime;
                break;
            case -4:
                onPathTime[a, 3] += 1f * Time.deltaTime;
                break;
        }
    }

    public void Send()
    {
        for (int i = 0; i < pathOption.GetLength(0); i++)
        {
            float platformLength = platformEnd[i] - platformStart[i];
            for (int j = 0; j < onPathTime.GetLength(1); j++)
                if (onPathTime[i, j] >= platformLength * 0.3)
                    pathOption[i] += j.ToString();
        }
        _perfect = GameOverScreen.instance.GetPerfect();
        _good = GameOverScreen.instance.GetGood();

        StartCoroutine(Post(_sessionId.ToString(), pathOption, _perfect.ToString(), _good.ToString()));
    }

    private IEnumerator Post(string sessionId, string[] pathOption, string perfect, string good)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1543633014", sessionId);
        form.AddField("entry.1130630893", pathOption[0]);
        form.AddField("entry.1056179249", pathOption[1]);
        form.AddField("entry.702910713", pathOption[2]);
        form.AddField("entry.653905903", pathOption[3]);
        form.AddField("entry.281356619", perfect);
        form.AddField("entry.2094259220", good);

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
                Debug.Log("Form upload complete");
            }
        }
    }
}