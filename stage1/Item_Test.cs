using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Test : MonoBehaviour
{
   private Inventory inventory;

    void Start()
    {
        inventory = Inventory.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inventory == null) return;

        if (collision.CompareTag("Item") || collision.CompareTag("Gun"))
        {
            ItemPickUp pickUp = collision.GetComponent<ItemPickUp>();
            if (pickUp == null) return;

            inventory.AcquireItem(pickUp.item, pickUp.gun);
        }
    }
}
