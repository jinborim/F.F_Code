using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static GameObject INVENTORY;
    public static bool invectoryActivated = false;  // 인벤토리 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 
    [SerializeField]
    public GameObject Top_SlotsParent;


    private Slot[] slots;  // 슬롯들 배열
    private TopSlot[] slot_top;

    private void Awake()
    {
        INVENTORY = this.gameObject;
    }

    void Start()
    {
        
        go_InventoryBase.SetActive(false);
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        slot_top = Top_SlotsParent.GetComponentsInChildren<TopSlot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invectoryActivated = !invectoryActivated;

            if (invectoryActivated)
            {
                OpenInventory();
                

            }

            else
            {
                CloseInventory();
            }
                

        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, GunType_selected _gun, int _count = 1) //무기가 아닌 경우 개수를 세줌..
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.itemName == _item.itemName) // 모든 슬롯을 검사해서 어떤 슬롯에 새 아이템과 같은 종류의 아이템이 있을때..
                    {
                        slots[i].SetSlotCount(_count); //현재 _count=1이므로 slot의 SetSlotCount에서 아이템 카운트를 1만큼 새로 올려줌
                        return;
                    }
                }
            }
        }

        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)//앞의 슬롯부터 차례로 검사해서 빈 슬롯이 있을 때
                {
                    slots[i].AddItem(_item, _count); // 해당 슬롯에 아이템을 넣어줌
                    return;
                }
            }
        }
        else if(Item.ItemType.Equipment == _item.itemType)
        {
            for (int i = 0; i < slot_top.Length; i++)
            {
                if (slot_top[i].item == null)//앞의 슬롯부터 차례로 검사해서 빈 슬롯이 있을 때
                {
                    slot_top[i].AddItem(_item, _gun, _count); // 해당 슬롯에 아이템을 넣어줌(총기류만 위로)
                    return;
                }
            }
        }
    }
}
