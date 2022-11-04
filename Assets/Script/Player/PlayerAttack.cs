using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool pressingK;
    // The diamond to destroy
    private GameObject toHit;

    private float nextTime;
    private float collsionTime;
    private float longNoteScoreTimeCounter = 0f;
    private float longNoteScoreTimeBar = 0.2f;
    
    private bool canGetSingleScore;
    private bool canGetLongScore;
    private bool canAvoidDamage;
    public bool canCross;
    private bool missFood;
    private bool missMine;
    [SerializeField]
    public GameObject hitEffect;
    public GameObject missEffect;
    // Diamond collection
    SpriteRenderer fireRenderer;
    SpriteRenderer grassRenderer;
    SpriteRenderer waterRenderer;
    SpriteRenderer rockRenderer;
    public GameObject fireDiamond;
    public SliderBar fireBar;
    public GameObject grassDiamond;
    public SliderBar grassBar;
    public GameObject waterDiamond;
    public SliderBar waterBar;
    public GameObject rockDiamond;
    public SliderBar rockBar;
    private int fireCount = 0;
    private int grassCount = 0;
    private int waterCount = 0;
    private int rockCount = 0;
    void Awake()
    {
        pressingK = false;
        canCross = false;

        nextTime = Time.time;
        canGetSingleScore = false;

        fireBar.SetMinValue(fireCount);
        grassBar.SetMinValue(grassCount);
        waterBar.SetMinValue(waterCount);
        rockBar.SetMinValue(rockCount);
        fireRenderer = fireDiamond.GetComponent<SpriteRenderer>();
        grassRenderer = grassDiamond.GetComponent<SpriteRenderer>();
        waterRenderer = waterDiamond.GetComponent<SpriteRenderer>();
        rockRenderer = rockDiamond.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
