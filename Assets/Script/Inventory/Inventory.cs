using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;
    
    public Inventory() {
        itemList = new List<Item>();
        
        //AddItem(new Item{ itemType = Item.ItemType.Knife, amount = 1});
        //AddItem(new Item{ itemType = Item.ItemType.Shield, amount = 1});
        //AddItem(new Item{ itemType = Item.ItemType.Mine, amount = 1});
    }
    
    private Item Sprite2Item(Sprite sprite) {
        switch (sprite.name) {
            default:
            case "knife_0": return new Item{ itemType = Item.ItemType.Knife, amount = 1};
            case "Shield": return new Item{ itemType = Item.ItemType.Shield, amount = 1};
            case "Mine": return new Item{ itemType = Item.ItemType.Mine, amount = 1};
            case "Sword": return new Item{ itemType = Item.ItemType.Sword, amount = 1};
        }            
    }
    
    public void AddItem(Item item) {
        itemList.Add(item);
    }
    
    public void AddSprite(Sprite sprite) {
        itemList.Add(Sprite2Item(sprite));
    }
    
    public List<Item> GetItemList() {
        return itemList;
    }
}
