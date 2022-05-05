using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHUD : MonoBehaviour
{
    public ButtonUI[] hotbar; 

    public void AddItem(InventoryItem newItem)
    {
        foreach(ButtonUI curr in hotbar)
        {
            if (curr.myItem == null || curr.myItem == newItem)
            {
                print("test");
                curr.myItem = newItem;
                curr.image.sprite = newItem.data.icon;
                curr.text.text = "x" + newItem.stackSize.ToString();
                return;
            }
        }
    }
}
