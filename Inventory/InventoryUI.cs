using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{//‰«È‡æ‘Ë¡‰Õ‡∑Á¡≈ßslot
    public static InventoryUI instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Inventory.instance.onItemChangedCallback += UpdateUI;
        
    }
    public void UpdateUI()
    {
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length ; i++)
        { 
            if(i< Inventory.instance.items.Count)
            {
                slots[i].AddItem(Inventory.instance.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        } 
    }
}
