using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetTimer : MonoBehaviour
{
    public float timeLeft;

    private float fillSpeed;

    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        var frameNeedToComplete = timeLeft / Time.fixedDeltaTime;
        fillSpeed = 1 / frameNeedToComplete;
    }

    private void FixedUpdate()
    {
        if(slider.value > 0)
        {
            slider.value -= fillSpeed;
        }
    }
}
