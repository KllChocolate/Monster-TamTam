using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseInventory : MonoBehaviour
{
    private Toggle toggle;
    public GameObject inventoryUI;
    public void Start()
    {
        toggle = GetComponent<Toggle>();

    }

    public void Update()
    {
        if (toggle.isOn)
        {
            inventoryUI.SetActive(true);
            InventoryUI.instance.UpdateUI();
        }
        else inventoryUI.SetActive(false);
    }
}
