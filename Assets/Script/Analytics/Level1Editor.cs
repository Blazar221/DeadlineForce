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
    private string subQuests = "";

    // private static readonly double[] checkpoint = { 22.77, 32.07, 41.37, 50.67, 53.67, 57.42, 60.87, 70.47 };
    // private int[] hit = new int[9];
    // private bool[] hasUpdate = new bool[9];
    // private int totalHit = 0;
    // private int lastTotalHit = 0;
    //private float currentTime;
    //private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _sessionId = DateTime.Now.Ticks;
        // for(int i=0; i<hit.Length; i++)
        // {
        //     hit[i] = -1;
        //     hasUpdate[i] = false;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // if(index<=7){
        //     currentTime = Time.timeSinceLevelLoad;
        //     totalHit = (int)ScoreManager.instance.GetTotalHit();
            
        //     if (currentTime >= checkpoint[index]){
        //         hit[index] = totalHit - lastTotalHit;
        //         lastTotalHit = totalHit;
        //         hasUpdate[index] = true;
        //         index++;
        //     }
        // }
    }

    public void UpdateQuest(string index, string desc, string complete)
    {
        string quest = index + ',' + desc + ',' + complete + '|';
        subQuests += quest;
    }

    public void Send()
    {
        _playtime = Time.timeSinceLevelLoad;
        // for (int i=0; i < hasUpdate.Length; i++)
        // {
        //     if (!hasUpdate[i])
        //     {
        //         hit[i] = (int)ScoreManager.instance.GetTotalHit() - lastTotalHit;
        //         break;
        //     }
        // }
        StartCoroutine(Post(_sessionId.ToString(), _playtime.ToString(), subQuests));
    }

    private IEnumerator Post(string sessionId, string playtime, string quests)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1340477315", sessionId);
        form.AddField("entry.1068428193", playtime);
        form.AddField("entry.1514832225", quests);
        // form.AddField("entry.39244634", hit[0].ToString());
        // form.AddField("entry.349395048", hit[1].ToString());
        // form.AddField("entry.1189285148", hit[2].ToString());
        // form.AddField("entry.17082960", hit[3].ToString());
        // form.AddField("entry.244455843", hit[4].ToString());
        // form.AddField("entry.1535585891", hit[5].ToString());
        // form.AddField("entry.541977426", hit[6].ToString());
        // form.AddField("entry.98738214", hit[7].ToString());
        // form.AddField("entry.1750407743", hit[8].ToString());

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
                Debug.Log("Level1Editor Form upload complete");
            }
        }
    }
}
