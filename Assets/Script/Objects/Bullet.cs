using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject player;
    
    float speed = 0.06f;

    Vector3 midTarget;

    bool reachMid = false;

    int alertLine = -1;

    void Start()
    {
       player = GameObject.Find("Player");
    }

    public void SetMidTarget(Vector3 v)
    {
        midTarget = v;
    }

    public void SetAlertLine(int l)
    {
        alertLine = l;
    }

    private void FixedUpdate()
    {
        if(!reachMid && transform.position == midTarget)
        {
            reachMid = true;
            Debug.Log(midTarget);
        }
        if(!reachMid)
        {
            transform.position = Vector3.MoveTowards(transform.position, midTarget, speed);
        }
        else{
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }
        
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 1.2f)
        {
            PlayerHealth.Instance.TakeDamage(30);
            Destroy(gameObject);
            if(alertLine!=-1)
            {
                AlertController.Instance.EndAlert(alertLine);
            }
        }
        else if(transform.position.x < player.transform.position.x - 3)
        {
            Destroy(gameObject);
            if(alertLine!=-1)
            {
                AlertController.Instance.EndAlert(alertLine);
            }
        }
    }
}
