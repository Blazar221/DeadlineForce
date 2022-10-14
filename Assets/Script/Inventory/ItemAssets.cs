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
    
    [SerializeField] public Sprite knifeSprite;
    [SerializeField] public Sprite shieldSprite;
    [SerializeField] public Sprite mineSprite;
    [SerializeField] public Sprite swordSprite;
}
