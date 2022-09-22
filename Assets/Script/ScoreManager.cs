using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    
    int hit = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = hit.ToString() + "  " + "HIT";
    }

    public void AddPoint()
    {
        hit += 1;
        scoreText.text = hit.ToString() + "  " + "HIT";
    }
}
