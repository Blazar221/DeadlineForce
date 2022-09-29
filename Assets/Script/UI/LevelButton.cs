using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _source;
    
    [SerializeField] private string ChosenLevel;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _source.PlayOneShot(_compressClip);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _source.PlayOneShot(_uncompressClip);
    }
    
    public void IsClicked()
    {
        SceneManager.LoadScene(ChosenLevel);
        Time.timeScale = 1f;
    }
}
