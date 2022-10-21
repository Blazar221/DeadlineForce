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
        
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        
        RefreshInventoryItems();
    }
    
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }
    
    private void RefreshInventoryItems() {
        foreach (Transform child in slotContainer) {
            if (child == slot) continue;
            Destroy(child.gameObject);
        }
        float x = -2.5f;
        float slotCellSize = 100f;
        foreach (Item item in inventory.GetItemList()) {
            //if (x > 4)  
            RectTransform slotRectTransform = Instantiate(slot, slotContainer).GetComponent<RectTransform>();
            slotRectTransform.gameObject.SetActive(true);
            slotRectTransform.anchoredPosition = new Vector2(x * slotCellSize, 0);
            Image image = slotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            if(image.sprite.name == "Rock"){
                Debug.Log("change Rock color");
                image.color = new Color(0.58f, 0.3f, 0f, 1f);
            }
            //SpriteRenderer spriteRenderer = slotRectTransform.Find("Sprite").GetComponent<SpriteRenderer>();
            //spriteRenderer.sprite = item.GetSprite();
            x++;
        }
    }
}
