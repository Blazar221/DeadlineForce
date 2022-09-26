using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TextTutorial : MonoBehaviour
{

    ///private Text shortNoteInstruction;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI shortNoteInstruction;
    [SerializeField] private TextMeshProUGUI longNoteInstruction;
    [SerializeField] private TextMeshProUGUI gravSwitchInstruction;
    [SerializeField] private TextMeshProUGUI blockInstruction;
    [SerializeField] private TextMeshProUGUI finishInstruction;
    
    private PlayerControl playerControl;
    
    private bool shortNoteLearned = false;
    private bool longNoteLearned = false;
    private bool gravSwitchLearned = false;
    private bool blockLearned = false;

    private float firstShortNoteTime = 2f;
    private float firstLongNoteTime = 2.75f;
    private float firstGravSwitchTime = 5.5f;
    private float disableGravSwitchTime = 6.00f;
    private float firstBlockTime = 7.5f;
    private float finishTime = 8.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        //shortNoteInstruction=GetComponent<Text>();
        shortNoteInstruction.enabled = false;
        longNoteInstruction.enabled = false;
        gravSwitchInstruction.enabled = false;
        blockInstruction.enabled = false;
        finishInstruction.enabled = false;
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

        if(!shortNoteLearned && Time.timeSinceLevelLoad >= firstShortNoteTime){
            Time.timeScale = 0f;
            shortNoteInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.P)) {
                shortNoteLearned = true;
                shortNoteInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!longNoteLearned && Time.timeSinceLevelLoad >= firstLongNoteTime){
            Time.timeScale = 0f;
            longNoteInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.O)) {
                longNoteLearned = true;
                longNoteInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!gravSwitchLearned && Time.timeSinceLevelLoad >= firstGravSwitchTime){
            Time.timeScale = 0f;
            gravSwitchInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                gravSwitchLearned = true;
                gravSwitchInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!blockLearned && Time.timeSinceLevelLoad >= firstBlockTime){
            Time.timeScale = 0f;
            blockInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.P)) {
                blockLearned = true;
                blockInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(Time.timeSinceLevelLoad >= disableGravSwitchTime){
            playerControl = Player.GetComponent<PlayerControl>();
            playerControl.canChangeGravity = false;
        }
        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene("LevelMenu");
            }
        }
    }

}
