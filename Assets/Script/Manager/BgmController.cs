using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    public static BgmController instance;

    //Current song position, in seconds
    public float songPosition;

    //How many seconds have passed since the song started
    private float dspSongTime;

    public bool started = false;
    
    [SerializeField]
    AudioSource audioSource;

    [Range(0f, 2f)]
    [SerializeField]
    private float waitTime = 1f;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayBgm", waitTime);
    }

    void PlayBgm()
    {
        audioSource = GetComponent<AudioSource>();
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        Debug.Log("in music init:" + dspSongTime + "   "+ songPosition);
        audioSource.Play();
        started = true;
    }

    public void PauseBgm()
    {
        audioSource.Pause();
    }

    public void StopBgm()
    {
        audioSource.Stop();
    }

    public void ContinuePlayBgm()
    {
        audioSource.Play();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);
    }
}
