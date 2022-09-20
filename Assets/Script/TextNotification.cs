using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class TextNotification : MonoBehaviour
{

    private Text notification;
    private Color colorOri;
    private float Alpha = 1.0f;
    private bool fading=true;
    // Start is called before the first frame update
    void Start()
    {
        notification=GetComponent<Text>();
        colorOri=notification.color;
    }

    // Update is called once per frame
    void Update()
    {

        if(fading){
            Alpha = Alpha - (Time.deltaTime)/2;
            colorOri.a = Alpha;
            notification.color = colorOri;
            if(Alpha<=0){
                fading=false;
            }
        }else{
            Alpha = Alpha + (Time.deltaTime)/2;
            colorOri.a = Alpha;
            notification.color = colorOri;
            if(Alpha>=1){
                fading=true;
            }
        }

        if(Time.time>10){
            notification.enabled=false;
            
        }

        // if (Input.GetKeyDown (KeyCode.Space)&&notification.name=="SpaceBar")
		// {
        //     notification.enabled=false;
		// }

        //  if (Input.GetKeyDown (KeyCode.P)&&notification.name=="P")
		// {
        //     notification.enabled=false;
		// }

       
    }

}
