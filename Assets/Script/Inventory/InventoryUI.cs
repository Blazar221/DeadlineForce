using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform slotContainer;
    private Transform slot;
    
    private void Awake() {
        slotContainer = transform.Find("SlotContainer");
        slot = slotContainer.Find("Slot");
    }
    
    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        RefreshInventoryItems();
    }
    
    private void RefreshInventoryItems() {
        float x = -1.5f;
        float slotCellSize = 100f;
        foreach (Item item in inventory.GetItemList()) {
            //if (x > 4)  
            RectTransform slotRectTransform = Instantiate(slot, slotContainer).GetComponent<RectTransform>();
            slotRectTransform.gameObject.SetActive(true);
            slotRectTransform.anchoredPosition = new Vector2(x * slotCellSize, 0);
            Image image = slotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            //SpriteRenderer spriteRenderer = slotRectTransform.Find("Sprite").GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = item.GetSprite();
            x++;
        }
    }
}
