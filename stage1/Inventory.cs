using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public static bool inventoryActivated = false;

    [SerializeField] private GameObject inventoryBase;
    [SerializeField] private GameObject slotsParent;
    [SerializeField] private GameObject topSlotsParent;

    private Slot[] slots;
    private TopSlot[] topSlots;

    private void Awake()
    {
        // 싱글톤
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

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
        ToggleInventoryInput();
    }

    #region Inventory Toggle

    private void ToggleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        inventoryActivated = !inventoryActivated;
        inventoryBase.SetActive(inventoryActivated);
    }

    #endregion

    #region Item Logic

    public void AcquireItem(Item item, GunType_selected gun, int count = 1)
    {
        if (item.itemType == Item.ItemType.Equipment)
        {
            AddEquipment(item, gun, count);
        }
        else
        {
            AddItem(item, count);
        }
    }

    private void AddItem(Item item, int count)
    {
        // 기존 아이템 찾기 (스택)
        foreach (var slot in slots)
        {
            if (slot.item != null && slot.item.itemName == item.itemName)
            {
                slot.SetSlotCount(count);
                return;
            }
        }

        // 빈 슬롯 찾기
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.AddItem(item, count);
                return;
            }
        }
    }

    private void AddEquipment(Item item, GunType_selected gun, int count)
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

    #endregion
}
