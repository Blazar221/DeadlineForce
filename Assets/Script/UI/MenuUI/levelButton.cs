using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    [SerializeField] private AudioClip _compressClip, _uncompressClip, _hoverClip;
    [SerializeField] private AudioSource _source;
    [SerializeField] private string nextScene;
    
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
    
    public void IsClicked()
    {
        SceneManager.LoadScene(nextScene);
    }
}
