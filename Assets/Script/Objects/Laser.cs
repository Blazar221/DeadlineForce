using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    float hurtLimit = 0.2f;

    float hurtTimer = 0;

    GameObject player;

    int alertLine = -1;

    public void SetAlertLine(int l)
    {
        alertLine = l;
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 1.2f)
        {
            hurtTimer += Time.fixedDeltaTime;
            if(hurtTimer > hurtLimit)
            {
                PlayerHealth.Instance.TakeDamage(10);
                hurtTimer = 0;
            }
        }
        else{
            hurtTimer = 0;
        }
    }

    private void OnDestroy() {
        AlertController.Instance.EndAlert(alertLine);
    }
}
