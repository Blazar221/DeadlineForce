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
    [SerializeField] private TextMeshProUGUI PressJInstruction;
    [SerializeField] private Image LeftArrow;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D GlobalLight;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D SpotLight;
    
    private PlayerMovement playerMovement;
    
    private bool shortNoteLearned = false;
    private bool tripleLearned1 = false;
    private bool tripleLearned2 = false;
    private bool tripleLearned3 = false;
    private bool longNoteLearned = false;

    private float a0ShortNoteTime = 2f;
    private float a1ShortNoteTime = 3f;
    private float a2ShortNoteTime = 4f;
    private float a3ShortNoteTime = 5f;
    private float firstLongNoteTime = 7f;
    private float finishTime = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        shortNoteInstruction.enabled = false;
        longNoteInstruction.enabled = false;
        differentcolorInstruction.enabled = false;
        finishInstruction.enabled = false;
        PressJInstruction.enabled=false;
        LeftArrow.enabled = false;
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

        if(!shortNoteLearned && Time.timeSinceLevelLoad >= a0ShortNoteTime){
            Time.timeScale = 0f;
            shortNoteInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                shortNoteLearned = true;
                shortNoteInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned1 && Time.timeSinceLevelLoad >= a1ShortNoteTime){
            Time.timeScale = 0f;
            differentcolorInstruction.enabled = true;
            LeftArrow.enabled = true;
            PressJInstruction.enabled = true;
            GlobalLight.enabled = true;
            SpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned1 = true;
                PressJInstruction.enabled = false;
                GlobalLight.enabled = false;
                SpotLight.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned2 && Time.timeSinceLevelLoad >= a2ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned2 = true;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
         if(!tripleLearned3 && Time.timeSinceLevelLoad >= a3ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned3 = true;
                differentcolorInstruction.enabled = false;
                LeftArrow.enabled = false;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!longNoteLearned && Time.timeSinceLevelLoad >= firstLongNoteTime){
            Time.timeScale = 0f;
            longNoteInstruction.enabled = true;
            LeftArrow.enabled = true;
            GlobalLight.enabled = true;
            SpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                longNoteLearned = true;
                GlobalLight.enabled = false;
                SpotLight.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            longNoteInstruction.enabled = false;
            LeftArrow.enabled = false;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                Time.timeScale = 1f;
                SceneManager.LoadScene("AttackTutorial");
            }
        }
    }

}
