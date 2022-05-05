using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{

    public InventoryItemData data;
    public int stackSize;

    public InventoryItem(InventoryItemData source, int amount)
    {
        data = source;
        AddToStack(amount);
    }

    public void AddToStack(int amount)
    {
        stackSize+=amount;
    }

    public void RemoveFromStackToStack()
    {
        stackSize--;
    }
}
