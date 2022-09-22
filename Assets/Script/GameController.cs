using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gameOverMenu;

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
        gameOverMenu.SetActive(true);
    }
}
