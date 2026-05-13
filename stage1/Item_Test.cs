using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Test : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item")|| collision.gameObject.CompareTag("Gun"))
        {
            theInventory.AcquireItem(collision.transform.GetComponent<ItemPickUp>().item, collision.transform.GetComponent<ItemPickUp>().gun);
        }
    }
            

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
