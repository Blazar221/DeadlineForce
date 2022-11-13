using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSliderFollow : MonoBehaviour
{
    public Slider healthSlider;

    private Vector3 offset = new Vector3(0, 1.45f, 0);
    private Vector3 upsideOffset = new Vector3(0, -1.9f, 0);

    void Update()
    {
        bool isUpsideDown = transform.parent.gameObject.transform.localScale.y < 0;
        if(isUpsideDown)
        {
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + upsideOffset);        
        }
        else
        {
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);        
        }
    }
}
