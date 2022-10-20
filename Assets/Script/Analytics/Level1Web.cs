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
    private int _bossHealth;
    private int _regularAttack = 0;
    private int _bonusAttack = 0;
    private int _reducedAttack = 0;
    private string subQuests = "";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _sessionId = DateTime.Now.Ticks;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateQuest(string index, string desc, string complete)
    {
        string quest = index + ',' + desc + ',' + complete + '|';
        subQuests += quest;
    }

    public void UpdateCounter(int[] counter){
        _regularAttack = counter[0];
        _bonusAttack = counter[1];
        _reducedAttack = counter[2];
    }

    public void Send()
    {
        _playtime = Time.timeSinceLevelLoad;
        _bossHealth = (Boss.instance != null)?Boss.instance.bossHealth:0;
        StartCoroutine(Post(_sessionId.ToString(), _playtime.ToString(), _bossHealth.ToString(), 
                        subQuests, _regularAttack.ToString(), _bonusAttack.ToString(), _reducedAttack.ToString()));
    }

    private IEnumerator Post(string sessionId, string playtime, string bossHealth, 
                                string quests, string regular, string bonus, string reduced)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1331702699", sessionId);
        form.AddField("entry.2001195189", playtime);
        form.AddField("entry.895654878", bossHealth);
        form.AddField("entry.1101869322", quests);
        form.AddField("entry.476266214", regular);
        form.AddField("entry.1873070447", bonus);
        form.AddField("entry.663020293", reduced);

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
