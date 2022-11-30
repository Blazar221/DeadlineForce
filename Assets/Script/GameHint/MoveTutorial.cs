using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoveTutorial : MonoBehaviour
{

    ///private Text shortNoteInstruction;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI firstmoveInstruction;
    [SerializeField] private TextMeshProUGUI Switch10Instruction;
    [SerializeField] private TextMeshProUGUI Switch01Instruction;
    [SerializeField] private TextMeshProUGUI finishInstruction;
    [SerializeField] private TextMeshProUGUI Switch12Instruction;
    [SerializeField] private TextMeshProUGUI Switch21Instruction; 
    [SerializeField] private TextMeshProUGUI Switch23Instruction;
    [SerializeField] private TextMeshProUGUI Switch30Instruction;
    [SerializeField] private TextMeshProUGUI Switch03Instruction;
    [SerializeField] private TextMeshProUGUI Switch32Instruction;
    [SerializeField] private TextMeshProUGUI WarningInstruction;
    [SerializeField] private TextMeshProUGUI ContinueInstruction;
    [SerializeField] private TextMeshProUGUI UpDownInstruction;
    [SerializeField] private Image LeftArrow;
   
    
    private PlayerMovement playerMovement;
    
    private bool firstmoveLearned = false;
    private bool Switch10Learned = false;
    private bool Switch01Learned = false;
    private bool Switch12Learned = false;
    private bool Switch23Learned = false;
    private bool Switch30Learned = false;
    private bool Switch03Learned = false;
    private bool Switch32Learned = false;
    private bool Switch21Learned = false;
    private bool UpdownLearned = false;

    private float firstmoveTime = 2f;
    private float Switch10Time = 3f;
    private float Switch01Time = 4f;
    private float Switch12Time = 5f;
    private float Switch23Time = 6f;
    private float Switch30Time = 7f;
    private float Switch03Time = 8.3f;
    private float Switch32Time = 11f;
    private float Switch21Time = 12f;
    
    private float UpDownTime = 13f;
    
    private float finishTime = 14f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        firstmoveInstruction.enabled = false;
        Switch10Instruction.enabled = false;
        Switch01Instruction.enabled = false;
        Switch12Instruction.enabled = false;
        Switch21Instruction.enabled = false;
        Switch23Instruction.enabled = false;
        Switch30Instruction.enabled = false;
        Switch03Instruction.enabled = false;
        Switch32Instruction.enabled = false;
        WarningInstruction.enabled = false;
        LeftArrow.enabled = false;
        ContinueInstruction.enabled = false;
        UpDownInstruction.enabled = false;
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
        // if (Time.timeSinceLevelLoad < freemoveTime) {
        //     playerControl = Player.GetComponent<PlayerControl>();
        //     playerControl.canChangeGravity = false;
        // }else{
        //     playerControl = Player.GetComponent<PlayerControl>();
        //     playerControl.canChangeGravity = true;
        // }

       if(!firstmoveLearned && Time.timeSinceLevelLoad >= firstmoveTime){
            Time.timeScale = 0f;
            firstmoveInstruction.enabled = true;
            ContinueInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                firstmoveLearned = true;
                firstmoveInstruction.enabled = false;
                ContinueInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch10Learned && Time.timeSinceLevelLoad >= Switch10Time){
            Time.timeScale = 0f;
            Switch10Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.W)) {
                Switch10Learned = true;
                Switch10Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch01Learned && Time.timeSinceLevelLoad >= Switch01Time){
            Time.timeScale = 0f;
            Switch01Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.S)) {
                Switch01Learned = true;
                Switch01Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch12Learned && Time.timeSinceLevelLoad >= Switch12Time){
            Time.timeScale = 0f;
            Switch12Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.S)) {
                Switch12Learned = true;
                Switch12Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch23Learned && Time.timeSinceLevelLoad >= Switch23Time){
            Time.timeScale = 0f;
            Switch23Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.S)) {
                Switch23Learned = true;
                Switch23Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch30Learned && Time.timeSinceLevelLoad >= Switch30Time){
            Time.timeScale = 0f;
            Switch30Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.S)) {
                Switch30Learned = true;
                Switch30Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch03Learned && Time.timeSinceLevelLoad >= Switch03Time){
            Time.timeScale = 0f;
            WarningInstruction.enabled = true;
            LeftArrow.enabled = true;
            Switch03Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.W)) {
                Switch03Learned = true;
                Switch03Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch32Learned && Time.timeSinceLevelLoad >= Switch32Time){
            Time.timeScale = 0f;
            WarningInstruction.enabled = false;
            LeftArrow.enabled = false;
            Switch32Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.W)) {
                Switch32Learned = true;
                Switch32Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!Switch21Learned && Time.timeSinceLevelLoad >= Switch21Time){
            Time.timeScale = 0f;
            Switch21Instruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.W)) {
                Switch21Learned = true;
                Switch21Instruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!UpdownLearned && Time.timeSinceLevelLoad >= UpDownTime){
            Time.timeScale = 0f;
            UpDownInstruction.enabled = true;
            ContinueInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                UpdownLearned = true;
                UpDownInstruction.enabled = false;
                ContinueInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                Time.timeScale = 1f;
                SceneManager.LoadScene("LevelMenu");
            }
        }
    }

}
