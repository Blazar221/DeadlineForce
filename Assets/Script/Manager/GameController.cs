using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverMenu;

    private float endTime = 84f;
    private float timeCount = 0f;
    private bool gameIsEnd = false;

    void Update()
    {
        timeCount += Time.deltaTime;
        if(timeCount > endTime && !IsGameEnd())
        {
            gameIsEnd = true;
            EnableGameOverMenu();
        }
    }

    bool IsGameEnd()
    {
        return gameIsEnd;
    }

    private void OnEnable()
    {
        PlayerControl.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerControl.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        //SendAnalytics.instance.Send(GameOverScreen.instance.getScore());
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        BgmController.instance.StopBgm();
    }
}
