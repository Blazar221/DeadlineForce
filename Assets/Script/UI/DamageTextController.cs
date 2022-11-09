using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    public float speed = 20;
    // Use this for initialization
    void Start () {
    //
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update () 
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
