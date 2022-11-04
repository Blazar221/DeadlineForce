using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondCollection : MonoBehaviour
{
    public static DiamondCollection Instance;
    public string time = "";
    private float lastTime = 0f;
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
    public SliderBar fireBar;
    public GameObject grassDiamond;
    public SliderBar grassBar;
    public GameObject waterDiamond;
    public SliderBar waterBar;
    public GameObject rockDiamond;
    public SliderBar rockBar;

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
            BossUI.instance.TakeDamage(fullDamage);
            fireCount = 0;
            grassCount = 0;
            waterCount = 0;
            rockCount = 0;
            fireBar.SetValue(fireCount);
            grassBar.SetValue(grassCount);
            waterBar.SetValue(waterCount);
            rockBar.SetValue(rockCount);
            time += (Time.timeSinceLevelLoad - lastTime).ToString() + "|";
            lastTime = Time.timeSinceLevelLoad;
            Debug.Log(time);
        }
        
    }

    public void AddFireCount()
    {
        fireCount += 1;
        fireBar.SetValue(fireCount);
    }

    public void AddGrassCount()
    {
        grassCount += 1;
        grassBar.SetValue(grassCount);
    }

    public void AddWaterCount()
    {
        waterCount += 1;
        waterBar.SetValue(waterCount);
    }

    public void AddRockCount()
    {
        rockCount += 1;
        rockBar.SetValue(rockCount);
    }
    
    public void SetFireAlmostFull()
    {
        fireCount = 8;
        fireBar.SetValue(fireCount);
    }
    
    public void SetGrassAlmostFull()
    {
        grassCount = 8;
        grassBar.SetValue(grassCount);
    }
    
    public void SetWaterAlmostFull()
    {
        waterCount = 8;
        waterBar.SetValue(waterCount);
    }
    
    public void SetRockAlmostFull()
    {
        rockCount = 8;
        rockBar.SetValue(rockCount);
    }
}
