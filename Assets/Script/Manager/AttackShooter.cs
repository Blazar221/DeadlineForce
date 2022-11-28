using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShooter : MonoBehaviour
{
    public static AttackShooter Instance;

    [SerializeField] private GameObject rainbowBall;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject iceBall;

    private GameObject originalPlayer;

    private void Start() {
        Instance = this;

        originalPlayer = GameObject.Find("Player");
    }

    public void ShootRainbow(){
        Instantiate(rainbowBall, originalPlayer.transform.position, Quaternion.identity);
    }

    public void ShootFire(){
        Instantiate(fireBall, originalPlayer.transform.position, Quaternion.identity);
    }

    public void ShootIce(){
        Instantiate(iceBall, originalPlayer.transform.position, Quaternion.identity);
    }
}
