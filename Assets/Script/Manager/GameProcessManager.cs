using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessManager : MonoBehaviour
{
    private float endTime = 84f;

    private float timeCount = 0f;

    private bool gameIsEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        Debug.Log("time is "+timeCount);
        if(timeCount>endTime){
            gameIsEnd = true;
        }
    }

    bool IsGameEnd(){
        return gameIsEnd;
    }
}
