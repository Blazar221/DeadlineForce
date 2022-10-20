using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance {
        get;
        private set;
    }
    
    private void Awake() {
        Instance = this;
    }
    
    [SerializeField] public Sprite waterSprite;
    [SerializeField] public Sprite fireSprite;
    [SerializeField] public Sprite grassSprite;
    [SerializeField] public Sprite rockSprite;
    [SerializeField] public Sprite RBSprite;
    [SerializeField] public Sprite RGSprite;
    [SerializeField] public Sprite RYSprite;
    [SerializeField] public Sprite BGSprite;
    [SerializeField] public Sprite BYSprite;
    [SerializeField] public Sprite GYSprite;
}
