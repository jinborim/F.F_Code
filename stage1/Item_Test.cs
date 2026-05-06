using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Test : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Item / Gun 둘 다 처리
        if (!collision.CompareTag("Item") && !collision.CompareTag("Gun"))
            return;

        // ItemPickUp은 항상 존재한다는 전제지만, 구조적으로 안전하게 1번만 가져옴
        ItemPickUp pickup = collision.GetComponent<ItemPickUp>();

        if (pickup == null)
            return;

        // 인벤토리에 아이템 전달
        theInventory.AcquireItem(pickup.item, pickup.gun);

        // 아이템 먹었으면 제거
        collision.gameObject.SetActive(false);
        // Destroy(collision.gameObject);
    }
}
