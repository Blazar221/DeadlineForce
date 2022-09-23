using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendAnalytics : MonoBehaviour
{
    [SerializeField] private string formURL;

    public static SendAnalytics instance;
    private long _sessionId;
    private float _playtime;
    private int _score;

    private void Awake()
    {
        instance = this;
        _sessionId = DateTime.Now.Ticks;
    }

    public void Send(int point)
    {
        _playtime = Time.timeSinceLevelLoad;
        _score = point;
        StartCoroutine(Post(_sessionId.ToString(), _playtime.ToString(), _score.ToString()));
    }

    private IEnumerator Post(string sessionId, string playtime, string score)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.331597584", sessionId);
        form.AddField("entry.1063281025", playtime);
        form.AddField("entry.298718684", score);

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