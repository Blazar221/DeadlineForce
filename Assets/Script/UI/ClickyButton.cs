using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _source;
    
    [SerializeField] private string LevelMenu = "LevelMenu";
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressClip);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressClip);
    }
    
    public void IsClicked()
    {
        SceneManager.LoadScene(LevelMenu);
    }
}
