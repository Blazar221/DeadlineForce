using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Water,
        Fire,
        Grass,
        Rock,
    }
    public ItemType itemType;
    public int amount;
    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Water:
                return ItemAssets.Instance.waterSprite;
            case ItemType.Grass:
                return ItemAssets.Instance.grassSprite;
            case ItemType.Fire:
                return ItemAssets.Instance.fireSprite;
            case ItemType.Rock:
                return ItemAssets.Instance.rockSprite;
        }
    }
}
