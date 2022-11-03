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
    [SerializeField] private Image arrowtobar;
    [SerializeField] private Image arrowtoboss;
    [SerializeField] private Image arrowtoMission;
    [SerializeField] private Image arrowtoplayer;
    [SerializeField] private TextMeshProUGUI finishInstruction;
    [SerializeField] private TextMeshProUGUI collectlastInstruction;
    [SerializeField] private TextMeshProUGUI gemcollectionInstruction;
    [SerializeField] private TextMeshProUGUI PressJInstruction;
    [SerializeField] private TextMeshProUGUI DamageInstruction;
    [SerializeField] private TextMeshProUGUI MissionInstruction;
    [SerializeField] private TextMeshProUGUI selfdamageInstruction;
    [SerializeField] private TextMeshProUGUI pointtoselfInstruction;
   
    
    private PlayerControl playerControl;
    
    private bool shortNoteLearned = false;
   
    private bool tripleLearned2 = false;
    private bool tripleLearned3 = false;
    private bool tripleLearned4 = false;
    private bool tripleLearned5 = false;
    
    
    private bool gemcollectionLearned = false;
     private bool MissionLearned = false;
    
    private bool damageLearned = false;
    private bool seconddamageLearned = false;
    private bool selfdamageLearned = false;
    private bool selfdamagePressLearned = false;
    private bool pointtoselfLearned = false;
    
    

    private float firstShortNoteTime = 2f;
   
    private float firstgemcollectionTime = 3f;
    private float t2ShortNoteTime = 4f;
    private float t3ShortNoteTime = 5f;
    private float DamageTime = 6f;
    private float firstmissioncollectionTime = 7f;
    private float t4ShortNoteTime = 8f;
    private float t5ShortNoteTime = 9f;
    
    private float secondDamageTime = 10f;
    private float selfdamageTime = 12f;
    private float pointtoselfTime = 13f;
    
    private float finishTime = 15f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControl = Player.GetComponent<PlayerControl>();
        playerControl.canChangeGravity = false;
        StartInstruction.enabled = false;
        arrowtobar.enabled=false;
        arrowtoboss.enabled=false;
        finishInstruction.enabled = false;
        collectlastInstruction.enabled=false;
        gemcollectionInstruction.enabled=false;
        PressJInstruction.enabled=false;
        MissionInstruction.enabled=false;
        DamageInstruction.enabled=false;
        selfdamageInstruction.enabled=false;
        arrowtoMission.enabled=false;
        pointtoselfInstruction.enabled=false;
        arrowtoplayer.enabled=false;
        
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
            PressJInstruction.enabled = true;
            
            if (Input.GetKeyDown(KeyCode.Space)) {
                shortNoteLearned = true;
                StartInstruction.enabled = false;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }

        if(!gemcollectionLearned && Time.timeSinceLevelLoad >= firstgemcollectionTime){
            Time.timeScale = 0f;
            gemcollectionInstruction.enabled = true;
            PressJInstruction.enabled = true;
            arrowtobar.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                gemcollectionLearned = true;
                gemcollectionInstruction.enabled = false;
                PressJInstruction.enabled = false;
                arrowtobar.enabled=false;

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
            arrowtobar.enabled=true;
            collectlastInstruction.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned3 = true;
                PressJInstruction.enabled = false;
                collectlastInstruction.enabled=false;
                arrowtobar.enabled=false;
                Time.timeScale = 1f;
            }
        }
        if(!damageLearned && Time.timeSinceLevelLoad >= DamageTime){
            Time.timeScale = 0f;
            
            PressJInstruction.enabled = true;
            arrowtoboss.enabled=true;
            DamageInstruction.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                damageLearned=true;
                DamageInstruction.enabled=false;
                PressJInstruction.enabled = false;
                arrowtoboss.enabled=false;

                Time.timeScale = 1f;
            }
        }

        if(!MissionLearned && Time.timeSinceLevelLoad >= firstmissioncollectionTime){
            Time.timeScale = 0f;
            
            PressJInstruction.enabled = true;
            arrowtoMission.enabled=true;
            MissionInstruction.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                MissionLearned=true;
                MissionInstruction.enabled=false;
                PressJInstruction.enabled = false;
                arrowtoMission.enabled=false;

                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned4 && Time.timeSinceLevelLoad >= t4ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            arrowtoMission.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned4 = true;
                arrowtoMission.enabled=false;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        if(!tripleLearned5 && Time.timeSinceLevelLoad >= t5ShortNoteTime){
            Time.timeScale = 0f;
            PressJInstruction.enabled = true;
            arrowtoMission.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                tripleLearned5 = true;
                arrowtoMission.enabled=false;
                PressJInstruction.enabled = false;
                Time.timeScale = 1f;
            }
        }
        
        if(!seconddamageLearned && Time.timeSinceLevelLoad >= secondDamageTime){
            Time.timeScale = 0f;
            
            PressJInstruction.enabled = true;
            arrowtoboss.enabled=true;
            DamageInstruction.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                seconddamageLearned=true;
                DamageInstruction.enabled=false;
                PressJInstruction.enabled = false;
                arrowtoboss.enabled=false;

                Time.timeScale = 1f;
            }
        }
        if(!selfdamageLearned && Time.timeSinceLevelLoad >= selfdamageTime){
            Time.timeScale = 0f;
            
            PressJInstruction.enabled = true;
            arrowtoplayer.enabled=true;
            selfdamageInstruction.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                selfdamageLearned=true;
                selfdamageInstruction.enabled=false;
                PressJInstruction.enabled = false;
                arrowtoplayer.enabled=false;

                Time.timeScale = 1f;
            }
        }

        if(!pointtoselfLearned  && Time.timeSinceLevelLoad >= pointtoselfTime){
            Time.timeScale = 0f;
            
            PressJInstruction.enabled = true;
            arrowtoplayer.enabled=true;
            pointtoselfInstruction.enabled=true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                pointtoselfLearned=true;
                pointtoselfInstruction.enabled=false;
                PressJInstruction.enabled = false;
                arrowtoplayer.enabled=false;

                Time.timeScale = 1f;
            }
        }



        

       


        if(Time.timeSinceLevelLoad >= finishTime){
            Time.timeScale = 0f;
            finishInstruction.enabled = true;
            if (Input.GetKeyDown(KeyCode.J)) {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MoveTutorial");
            }
        }
    }

}
