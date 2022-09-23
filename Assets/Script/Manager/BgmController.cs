using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    public static BgmController instance;

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
        StartCoroutine(PlayBgm());
    }

    IEnumerator PlayBgm()
    {
        yield return new WaitForSeconds(waitTime);
        audioSource.Play();
    }

    public void PauseBgm()
    {
        audioSource.Pause();
    }

    public void ContinuePlayBgm()
    {
        audioSource.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
