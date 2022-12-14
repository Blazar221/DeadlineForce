using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject congratsMenu;
    public static GameController Instance;

    private int bossHealth;
    private int playerHealth;
    [SerializeField]
    private float endTime = 84f;
    private float timeCount = 0f;
    private bool gameIsEnd = false;

    private void Awake() {
        Instance = this;
    }

    void Update()
    {
        timeCount += Time.deltaTime;
        bossHealth = BossHealth.Instance.GetBossHealth();
        playerHealth = PlayerHealth.Instance.GetPlayerHealth();
        if(timeCount > endTime && !IsGameEnd()){
            gameIsEnd = true;
            if(bossHealth <= 0) EnableCongratsMenu();
            else EnableGameOverMenu();
            
        }
        // if(timeCount > endTime && bossHealth <= 0 && playerHealth > 0 && !IsGameEnd())
        // {
        //     gameIsEnd = true;
        //     EnableCongratsMenu();
        // } else if ((timeCount > endTime && bossHealth > 0 && !IsGameEnd()) || playerHealth <= 0)
        // {
        //     gameIsEnd = true;
        //     EnableGameOverMenu();
        // }
    }

    bool IsGameEnd()
    {
        return gameIsEnd;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += EnableGameOverMenu;
        BossHealth.OnBossDeath += EnableCongratsMenu;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= EnableGameOverMenu;
        BossHealth.OnBossDeath -= EnableCongratsMenu;
    }

    // 血量掉光游戏结束
    public void EnableGameOverMenu()
    {
        SendAnalytics();
        //gameOverMenu.SetActive(true);
        //Time.timeScale = 0f;
        BgmController.instance.StopBgm();
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("GameOverMenu");
    }

    // 通关游戏结束
    public void EnableCongratsMenu()
    {
        SendAnalytics();
        ScoreManager.instance.GetTotalScore();
        //congratsMenu.SetActive(true);
        //Time.timeScale = 0f;
        BgmController.instance.StopBgm();
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("CongratsMenu");
    }

    public void SendAnalytics()
    {
        if (Application.isEditor)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    // Level1Editor.instance.Send();
                    break;
                case "Level2":
                    // Debug.Log(Level2Editor.instance);
                    Level2Editor.instance.Send();
                    break;
                case "Level3":
                    // Level3Editor.instance.Send();
                    break;
            }
        }
        else
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    Level1Web.instance.Send();
                    break;
                case "Level2":
                    Level2Web.instance.Send();
                    break;
                case "Level3":
                    Level3Web.instance.Send();
                    break;
            }
        }
    }
}
