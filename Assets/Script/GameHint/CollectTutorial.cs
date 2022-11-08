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
    [SerializeField] private TextMeshProUGUI differentcolorInstruction;
    [SerializeField] private TextMeshProUGUI finishInstruction;
    [SerializeField] private TextMeshProUGUI TripleInstruction;
    [SerializeField] private TextMeshProUGUI PressJInstruction;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D GlobalLight;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D SpotLight;
    
    private PlayerMovement playerMovement;
    
    private bool shortNoteLearned = false;
    private bool tripleLearned1 = false;
    private bool tripleLearned2 = false;
    private bool tripleLearned3 = false;
    private bool longNoteLearned = false;

    private float firstShortNoteTime = 2f;
    private float t1ShortNoteTime = 3f;
    private float t2ShortNoteTime = 4f;
    private float t3ShortNoteTime = 5f;
    private float firstLongNoteTime = 7f;
    private float finishTime = 9f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        playerMovement.canChangeGravity = false;
        shortNoteInstruction.enabled = false;
        longNoteInstruction.enabled = false;
        differentcolorInstruction.enabled = false;
        finishInstruction.enabled = false;
        TripleInstruction.enabled=false;
        PressJInstruction.enabled=false;
        GlobalLight.enabled = false;
        SpotLight.enabled = false;
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
        playerMovement.canChangeGravity = false;

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
            differentcolorInstruction.enabled = true;
            PressJInstruction.enabled = true;
            GlobalLight.enabled = true;
            SpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned1 = true;
                PressJInstruction.enabled = false;
                differentcolorInstruction.enabled = false;
                GlobalLight.enabled = false;
                SpotLight.enabled = false;
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
       
        
        if(!longNoteLearned && Time.timeSinceLevelLoad >= firstLongNoteTime){
            Time.timeScale = 0f;
            longNoteInstruction.enabled = true;
            GlobalLight.enabled = true;
            SpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                longNoteLearned = true;
                longNoteInstruction.enabled = false;
                GlobalLight.enabled = false;
                SpotLight.enabled = false;
                Time.timeScale = 1f;
            }
        }


        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                Time.timeScale = 1f;
                SceneManager.LoadScene("AttackTutorial");
            }
        }
    }

}
