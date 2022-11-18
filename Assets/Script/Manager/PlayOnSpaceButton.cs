using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnSpaceButton : MonoBehaviour
{
    public static PlayOnSpaceButton Instance;
    public AudioSource SpaceButtonSound;
    // Start is called before the first frame update
    void Awake()
    {
        Instance =this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void makeSound()
    {
        SpaceButtonSound.Play();
    }
}
