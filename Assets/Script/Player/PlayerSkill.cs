using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]
    private GameObject originPlayer;
    private GameObject copyPlayer;

    private PlayerControl originPlayerControl;
    
    private float cloneExistingTime = 7f;
    private bool hasClone = false;

    // Start is called before the first frame update
    void Start()
    {
        originPlayerControl = originPlayer.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            int yPos = 3 - originPlayerControl.GetYPos();
            
            Vector3 spawnPos = originPlayer.transform.position;
            spawnPos.y = originPlayerControl.playerYPosArr[yPos];
            copyPlayer = Instantiate(originPlayer, spawnPos, Quaternion.identity);

            Destroy(copyPlayer, cloneExistingTime);
            
            PlayerControl copyPlayerHandler = copyPlayer.GetComponent<PlayerControl>();
            copyPlayerHandler.EnableClone();
            copyPlayerHandler.SetYPos(yPos);
        }
    }
}
