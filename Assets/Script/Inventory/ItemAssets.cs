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
    
    [SerializeField] public Sprite iceSprite;
    [SerializeField] public Sprite grassSprite;
    [SerializeField] public Sprite fireSprite;
    [SerializeField] public Sprite darkSprite;
}
