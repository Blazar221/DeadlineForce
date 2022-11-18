using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFollow : MonoBehaviour
{
    public Slider healthSlider;

    private Vector3 hsOffset = new Vector3(0, 1, 0);
    private Vector3 hsUpsideOffset = new Vector3(0, -1.45f, 0);

    public Slider fireSlider;
    public Slider grassSlider;
    public Slider waterSlider;
    public Slider rockSlider;
    private List<Slider> energySliders;

    private float energyBarDistance = 0.65f;

    private List<Vector3> energySliderOffsets;
    private List<Vector3> energySliderUpsideDownOffsets;

    void Awake() {
        energySliders = new List<Slider>();
        energySliders.Add(fireSlider);
        energySliders.Add(grassSlider);
        energySliders.Add(waterSlider);
        energySliders.Add(rockSlider);
        
        Vector3 o1 = new Vector3(-1.5f*energyBarDistance, 1.45f, 0);
        Vector3 o2 = new Vector3(-0.5f*energyBarDistance, 1.45f, 0);
        Vector3 o3 = new Vector3(0.5f*energyBarDistance, 1.45f, 0);
        Vector3 o4 = new Vector3(1.5f*energyBarDistance, 1.45f, 0);

        Vector3 o5 = new Vector3(-1.5f*energyBarDistance, -1, 0);
        Vector3 o6 = new Vector3(-0.5f*energyBarDistance, -1, 0);
        Vector3 o7 = new Vector3(0.5f*energyBarDistance, -1, 0);
        Vector3 o8 = new Vector3(1.5f*energyBarDistance, -1, 0);

        energySliderOffsets = new List<Vector3>();
        energySliderOffsets.Add(o1);
        energySliderOffsets.Add(o2);
        energySliderOffsets.Add(o3);
        energySliderOffsets.Add(o4);

        energySliderUpsideDownOffsets = new List<Vector3>();
        energySliderUpsideDownOffsets.Add(o5);
        energySliderUpsideDownOffsets.Add(o6);
        energySliderUpsideDownOffsets.Add(o7);
        energySliderUpsideDownOffsets.Add(o8);
    }

    void Update()
    {
        // TODO change rg judge to transform judge
        bool isUpsideDown = transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale < 0;
        if(isUpsideDown)
        {
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + hsUpsideOffset);        
        
            for(int i=0;i<4;i++)
            {
                energySliders[i].transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + energySliderUpsideDownOffsets[i]);
            }
        }
        else
        {
            healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + hsOffset);        
            
            for(int i=0;i<4;i++)
            {
                energySliders[i].transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + energySliderOffsets[i]);
            }
        }
    }


}
