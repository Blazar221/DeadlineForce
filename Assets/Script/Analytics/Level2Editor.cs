using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Level2Editor : MonoBehaviour
{
    [SerializeField] private string formURL;
    public static Level2Editor instance;
    private GameObject player;
    private long _sessionId;
    private float _playtime;
    private int _bossHealth;
    private int _playerHealth;
    private string _subQuests = "";
    private string _timer = "";

    private static readonly float[] _diamondStart = {3.091f, 11.818f, 22.727f, 33.636f, 42.364f, 51.091f};
    private static readonly float[] _diamondEnd = {10.727f, 21.364f, 32.273f, 41.273f, 50.0f, 70.454f};
    private string pathOption = "";
    private float[,] onPathTime = new float[6,4];
    private float playerPos;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("lvl2 editor is running");
        instance = this;
        _sessionId = DateTime.Now.Ticks;
        player = GameObject.Find("DinasourRunner");
        for (int i = 0; i < onPathTime.GetLength(0); i++)
            for (int j = 0; j < onPathTime.GetLength(1); j++)
                onPathTime[i, j] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.timeSinceLevelLoad;
        playerPos = Mathf.Round(player.transform.position.y);
        for(int i=0; i<onPathTime.GetLength(0); i++){
            if(currentTime <= _diamondEnd[i] && currentTime >= _diamondStart[i]){
                UpdatePathTime(i, playerPos);
                break;
            }
        }
    }

    private void UpdatePathTime(int a, float pos){
        switch (pos){
            case 4:
                onPathTime[a, 0] += 1f * Time.deltaTime;
                break;
            case 2:
                onPathTime[a, 1] += 1f * Time.deltaTime;
                break;
            case -2:
                onPathTime[a, 2] += 1f * Time.deltaTime;
                break;
            case -4:
                onPathTime[a, 3] += 1f * Time.deltaTime;
                break;
        }
    }

    public void UpdateQuest(string index, string desc, string complete){
        string quest = index + ',' + desc + ',' + complete + '|';
        _subQuests += quest;
    }

    private void CalculatePath(){
        for (int i = 0; i < onPathTime.GetLength(0); i++)
        {
            float sectionLength = _diamondEnd[i] - _diamondStart[i];
            for (int j = 0; j < onPathTime.GetLength(1); j++)
                if (onPathTime[i, j] >= sectionLength * 0.3)
                    pathOption += j.ToString();
            pathOption += "|";
        }
    }

    public void Send(){
        _playtime = Time.timeSinceLevelLoad;
        _bossHealth = (BossUI.instance != null)?BossUI.instance.bossHealth:0;
        _playerHealth = PlayerHealth.Instance.currentHealth;
        _timer = CollectionController.Instance.time;
        CalculatePath();
        StartCoroutine(Post(_sessionId.ToString(), _playtime.ToString(), _bossHealth.ToString(), _playerHealth.ToString(),
                        _subQuests, pathOption, _timer));
    }

    private IEnumerator Post(string sessionId, string playtime, string bossHealth, string playerHealth,
                                string quests, string path, string timer){
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.595256420", sessionId);
        form.AddField("entry.1906048010", playtime);
        form.AddField("entry.813446464", bossHealth);
        form.AddField("entry.54375219", playerHealth);
        form.AddField("entry.1847098271", quests);
        form.AddField("entry.1082972053", path);
        form.AddField("entry.811315430", timer);

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
                Debug.Log("Level2Editor Form upload complete");
            }
        }
    }
}
