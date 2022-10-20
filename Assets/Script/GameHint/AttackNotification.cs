using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Attacknotification : MonoBehaviour
{
    public static Attacknotification Instance;

    public float blinkSpeed;
    private bool isAddAlpha;
    private float timer;
    public float timeval = 1;

    private Text t;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
       t  = GetComponent<Text>();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (isAddAlpha)
        {
            t.color += new Color(0, 0, 0, Time.deltaTime * blinkSpeed);
            if (timer >= timeval)
            {
                t.color = new Color(t.color.r, t.color.g, t.color.b, 1);
                isAddAlpha = false;
                timer = 0;
            }
        }
        else
        {
            t.color -= new Color(0, 0, 0, Time.deltaTime * blinkSpeed);
            if (timer >= timeval)
            {
                t.color = new Color(t.color.r,t.color.g, t.color.b, 0);
                isAddAlpha = true;
                timer = 0;
            }
        }
    }

}
