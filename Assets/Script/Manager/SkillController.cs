using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public static SkillController Instance;

    [SerializeField]
    private GameObject playerOrigin;
    [SerializeField]
    private GameObject playerClone;

    private PlayerControl playerOriginControl;
    private PlayerControl playerCloneControl;
    
    [SerializeField]
    private GameObject shield;
    
    private float cloneExistingTime = 7f;
    private bool hasClone = false;


    private void Awake() {
        Instance = this;
        playerOriginControl = playerOrigin.GetComponent<PlayerControl>();
        playerCloneControl = playerClone.GetComponent<PlayerControl>();
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
        playerClone.SetActive(true);
        int yPos = 3 - playerOriginControl.GetYPos();
        playerCloneControl.EnableClone();
        playerCloneControl.SetYPos(yPos);
        StartCoroutine(CloseCloneSkill());
    }

    IEnumerator CloseCloneSkill()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Close");
        playerClone.SetActive(false);
    }
}
