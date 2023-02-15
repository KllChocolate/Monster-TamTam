using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemCountText;
    public int itemCount;
    public GameObject itemFood;
    public Item item;
    public static InventorySlot instance;

    public void Awake()
    {
        instance = this;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemFood = item.Food;
        icon.sprite = item.icon;
        icon.enabled = true;
        itemCountText.enabled = true;
        if (Inventory.instance.itemCounts.ContainsKey(newItem.name))
        {
            itemCount = Inventory.instance.itemCounts[newItem.name];
        }
        else
        {
            itemCount = 1;
        }
        UpdateItemCountText();
    }
    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        itemCountText.enabled = false;
    }
    public void RemoveItemformInventory()
    {
        Inventory.instance.Remove(item);
    }

    public void UpdateItemCountText()
    {

        itemCountText.text = "X" + itemCount.ToString();
        bool textActive = itemCount > 1;
        itemCountText.gameObject.SetActive(textActive);
    }
}

