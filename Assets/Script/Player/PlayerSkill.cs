using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public static PlayerSkill instance;

    [SerializeField]
    private GameObject originPlayer;
    private GameObject copyPlayer;

    private PlayerControl originPlayerControl;
    
    [SerializeField]
    private GameObject shield;
    
    private float cloneExistingTime = 7f;
    private bool hasClone = false;


    private void Awake() {
        instance = this;
        originPlayerControl = originPlayer.GetComponent<PlayerControl>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            CallFreezeSkill();
        }
    }

    public void CallFreezeSkill()
    {
        BossBehavior.instance.Freeze();
    }

    public void CallShieldSkill()
    {
        shield.SetActive(true);
        StartCoroutine(CloseShieldSkill());
    }

    IEnumerator CloseShieldSkill()
    {
        yield return new WaitForSeconds(3f);
        shield.SetActive(false);
    }

    public void CallCloneSkill()
    {
        int yPos = 3 - originPlayerControl.GetYPos();
        
        Vector3 spawnPos = originPlayer.transform.position;
        spawnPos.y = originPlayerControl.playerYPosArr[yPos];
        copyPlayer = Instantiate(originPlayer, spawnPos, Quaternion.identity);
        // Clone should disappear after a while
        Destroy(copyPlayer, cloneExistingTime);
        // Make clone mirror the origin
        PlayerControl copyPlayerHandler = copyPlayer.GetComponent<PlayerControl>();
        copyPlayerHandler.EnableClone();
        copyPlayerHandler.SetYPos(yPos);
    }
}
