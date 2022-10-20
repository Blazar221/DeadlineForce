using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    
    private List<Item> itemList;
    
    public Inventory() {
        itemList = new List<Item>();
        
        //AddItem(new Item{ itemType = Item.ItemType.Knife, amount = 1});
        //AddItem(new Item{ itemType = Item.ItemType.Shield, amount = 1});
        //AddItem(new Item{ itemType = Item.ItemType.Mine, amount = 1});
    }
    
    private Item Sprite2Item(Sprite sprite) {
        Debug.Log("Sprite2Item" + sprite.name);
        switch (sprite.name) {
            default:
            case "Water": return new Item{ itemType = Item.ItemType.Water, amount = 1};
            case "Fire": return new Item{ itemType = Item.ItemType.Fire, amount = 1};
            case "Grass": return new Item{ itemType = Item.ItemType.Grass, amount = 1};
            case "Rock": return new Item{ itemType = Item.ItemType.Rock, amount = 1};
            case "RB": return new Item{ itemType = Item.ItemType.RB, amount = 1};
            case "RG": return new Item{ itemType = Item.ItemType.RG, amount = 1};
            case "RY": return new Item{ itemType = Item.ItemType.RY, amount = 1};
            case "BG": return new Item{ itemType = Item.ItemType.BG, amount = 1};
            case "BY": return new Item{ itemType = Item.ItemType.BY, amount = 1};
            case "GY": return new Item{ itemType = Item.ItemType.GY, amount = 1};
        }            
    }
    
    public bool isEmpty(){
        return itemList.Count <= 0;
    }

    public void AddItem(Item item) {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void AddSprite(Sprite sprite) {
        itemList.Add(Sprite2Item(sprite));
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void RemoveItem(Item item) {
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void RemoveSprite(Sprite sprite) {
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == Sprite2Item(sprite).itemType) {
                itemInInventory = inventoryItem;
            }
        }
        itemList.Remove(itemInInventory);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public Item RemoveFirst() {
        Item itemInInventory = itemList[0];
        itemList.Remove(itemInInventory);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return itemInInventory;
    }
    
    public List<Item> GetItemList() {
        return itemList;
    }
}
