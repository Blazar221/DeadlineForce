using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondCollection : MonoBehaviour
{
    public static DiamondCollection Instance;
    private float time = 0f;
    private float timeCount_f = 0f;
    private int timeCount_i = 0;

    private int fireCount = 0;
    private int grassCount = 0;
    private int waterCount = 0;
    private int rockCount = 0;
    SpriteRenderer fireRenderer;
    SpriteRenderer grassRenderer;
    SpriteRenderer waterRenderer;
    SpriteRenderer rockRenderer;

    public int fullDamage = 200;
    public int singleLimit = 5;


    [SerializeField] public GameObject fireDiamond;
    public HealthBar fireBar;
    public GameObject grassDiamond;
    public HealthBar grassBar;
    public GameObject waterDiamond;
    public HealthBar waterBar;
    public GameObject rockDiamond;
    public HealthBar rockBar;

    // Update is called once per frame
    void Awake()
    {
        Instance = this;
    }

    public void Reset()
    {
        
        // if ((fireCount + grassCount + waterCount + rockCount) >= 30)
        if (fireCount >= singleLimit && grassCount >= singleLimit && waterCount >= singleLimit && rockCount >= singleLimit)
        {
            Boss.instance.TakeDamage(fullDamage);
            fireCount = 0;
            grassCount = 0;
            waterCount = 0;
            rockCount = 0;
            fireBar.SetHealth(fireCount);
            grassBar.SetHealth(grassCount);
            waterBar.SetHealth(waterCount);
            rockBar.SetHealth(rockCount);
            timeCount_f = Time.deltaTime - time;
            timeCount_i = (int)timeCount_f;
            Debug.Log(timeCount_i);
        }
        time = Time.deltaTime;
    }

    public void AddFireCount()
    {
        fireCount += 1;
        fireBar.SetHealth(fireCount);
    }

    public void AddGrassCount()
    {
        grassCount += 1;
        grassBar.SetHealth(grassCount);
    }

    public void AddWaterCount()
    {
        waterCount += 1;
        waterBar.SetHealth(waterCount);
    }

    public void AddRockCount()
    {
        rockCount += 1;
        rockBar.SetHealth(rockCount);
    }
}
