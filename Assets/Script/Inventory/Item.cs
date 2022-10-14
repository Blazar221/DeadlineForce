using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Knife,
        Shield,
        Mine,
        Sword,
    }
    public ItemType itemType;
    public int amount;
    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Knife:
                return ItemAssets.Instance.knifeSprite;
            case ItemType.Shield:
                return ItemAssets.Instance.shieldSprite;
            case ItemType.Mine:
                return ItemAssets.Instance.mineSprite;
            case ItemType.Sword:
                return ItemAssets.Instance.swordSprite;
        }
    }
}
