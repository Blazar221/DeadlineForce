using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Ice,
        Grass,
        Fire,
        Dark,
    }
    public ItemType itemType;
    public int amount;
    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Ice:
                return ItemAssets.Instance.iceSprite;
            case ItemType.Grass:
                return ItemAssets.Instance.grassSprite;
            case ItemType.Fire:
                return ItemAssets.Instance.fireSprite;
            case ItemType.Dark:
                return ItemAssets.Instance.darkSprite;
        }
    }
}
