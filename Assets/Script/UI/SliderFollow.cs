using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFollow : MonoBehaviour
{
    public Slider healthSlider;

    private Vector3 hsOffset = new Vector3(0, 1, 0);
    private Vector3 hsUpsideOffset = new Vector3(0, -1, 0);

    public Slider fireSlider;
    public Slider grassSlider;
    public Slider waterSlider;
    public Slider rockSlider;
    private List<Slider> energySliders;

    
    private Vector3 fsOffset = new Vector3(-3, -1f, 0);
    private Vector3 gsOffset = new Vector3(-3, -0.5f, 0);
    private Vector3 wsOffset = new Vector3(-3, 0, 0);
    private Vector3 rsOffset = new Vector3(-3, 0.5f, 0);
    private List<Vector3> energySliderOffsets;

    void Awake() {
        energySliders = new List<Slider>();
        energySliders.Add(fireSlider);
        energySliders.Add(grassSlider);
        energySliders.Add(waterSlider);
        energySliders.Add(rockSlider);
        
        energySliderOffsets = new List<Vector3>();
        energySliderOffsets.Add(fsOffset);
        energySliderOffsets.Add(gsOffset);
        energySliderOffsets.Add(wsOffset);
        energySliderOffsets.Add(rsOffset);
    }

    void Update()
    {
        bool isUpsideDown = transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale < 0;
        if(isUpsideDown)
        {
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + hsUpsideOffset);        
        }
        else
        {
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + hsOffset);        
        }
        for(int i=0;i<4;i++)
        {
            energySliders[i].transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + energySliderOffsets[i]);
        }
    }


}
