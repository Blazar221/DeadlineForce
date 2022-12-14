using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AttackTutorial : MonoBehaviour
{

    ///private Text shortNoteInstruction;
    [SerializeField] private GameObject Player;
    [SerializeField] private TextMeshProUGUI StartInstruction;
    //[SerializeField] private TextMeshProUGUI finishInstruction;
    [SerializeField] private TextMeshProUGUI PressJInstruction;
    [SerializeField] private TextMeshProUGUI DamageInstruction;
    [SerializeField] private TextMeshProUGUI MissionInstruction;
    [SerializeField] private TextMeshProUGUI selfdamageInstruction;
    [SerializeField] private TextMeshProUGUI pointtoselfInstruction;
    [SerializeField] private TextMeshProUGUI ContinueInstruction;
    [SerializeField] private TextMeshProUGUI SkillInstruction;
    [SerializeField] private TextMeshProUGUI CdInstruction;
    [SerializeField] private Image LeftArrow;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D GlobalLight;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D DiamondBarSpotLight;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D BossSpotLight;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D TargetPanelSpotLight;
    
    private PlayerMovement playerMovement;
    
    private bool shortNoteLearned = false;
   
    private bool tripleLearned2 = false;
    private bool tripleLearned3 = false;
    private bool skillLearned = false;
    private bool tripleLearned4 = false;
    private bool tripleLearned5 = false;
    private bool tripleLearned6 = false;
    
    
    private bool gemcollectionLearned = false;
    
    private bool selfdamageLearned = false;
    private bool selfdamagePressLearned = false;
    private bool pointtoselfLearned = false;
    
    

    private float firstShortNoteTime = 2f;
   
    private float firstgemcollectionTime = 3f;
    private float t3ShortNoteTime = 4f;
    private float skillTime = 6f;
    private float t4ShortNoteTime = 8f;
    private float t5ShortNoteTime = 9f;
    private float t6ShortNoteTime = 10f;
    
    private float selfdamageTime = 6f;
    private float pointtoselfTime = 7f;
    
    private float finishTime = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        StartInstruction.enabled = false;
        //finishInstruction.enabled = false;
        PressJInstruction.enabled=false;
        MissionInstruction.enabled=false;
        DamageInstruction.enabled=false;
        selfdamageInstruction.enabled=false;
        pointtoselfInstruction.enabled=false;
        ContinueInstruction.enabled = false;
        SkillInstruction.enabled = false;
        CdInstruction.enabled = false;
        LeftArrow.enabled = false;
        
        GlobalLight.enabled = false;
        DiamondBarSpotLight.enabled = false;
        BossSpotLight.enabled = false;
        TargetPanelSpotLight.enabled = false;
        CollectionController.Instance.SetFireAlmostFull();
        CollectionController.Instance.SetGrassFull();
        CollectionController.Instance.SetWaterAlmostFull();
        CollectionController.Instance.SetRockAlmostFull();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("currtime="+Time.timeSinceLevelLoad);

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
            StartInstruction.enabled = true;
            LeftArrow.enabled = true;
            PressJInstruction.enabled = true;
            GlobalLight.enabled = true;
            DiamondBarSpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                shortNoteLearned = true;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }

        if(!gemcollectionLearned && Time.timeSinceLevelLoad >= firstgemcollectionTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            GlobalLight.enabled = true;
            DiamondBarSpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                gemcollectionLearned = true;
                StartInstruction.enabled = false;
                PressJInstruction.enabled = false;
                LeftArrow.enabled = false;

                Time.timeScale = 1f;
            }
        }
         if(!tripleLearned3 && Time.timeSinceLevelLoad >= t3ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            LeftArrow.enabled = true;
            DamageInstruction.enabled=true;
            BossSpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned3 = true;
                PressJInstruction.enabled = false;
                DamageInstruction.enabled=false;
                LeftArrow.enabled = false;
                DiamondBarSpotLight.enabled = false;
                Time.timeScale = 1f;
            }
        }
        
        /*
        if(!skillLearned && Time.timeSinceLevelLoad >= skillTime){
            Time.timeScale = 0f;
            SkillInstruction.enabled = true;
            ContinueInstruction.enabled = true;
            GlobalLight.enabled = true;
            TargetPanelSpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                skillLearned = true;
                SkillInstruction.enabled = false;
                ContinueInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned4 && Time.timeSinceLevelLoad >= t4ShortNoteTime){
            Time.timeScale = 0f;
            MissionInstruction.enabled=true;
            PressJInstruction.enabled = true;
            BossSpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned4 = true;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned5 && Time.timeSinceLevelLoad >= t5ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned5 = true;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned6 && Time.timeSinceLevelLoad >= t6ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned6 = true;
                PressJInstruction.enabled = false;
                MissionInstruction.enabled=false;
                TargetPanelSpotLight.enabled = false;
                Time.timeScale = 1f;
            }
        }
        */
        if(!selfdamageLearned && Time.timeSinceLevelLoad >= selfdamageTime){
            Time.timeScale = 0f;
            
            PressJInstruction.enabled = true;
            selfdamageInstruction.enabled=true;
            BossSpotLight.enabled = false;
            DiamondBarSpotLight.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                selfdamageLearned=true;
                selfdamageInstruction.enabled=false;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }

        if(!pointtoselfLearned  && Time.timeSinceLevelLoad >= pointtoselfTime){
            Time.timeScale = 0f;
            
            ContinueInstruction.enabled = true;
            pointtoselfInstruction.enabled=true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                pointtoselfLearned=true;
                pointtoselfInstruction.enabled=false;
                ContinueInstruction.enabled = false;
                GlobalLight.enabled = false;
                DiamondBarSpotLight.enabled = false;

                Time.timeScale = 1f;
                SceneManager.LoadScene("MoveTutorial");
            }
        }
        /*
        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            GlobalLight.enabled = true;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return)) {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MoveTutorial");
            }
        }
        */
    }

}
