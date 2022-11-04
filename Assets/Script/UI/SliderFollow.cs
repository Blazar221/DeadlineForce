using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFollow : MonoBehaviour
{
    public Slider slider;
    private Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 upsideOffset = new Vector3(0, -1, 0);

    void Update()
    {
        bool isUpsideDown = transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale < 0;
        if(isUpsideDown)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + upsideOffset);        
        }
        else
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);        
        }
    }


}
