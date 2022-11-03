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
    [SerializeField] private TextMeshProUGUI upSwitchInstruction;
    [SerializeField] private TextMeshProUGUI downSwitchInstruction;
    [SerializeField] private TextMeshProUGUI finishInstruction;
    [SerializeField] private TextMeshProUGUI passdownInstruction;
    [SerializeField] private TextMeshProUGUI passupInstruction; 
    [SerializeField] private TextMeshProUGUI PressJInstruction;
    [SerializeField] private TextMeshProUGUI freemoveInstruction;
   
    
    private PlayerControl playerControl;
    
    private bool firstmoveLearned = false;
    private bool upSwitchLearned = false;
    private bool downSwitchLearned = false;
    
    private bool passdownLearned = false;
    private bool passupLearned = false;
    private bool freemoveLearned = false;

    private float firstmoveTime = 2f;
    private float upSwitchTime = 3f;
    private float downSwitchTime = 4f;
    private float passdownTime = 5f;
    private float passupTime = 6f;
    
    private float freemoveTime = 7f;
    
    private float finishTime = 13f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControl = Player.GetComponent<PlayerControl>();
        playerControl.canChangeGravity = true;
        firstmoveInstruction.enabled = false;
        upSwitchInstruction.enabled = false;
        downSwitchInstruction.enabled = false;
        finishInstruction.enabled = false;
        passdownInstruction.enabled = false;
        passupInstruction.enabled = false;
        PressJInstruction.enabled = false;
        freemoveInstruction.enabled = false;
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
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                firstmoveLearned = true;
                firstmoveInstruction.enabled = false;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!upSwitchLearned && Time.timeSinceLevelLoad >= upSwitchTime){
            Time.timeScale = 0f;
            upSwitchInstruction.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.W)) {
                upSwitchLearned = true;
                upSwitchInstruction.enabled = false;
                
                Time.timeScale = 1f;
            }
        }
        if(!downSwitchLearned && Time.timeSinceLevelLoad >= downSwitchTime){
            Time.timeScale = 0f;
            downSwitchInstruction.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.S)) {
                downSwitchLearned = true;
                downSwitchInstruction.enabled = false;
                
                Time.timeScale = 1f;
            }
        }
        if(!passdownLearned && Time.timeSinceLevelLoad >= passdownTime){
            Time.timeScale = 0f;
            passdownInstruction.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.S)) {
                passdownLearned = true;
                passdownInstruction.enabled = false;
                
                Time.timeScale = 1f;
            }
        }
        if(!passupLearned && Time.timeSinceLevelLoad >= passupTime){
            Time.timeScale = 0f;
            passupInstruction.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.W)) {
                passupLearned = true;
                passupInstruction.enabled = false;
                
                Time.timeScale = 1f;
            }
        }

        if(!freemoveLearned && Time.timeSinceLevelLoad >=  freemoveTime){
            Time.timeScale = 0f;
            freemoveInstruction.enabled = true;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                freemoveLearned = true;
                freemoveInstruction.enabled = false;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
                
            }
        }
        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Time.timeScale = 1f;
                SceneManager.LoadScene("LevelMenu");
            }
        }
    }

}
