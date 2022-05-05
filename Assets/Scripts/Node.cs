using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Health
{
    public InventoryItemData stone;
    Inventory inventory;

    public int amount;

    public void Start()
    {
        inventory = Player.instance.inventory;
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }

    public override void TakeDamage(int damage, Transform other)
    {
        int temp;
        if(amount * damage > currentHealth)
            temp = currentHealth;
        else
            temp = amount * damage;
        inventory.Add(stone, temp);
        currentHealth -= temp;
        if (currentHealth <= 0)
            Die();
    }

}
