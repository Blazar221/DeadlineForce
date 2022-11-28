using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBall : MonoBehaviour
{
    private float speed = 0.25f;

    private void FixedUpdate()
    {
        Vector3 bossPos = BossUI.Instance.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, bossPos, speed);
        float distance = Vector3.Distance(transform.position, bossPos);
        if(distance < 1.2f)
        {
            BossHealth.Instance.TakeDamage(200);
            Destroy(gameObject);
        }
    }
}
