using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemCountText;
    public int itemCount;
    public GameObject itemFoodPrefab;
    public Item item;
    public static InventorySlot instance;

    private GameObject itemFood;

    public void Awake()
    {
        instance = this;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemFoodPrefab = item.Food;
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
        itemFoodPrefab = null;
        itemCountText.enabled = false;
    }
    public void RemoveItemformInventory()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            itemFood = Instantiate(itemFoodPrefab, mousePos, Quaternion.identity);
            itemFood.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            Inventory.instance.itemCounts[item.name]--;
            itemCount = Inventory.instance.itemCounts[item.name];
        }
        if (itemCount <= 0)
        {
            Inventory.instance.Remove(item);
            ClearSlot();
        }
        else
        {
            UpdateItemCountText();
        }
        

    }

    public void UpdateItemCountText()
    {

        itemCountText.text = "X" + itemCount.ToString();
        bool textActive = itemCount > 1;
        itemCountText.gameObject.SetActive(textActive);
    }
}

