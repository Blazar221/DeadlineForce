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
            case "Ice": return new Item{ itemType = Item.ItemType.Ice, amount = 1};
            case "Grass": return new Item{ itemType = Item.ItemType.Grass, amount = 1};
            case "Fire": return new Item{ itemType = Item.ItemType.Fire, amount = 1};
            case "Dark": return new Item{ itemType = Item.ItemType.Dark, amount = 1};
        }            
    }
    
    public void AddItem(Item item) {
        itemList.Add(item);
    }
    
    public void AddSprite(Sprite sprite) {
        itemList.Add(Sprite2Item(sprite));
    }
    
    public void RemoveItem(Item item) {
        itemList.Remove(item);
    }
    
    public void RemoveSprite(Sprite sprite) {
        itemList.Remove(Sprite2Item(sprite));
    }
    
    public List<Item> GetItemList() {
        return itemList;
    }
}
