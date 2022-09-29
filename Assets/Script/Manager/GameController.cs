using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject congratsMenu;

    private float endTime = 84f;
    private float timeCount = 0f;
    private bool gameIsEnd = false;

    void Update()
    {
        timeCount += Time.deltaTime;
        if(timeCount > endTime && !IsGameEnd())
        {
            gameIsEnd = true;
            EnableCongratsMenu();
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

    // 血量掉光游戏结束
    public void EnableGameOverMenu()
    {
        Level1Score.instance.Send();
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        BgmController.instance.StopBgm();
    }

    // 通关游戏结束
    public void EnableCongratsMenu()
    {
        Level1Score.instance.Send();
        ScoreManager.instance.GetTotalScore();
        congratsMenu.SetActive(true);
        Time.timeScale = 0f;
        BgmController.instance.StopBgm();
    }
}
