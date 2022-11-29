using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SampleSceneSkillNotification : MonoBehaviour
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
        //PlayerPrefs.DeleteKey("Sampleopened");
        //测试用每次使用前把注册表的键删掉，每次都会弹出提示
        //如果是第二关就是，Level2opened；如果是第三关就是，Level3opened...
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("Sampleopened")){
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
            Box.enabled= false;
            BgmController.instance.songPosition=0.0f;
            PlayerPrefs.SetInt("Sampleopened",1);
            Time.timeScale = 1f;
        }
    }
    
}
