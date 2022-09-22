using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen instance;

    public Text pointsText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pointsText.text = score.ToString() + " POINTS";
    }

    public void IncreaseScore() 
    {
        score += 1;
        pointsText.text = score.ToString() + "POINTS";
    }

    public void DecreaseScore()
    {
        score -= 1;
        pointsText.text = score.ToString() + "POINTS";
    }
}
