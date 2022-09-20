using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public float lifetime = 1.0f;

    /*
    public GameObject hitEffect, goodEffect, perfectEffect ,missEffect;
    drag prefabs into it in Unity Editor

    Use  Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
    to create an instance of the prefab

    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
