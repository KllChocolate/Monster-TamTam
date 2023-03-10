using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item" , menuName = "Inventory/Item")] 
public class Item : ScriptableObject 
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool showInventory = true;
    public GameObject Food;

    private void OnEnable()
    {
        Food = Resources.Load<GameObject>(name);
    }
}
