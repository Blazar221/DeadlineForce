using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject congratsMenu;
    public static GameController Instance;
    [SerializeField]
    GameObject platformTop;
    [SerializeField]
    GameObject platformBottom;


    [SerializeField]
    private float endTime = 84f;
    private float timeCount = 0f;
    private bool gameIsEnd = false;

    private void Awake() {
        Instance = this;
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Level1"){
            platformTop.SetActive(false);
            platformBottom.SetActive(false);
        }
    }

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
        SendAnalytics();
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        BgmController.instance.StopBgm();
    }

    // 通关游戏结束
    public void EnableCongratsMenu()
    {
        SendAnalytics();
        ScoreManager.instance.GetTotalScore();
        congratsMenu.SetActive(true);
        Time.timeScale = 0f;
        BgmController.instance.StopBgm();
    }

    public void SendAnalytics()
    {
        if (Application.isEditor)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    Level1Editor.instance.UpdateCounter(Boss.instance.stateCount);
                    Level1Editor.instance.Send();
                    break;
                case "Level2":
                    //Level2Editor.instance.Send();
                    break;
            }
        }
        else
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    Level1Web.instance.UpdateCounter(Boss.instance.stateCount);
                    Level1Web.instance.Send();
                    break;
                case "Level2":
                    //Level2Web.instance.Send();
                    break;
            }
        }
    }
}
