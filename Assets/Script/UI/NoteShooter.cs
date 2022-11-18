using System.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteShooter : MonoBehaviour
{

    public static NoteShooter Instance;

    private bool shooting = false;

    private Vector3 endPos;

    private GameObject originalPlayer;

    private Vector3 outsidePos = new Vector3(100f, 100f, 0f);

    private void Start() {
        Instance = this;

        originalPlayer = GameObject.Find("Player");

        transform.position = outsidePos;
    }

    void FixedUpdate()
    {
        if(shooting)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, 0.3f);
            if(transform.position == endPos){
                BossHealth.Instance.TakeDamage(200);
                shooting = false;
                transform.position = outsidePos;
            }
        }
    }

    public void Shoot(){
        transform.position = originalPlayer.transform.position;
        endPos = BossUI.Instance.transform.position;
        shooting = true;
    }
}
