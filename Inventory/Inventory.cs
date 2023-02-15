using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int space = 12;
    public List<Item> items = new List<Item>();
    public Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public void Add(Item item)
    {
        if (item.showInventory)
        {
             if (items.Count >= space)
                 return;
             if (itemCounts.ContainsKey(item.name))
             {
                 itemCounts[item.name] += 1;
             }
             else
             {
                 items.Add(item);
                 itemCounts.Add(item.name, 1);
             }
             if (onItemChangedCallback != null)
             {
                 onItemChangedCallback.Invoke();
             }
        }
    }
    public void Remove(Item item)
    {
        if (itemCounts.ContainsKey(item.name))
        {
            itemCounts[item.name] -= 1;

            if (itemCounts[item.name] <= 0)
            {
                items.Remove(item);
                itemCounts.Remove(item.name);
            }
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

}
