using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text totalScoreText;
    public Text hitTimesText;
    public Text hitScoreText;
    public Text hitRateText;
    
    int totalScore = 0;
    double hit = 0;
    double miss = 0;
    double hitRate = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        totalScoreText.text = "Total Score:" + " " + totalScore.ToString();
        hitTimesText.text = hit.ToString() + "  " + "HIT";
        hitScoreText.text = "Hit Score:" + " " + hit.ToString();
        hitRateText.text = "Hit Rate:" + " " + hitRate.ToString() + "%";
    }

    public void AddHit()
    {
        hit += 1;
        totalScore += 1;
        totalScoreText.text = "Total Score:" + " " + totalScore.ToString();
        hitTimesText.text = hit.ToString() + "  " + "HIT";
        hitScoreText.text = "Hit Score:" + " " + hit.ToString();
    }

    public void AddMiss()
    {
        miss += 1;
        totalScore -= 1;
        totalScoreText.text = "Total Score:" + " " + totalScore.ToString();
    }

    public void CalHitRate()
    {
        hitRate = hit / (hit + miss)*100;
        hitRateText.text = "Hit Rate:" + " " + hitRate.ToString("f2") + "%";
    }

    public int GetTotalScore()
    {
        if(totalScore <= 0)
        {
            totalScore = 0;
            return 0;
        }
        else
        {
            return totalScore;
        }
    }
}
