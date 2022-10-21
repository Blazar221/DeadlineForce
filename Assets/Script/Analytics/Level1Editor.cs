using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Editor : MonoBehaviour
{
    [SerializeField] private string formURL;
    public static Level1Editor instance;
    private GameObject player;
    private long _sessionId;
    private float _playtime;
    private int _bossHealth;
    private int _playerHealth;
    private string _subQuests = "";
    private string _attacks = "";

    private static readonly float[] _diamondStart = {2f, 25.67f, 34.07f, 46.07f, 55.67f, 62.87f, 72.47f};
    private static readonly float[] _diamondEnd = {22.25f, 31.07f, 43.37f, 52.67f, 60.17f, 69.47f, 81.47f};
    private string pathOption = "";
    private float[,] onPathTime = new float[7,2];
    private float playerPos;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
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
            case 2:
                onPathTime[a, 0] += 1f * Time.deltaTime;
                break;
            case -2:
                onPathTime[a, 1] += 1f * Time.deltaTime;
                break;
        }
    }

    public void UpdateQuest(string index, string desc, string complete){
        string quest = index + ',' + desc + ',' + complete + '|';
        _subQuests += quest;
    }

    public void UpdateAttack(int count){
        string attack = count.ToString() + "|";
        _attacks += attack;
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
        _bossHealth = (Boss.instance != null)?Boss.instance.bossHealth:0;
        _playerHealth = PlayerControl.instance.currentHealth;
        CalculatePath();
        StartCoroutine(Post(_sessionId.ToString(), _playtime.ToString(), _bossHealth.ToString(), _playerHealth.ToString(),
                        _subQuests, pathOption, _attacks));
    }

    private IEnumerator Post(string sessionId, string playtime, string bossHealth, string playerHealth,
                                string quests, string path, string attack){
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1340477315", sessionId);
        form.AddField("entry.1068428193", playtime);
        form.AddField("entry.742161712", bossHealth);
        form.AddField("entry.974960358", playerHealth);
        form.AddField("entry.1514832225", quests);
        form.AddField("entry.57498515", path);
        form.AddField("entry.1112563448", attack);

        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(formURL, form)){
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
