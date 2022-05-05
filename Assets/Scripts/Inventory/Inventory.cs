 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;
    public List<InventoryItemData> allItems;
    public InventoryHUD inventoryHUD;
    public bool hasPickaxe;

    private void Awake()
    {
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
    }

    public void Add(InventoryItemData referenceData, int amount)
    {
        if (amount == 0)
            return;
        if(itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            //already exists add to stack
            value.AddToStack(amount);
            inventoryHUD.AddItem(value);

        } else {
            //create new item in dictionary
            InventoryItem newItem = new InventoryItem(referenceData,amount);
            itemDictionary.Add(referenceData, newItem);
            inventoryHUD.AddItem(newItem);
        }
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStackToStack();
            if (value.stackSize == 0)
            {
                itemDictionary.Remove(referenceData);
            }
        }
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if(itemDictionary.TryGetValue(referenceData,out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    
}
