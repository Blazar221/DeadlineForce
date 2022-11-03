using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectTutorial : MonoBehaviour
{

    ///private Text shortNoteInstruction;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI shortNoteInstruction;
    [SerializeField] private TextMeshProUGUI longNoteInstruction;
    [SerializeField] private TextMeshProUGUI upSwitchInstruction;
    [SerializeField] private TextMeshProUGUI downSwitchInstruction;
    [SerializeField] private TextMeshProUGUI differentcolorInstruction;
    [SerializeField] private TextMeshProUGUI finishInstruction;
    [SerializeField] private TextMeshProUGUI TripleInstruction;
    [SerializeField] private TextMeshProUGUI passdownInstruction;
    [SerializeField] private TextMeshProUGUI passupInstruction;
    [SerializeField] private TextMeshProUGUI gemcollectionInstruction;
    [SerializeField] private TextMeshProUGUI PressJInstruction;
    [SerializeField] private TextMeshProUGUI MissionInstruction;
    [SerializeField] private TextMeshProUGUI startcollectInstruction;
    
    private PlayerControl playerControl;
    
    private bool shortNoteLearned = false;
    private bool tripleLearned1 = false;
    private bool tripleLearned2 = false;
    private bool tripleLearned3 = false;
    private bool differentcolorLearned = false;
    private bool gemcollectionLearned = false;
    
    private bool longNoteLearned = false;
    private bool upSwitchLearned = false;
    private bool downSwitchLearned = false;
    private bool blockLearned = false;
    private bool passdownLearned = false;
    private bool passupLearned = false;
    private bool startcollectLearned = false;

    private float firstShortNoteTime = 2f;
    private float t1ShortNoteTime = 3f;
    private float t2ShortNoteTime = 4f;
    private float t3ShortNoteTime = 5f;
    private float differentcolorShortNoteTime = 6f;
    
    private float firstLongNoteTime = 7f;
    private float firstgemcollectionTime = 10f;
    private float firstUpSwitchTime = 12f;
    private float firstDownSwitchTime = 14f;
    private float passdownSwitchTime = 16f;
    private float passupSwitchTime = 18f;
    private float startcollectTime = 18.5f;
    private float finishTime = 9f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControl = Player.GetComponent<PlayerControl>();
        playerControl.canChangeGravity = false;
        shortNoteInstruction.enabled = false;
        longNoteInstruction.enabled = false;
        upSwitchInstruction.enabled = false;
        downSwitchInstruction.enabled = false;
        differentcolorInstruction.enabled = false;
        finishInstruction.enabled = false;
        TripleInstruction.enabled=false;
        passdownInstruction.enabled=false;
        passupInstruction.enabled=false;
        gemcollectionInstruction.enabled=false;
        PressJInstruction.enabled=false;
        MissionInstruction.enabled=false;
        startcollectInstruction.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
        // if(fading){
        //     Alpha = Alpha - (Time.deltaTime)/2;
        //     colorOri.a = Alpha;
        //     notification.color = colorOri;
        //     if(Alpha<=0){
        //         fading=false;
        //     }
        // }else{
        //     Alpha = Alpha + (Time.deltaTime)/2;
        //     colorOri.a = Alpha;
        //     notification.color = colorOri;
        //     if(Alpha>=1){
        //         fading=true;
        //     }
        // }
        playerControl.canChangeGravity = false;

        if(!shortNoteLearned && Time.timeSinceLevelLoad >= firstShortNoteTime){
            Time.timeScale = 0f;
            shortNoteInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                shortNoteLearned = true;
                shortNoteInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned1 && Time.timeSinceLevelLoad >= t1ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned1 = true;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned2 && Time.timeSinceLevelLoad >= t2ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned2 = true;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
         if(!tripleLearned3 && Time.timeSinceLevelLoad >= t3ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned3 = true;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }

        if(!differentcolorLearned && Time.timeSinceLevelLoad >= differentcolorShortNoteTime){
            Time.timeScale = 0f;
            differentcolorInstruction.enabled = true;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                differentcolorLearned = true;
                PressJInstruction.enabled = false;
                differentcolorInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
       
        
        if(!longNoteLearned && Time.timeSinceLevelLoad >= firstLongNoteTime){
            Time.timeScale = 0f;
            longNoteInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                longNoteLearned = true;
                longNoteInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }


        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.J)) {
                Time.timeScale = 1f;
                SceneManager.LoadScene("AttackTutorial");
            }
        }
    }

}