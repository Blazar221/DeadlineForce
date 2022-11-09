using System.Diagnostics;
using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{

    public static bool acitivated;

    // Start is called before the first frame update
    void Start()
    {
        acitivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (acitivated) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            transform.position = PlayerMovement.instance.transform.position;
            Vector3 targ = new Vector3(BossUI.instance.transform.position.x, BossUI.instance.transform.position.y-3, BossUI.instance.transform.position.z);
            Vector2 dire = targ - transform.position;
            GetComponent<Rigidbody2D>().AddForce(dire*100);
            acitivated = false;
        }
    }
}
