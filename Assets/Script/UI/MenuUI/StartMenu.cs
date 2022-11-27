using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    [SerializeField] private AudioClip _compressClip, _uncompressClip, _hoverClip;
    [SerializeField] private AudioSource _source;
    
   public void OnPointerEnter(PointerEventData eventData)
    {
        _source.PlayOneShot(_hoverClip);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _source.PlayOneShot(_compressClip);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _source.PlayOneShot(_uncompressClip);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("LevelMenu");
    }
    
    /*
    public void Setting()
    {
        StartCoroutine(DelaySceneLoad("Settings_1"));
    }

    public void Back()
    {
        StartCoroutine(DelaySceneLoad("MainMenu"));
    }

    public void BetweenSettings()
    {
    
        StartCoroutine(DelaySceneLoad("Settings_2"));
    }
    */

/*
     IEnumerator DelaySceneLoad(string name)
 {
     yield return new WaitForSeconds(0.3f);
     SceneManager.LoadScene(name);
 }
 */
}
