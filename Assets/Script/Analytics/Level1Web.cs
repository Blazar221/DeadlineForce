using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Level1Web : MonoBehaviour
{
    [SerializeField] private string formURL;
    public static Level1Web instance;
    private GameObject player;
    private long _sessionId;
    private float _playtime;
    private int _bossHealth;
    private int _playerHealth;
    private int _regularAttack = 0;
    private int _bonusAttack = 0;
    private int _reducedAttack = 0;
    private string subQuests = "";

    private static readonly float[] _diamondStart = {2f, 14f, 25.67f, 36.47f, 46.07f, 55.67f, 64.07f};
    private static readonly float[] _diamondEnd = {11.75f, 22.25f, 31.07f, 43.37f, 52.67f, 61.07f, 74.27f};
    private static readonly float[] _emissionStart = {12f, 22.5f, 31.5f, 43.5f, 53f, 61.5f, 69.5f, 74.5f};
    private static readonly float[] _emissionEnd = {13.7f, 25f, 34f, 46f, 55.5f, 63f, 72.5f, 82f};
    private string pathOption = "";
    private float[,] onPathTime = new float[7,2];
    private int[] emission = new int[8];
    private string emis = "";
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
        if(Input.GetKeyDown(KeyCode.K)){
            for(int i=0; i<emission.Length; i++){
                if(currentTime <= _emissionEnd[i] && currentTime >= _emissionStart[i]){
                    emission[i] = 1;
                    break;
                }
            }
        }
        
    }

    private void UpdatePathTime(int a, float pos){
        switch (pos>0){
            case true:
                onPathTime[a, 0] += 1f * Time.deltaTime;
                break;
            case false:
                onPathTime[a, 1] += 1f * Time.deltaTime;
                break;
        }
    }

    public void UpdateQuest(string index, string desc, string complete){
        string quest = index + ',' + desc + ',' + complete + '|';
        subQuests += quest;
    }

    public void UpdateCounter(int[] counter){
        _regularAttack = counter[0];
        _bonusAttack = counter[1];
        _reducedAttack = counter[2];
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

    private void UpdateEmission(){
        foreach(int e in emission){
            emis += e.ToString() + "|";
        }
    }

    public void Send(){
        _playtime = Time.timeSinceLevelLoad;
        _bossHealth = (Boss.instance != null)?Boss.instance.bossHealth:0;
        _playerHealth = PlayerControl.instance.currentHealth;
        CalculatePath();
        UpdateEmission();
        StartCoroutine(Post(_sessionId.ToString(), _playtime.ToString(), _bossHealth.ToString(), _playerHealth.ToString(),
                        subQuests, _regularAttack.ToString(), _bonusAttack.ToString(), _reducedAttack.ToString(), 
                        pathOption, emis));
    }

    private IEnumerator Post(string sessionId, string playtime, string bossHealth, string playerHealth,
                                string quests, string regular, string bonus, string reduced, 
                                string path, string emission){
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1331702699", sessionId);
        form.AddField("entry.2001195189", playtime);
        form.AddField("entry.895654878", bossHealth);
        form.AddField("entry.1415939753", playerHealth);
        form.AddField("entry.1101869322", quests);
        form.AddField("entry.476266214", regular);
        form.AddField("entry.1873070447", bonus);
        form.AddField("entry.663020293", reduced);
        form.AddField("entry.1686257065", emission);

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
                Debug.Log("Level1Web Form upload complete");
            }
        }
    }
}
