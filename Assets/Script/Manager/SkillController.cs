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

    private PlayerMovement playerOriginMovement;
    private PlayerMovement playerCloneMovement;
    
    [SerializeField]
    private GameObject shield;
    
    private float cloneExistingTime = 7f;
    private bool hasClone = false;


    private void Awake() {
        Instance = this;
        playerOriginMovement = playerOrigin.GetComponent<PlayerMovement>();
        playerCloneMovement = playerClone.GetComponent<PlayerMovement>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            CallFreezeSkill();
        }
    }

    public void CallFreezeSkill()
    {
        Debug.Log("Freeze");
        BossBehavior.instance.Freeze();
    }

    public void CallShieldSkill()
    {
        shield.SetActive(true);
        PlayerHealth.Instance.EnableShield();
        StartCoroutine(CloseShieldSkill());
    }

    IEnumerator CloseShieldSkill()
    {
        yield return new WaitForSeconds(3f);
        PlayerHealth.Instance.DisableShield();
        shield.SetActive(false);
    }

    public void CallCloneSkill()
    {
        playerClone.SetActive(true);
        int yPos = 3 - playerOriginMovement.GetYPos();
        playerCloneMovement.EnableClone();
        playerCloneMovement.SetYPos(yPos);
        StartCoroutine(CloseCloneSkill());
    }

    IEnumerator CloseCloneSkill()
    {
        yield return new WaitForSeconds(cloneExistingTime);
        playerClone.SetActive(false);
    }
}
