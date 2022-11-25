using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertController : MonoBehaviour
{
    public static AlertController Instance;
    
    [SerializeField] private GameObject Alert0;
    [SerializeField] private GameObject Alert1;
    [SerializeField] private GameObject Alert2;
    [SerializeField] private GameObject Alert3;
    
    public void StartAlert(int pos)
    {
        switch (pos)
        {
            case 0:
                Alert0.GetComponent<SpriteRenderer>().enabled = true;
                Alert0.GetComponent<Animator>().enabled = true;
                break;
            case 1:
                Alert1.GetComponent<SpriteRenderer>().enabled = true;
                Alert1.GetComponent<Animator>().enabled = true;
                break;
            case 2:
                Alert2.GetComponent<SpriteRenderer>().enabled = true;
                Alert2.GetComponent<Animator>().enabled = true;
                break;
            case 3:
                Alert3.GetComponent<SpriteRenderer>().enabled = true;
                Alert3.GetComponent<Animator>().enabled = true;
                break;
            default:
                break;
        }
    }
    
    public void EndAlert(int pos)
    {
        switch (pos)
        {
            case 0:
                Alert0.GetComponent<SpriteRenderer>().enabled = false;
                Alert0.GetComponent<Animator>().enabled = false;
                break;
            case 1:
                Alert1.GetComponent<SpriteRenderer>().enabled = false;
                Alert1.GetComponent<Animator>().enabled = false;
                break;
            case 2:
                Alert2.GetComponent<SpriteRenderer>().enabled = false;
                Alert2.GetComponent<Animator>().enabled = false;
                break;
            case 3:
                Alert3.GetComponent<SpriteRenderer>().enabled = false;
                Alert3.GetComponent<Animator>().enabled = false;
                break;
            default:
                break;
        }
    }
    
    public void EndAllAlert()
    {
        Alert0.GetComponent<SpriteRenderer>().enabled = false;
        Alert0.GetComponent<Animator>().enabled = false;
        Alert1.GetComponent<SpriteRenderer>().enabled = false;
        Alert1.GetComponent<Animator>().enabled = false;
        Alert2.GetComponent<SpriteRenderer>().enabled = false;
        Alert2.GetComponent<Animator>().enabled = false;
        Alert3.GetComponent<SpriteRenderer>().enabled = false;
        Alert3.GetComponent<Animator>().enabled = false;
    }
    
    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
