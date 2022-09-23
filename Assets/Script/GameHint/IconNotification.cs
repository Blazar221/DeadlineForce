using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;


public class IconNotification : MonoBehaviour
{
    private Image notification;
    private Color colorOri;
    private float Alpha = 1.0f;
    private bool fading=true;

    private float showTime = 25f;
    // Start is called before the first frame update
    void Start()
    {
        notification=GetComponent<Image>();
        colorOri=notification.color;
    }

    // Update is called once per frame
    void Update()
    {

        // if(fading){
        //     Alpha = Alpha - (Time.deltaTime)/7;
        //     colorOri.a = Alpha;
        //     notification.color = colorOri;
        //     if(Alpha<=0){
        //         fading=false;
        //     }
        // }else{
        //     Alpha = Alpha + (Time.deltaTime)/7;
        //     colorOri.a = Alpha;
        //     notification.color = colorOri;
        //     if(Alpha>=1){
        //         fading=true;
        //     }
        // }

        if(Time.time>showTime){
            notification.enabled=false;
            
        }

        // if (Input.GetKeyDown (KeyCode.Space)&&notification.name=="SpaceIcon")
		// {
        //     notification.enabled=false;
		// }

        //  if (Input.GetKeyDown (KeyCode.P)&&notification.name=="PIcon")
		// {
        //     notification.enabled=false;
		// }

       
    }

}
