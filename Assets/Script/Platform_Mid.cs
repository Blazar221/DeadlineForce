using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Mid : MonoBehaviour
{   
    [SerializeField] GameObject  platform;
    [SerializeField] GameObject  GravSwitch;
    private Color colorOri;
    private float Alpha = 1.0f;
    private bool fading=true;
    private float playerX,GravSwitchX;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerX=GameObject.Find("DinasourRunner").transform.position.x;
        GravSwitch=GameObject.Find("GravSwitch(Clone)");
        if(GravSwitch){
            GravSwitchX=GravSwitch.transform.position.x;
        }else{
            GravSwitchX=playerX+30;
        }
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

        if(GravSwitchX-playerX<10&&GravSwitchX-playerX>-10){
            platform.GetComponent<SpriteRenderer>().enabled=false;
            platform.GetComponent<BoxCollider2D>().enabled=false;
        }else{
            platform.GetComponent<SpriteRenderer>().enabled=true;
            platform.GetComponent<BoxCollider2D>().enabled=true;
        }       
    }
}
