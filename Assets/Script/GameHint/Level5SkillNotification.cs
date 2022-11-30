using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level5SkillNotification : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI SkillInstruction;
    [SerializeField] private TextMeshProUGUI PressEnterInstruction;
    [SerializeField] private Image Box;
    // [SerializeField] private UnityEngine.Rendering.Universal.Light2D GlobalLight;
    // [SerializeField] private UnityEngine.Rendering.Universal.Light2D TargetPanelSpotLight;
    void Start()
    {
        SkillInstruction.enabled = false;
        PressEnterInstruction.enabled = false;
        Box.enabled=false;
        PlayerPrefs.DeleteKey("Level5opened");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("Level5opened")){
            this.enabled=false;
        }else{
            Time.timeScale = 0f;
            SkillInstruction.enabled = true;
            BgmController.instance.StopBgm();
            AudioListener.pause = true;
            Box.enabled= true;
            PressEnterInstruction.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            SkillInstruction.enabled = false;
            PressEnterInstruction.enabled = false;
            BgmController.instance.ContinuePlayBgm();
            AudioListener.pause = false;
            Box.enabled=false;
            BgmController.instance.songPosition=0.0f;
            PlayerPrefs.SetInt("Level5opened",1);
            Time.timeScale = 1f;
        }
    }
    
}
