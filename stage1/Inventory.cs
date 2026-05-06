using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public static bool invectoryActivated;

    [SerializeField] private GameObject inventoryBase;
    [SerializeField] private GameObject slotsParent;
    [SerializeField] private GameObject topSlotsParent;

    private Slot[] slots;
    private TopSlot[] topSlots;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventoryBase.SetActive(false);

        slots = slotsParent.GetComponentsInChildren<Slot>();
        topSlots = topSlotsParent.GetComponentsInChildren<TopSlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invectoryActivated = !invectoryActivated;
            inventoryBase.SetActive(invectoryActivated);
        }
    }

    public void AcquireItem(Item item, GunType_selected gun, int count = 1)
    {
        if (item == null) return;

        // 장비가 아닌 경우 → stack 먼저
        if (item.itemType != Item.ItemType.Equipment)
        {
            foreach (var slot in slots)
            {
                if (slot.item != null && slot.item.itemName == item.itemName)
                {
                    slot.SetSlotCount(count);
                    return;
                }
            }
        }

        // 빈 슬롯 찾기
        if (item.itemType != Item.ItemType.Equipment)
        {
            foreach (var slot in slots)
            {
                if (slot.item == null)
                {
                    slot.AddItem(item, count);
                    return;
                }
            }
        }
        else
        {
            foreach (var slot in topSlots)
            {
                if (slot.item == null)
                {
                    slot.AddItem(item, gun, count);
                    return;
                }
            }
        }
    }
}
