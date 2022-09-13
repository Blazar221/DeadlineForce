using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public bool changeSmall, changeBig, easing;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (changeSmall)
        {
            if (easing)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.6f, 0.6f, 1f), .08f);
            }
        }
        if (Mathf.Abs(transform.localScale.x - 0.6f) <= 0.01f)
        {
            Debug.Log("Ring Changing Big");
            changeSmall = false;
            changeBig = true;
        }
        if (changeBig)
        {
            if (easing)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), .08f);
            }
        }
        if (Mathf.Abs(1f - transform.localScale.x) <= 0.01f)
        {
            changeBig = false;
        }
    }
    
    public void CallThisMethod()
    {
        Debug.Log("Ring Changing Small");
        changeSmall = true;
    }
}
