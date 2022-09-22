using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [Range(0f, 2f)]
    [SerializeField]
    private float waitTime = 1f;

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

    void StopBgm()
    {
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
