using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCounter : MonoBehaviour{

    public Text numOffood_text;
    // Start is called before the first frame update
    void Start()
    {
        numOffood_text.text = PlayerControl.numOfFood.ToString() + "  " + "Diamonds";
        
    }

    // Update is called once per frame
    void Update()
    {
        numOffood_text.text = PlayerControl.numOfFood.ToString() + "  " + "Diamonds";
        // Debug.Log(numOffood_text.text);
    }
}
