using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemCountText;
    public int itemCount;
    Item item;
    public LayerMask spawnLayerMask;

    public static InventorySlot instance;

    public void Awake()
    {
        instance = this;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
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

    public void UseItem()
    {
        if (item != null)
        {
            if (Inventory.instance.itemCounts.ContainsKey(item.name))
            {
                Inventory.instance.itemCounts[item.name]--;
                itemCount = Inventory.instance.itemCounts[item.name];
            }
            Vector2 spawnPosition = new Vector2(Random.Range(-7.4f, -4.5f), Random.Range(-3, 3.8f));  // x0, y0
            item.Food = Instantiate(item.Food, spawnPosition, Quaternion.identity);

            if (itemCount <= 0)
            {
                Inventory.instance.Remove(item);
                Debug.Log("Item removed");
            }
            else
            {
                UpdateItemCountText();
            }
        }
    }
    public void UpdateItemCountText()
    {

        itemCountText.text = "X" + itemCount.ToString();
        bool textActive = itemCount > 1;
        itemCountText.gameObject.SetActive(textActive);
    }
}

