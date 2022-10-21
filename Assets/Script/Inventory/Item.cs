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
        RB,
        RG,
        RY,
        BG,
        BY,
        GY
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
            case ItemType.RB:
                return ItemAssets.Instance.RBSprite;
            case ItemType.RG:
                return ItemAssets.Instance.RGSprite;
            case ItemType.RY:   
                return ItemAssets.Instance.RYSprite;
            case ItemType.BG:   
                return ItemAssets.Instance.BGSprite;
            case ItemType.BY:   
                return ItemAssets.Instance.BYSprite; 
            case ItemType.GY:   
                return ItemAssets.Instance.GYSprite;
        }
    }
}
