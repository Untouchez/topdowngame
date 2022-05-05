using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Player player;
    public InventoryItemData item;
    public int amount;
    public float moveSpeed;

    public bool pickedUp;
    public bool canPickUp;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
        player = Player.instance;
    }

    public void Update()
    {
        if (pickedUp)
            OnPickUp();
    }

    public void OnPickUp()
    {
        if (!canPickUp)
            return;
        pickedUp = true;
        transform.position = Vector3.Slerp(transform.position, player.transform.position + new Vector3(0,2, 0), moveSpeed*Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!pickedUp || !canPickUp)
            return;
        if (collision.transform.CompareTag("Player"))
        {
            player.inventory.Add(item, amount);
            Destroy(this.gameObject);
        }
    }
}
