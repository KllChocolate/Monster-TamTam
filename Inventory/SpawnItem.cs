using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject itemFoodPrefab;
    public Item item;
    private GameObject itemFood;
    public bool isDragging = false;

    public void spawnItem()
    {
        item = InventorySlot.instance.item;
        itemFoodPrefab = InventorySlot.instance.itemFood;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        itemFood = Instantiate(itemFoodPrefab, mousePos, Quaternion.identity);
        isDragging = true;


        if (isDragging && itemFood)
        {
            Debug.Log("ลากเลย");
            itemFood.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }

        if (isDragging && Input.GetMouseButtonUp(0) && itemFood)
        {
            Debug.Log("ปล่อยเมาส์");
            isDragging = false;
            itemFood = null;
            Inventory.instance.itemCounts[item.name]--;
            InventorySlot.instance.itemCount = Inventory.instance.itemCounts[item.name];
        }

        if (InventorySlot.instance.itemCount <= 0 && Input.GetMouseButtonUp(0))
        {
            Inventory.instance.Remove(item);
            Debug.Log("Item removed");
        }
        else
        {
            InventorySlot.instance.UpdateItemCountText();
        }
    }
}
