using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedGunInventory : MonoBehaviour
{
    public GameObject gun_SlotsParent;

    private GunSlot[] gun_slot;
    private keycode_selector key_selector;
    private bulletTest bullet_change;

    void Start()
    {
        
        key_selector = GameObject.FindObjectOfType<keycode_selector>();
        gun_slot = gun_SlotsParent.GetComponentsInChildren<GunSlot>();
        bullet_change= GameObject.FindObjectOfType<bulletTest>();
    }
    
     void Update()
    {
        Selected_slot_event();
    }
    
     void Selected_slot_event()
    {
        for (int i = 0; i < key_selector.keyCodes.Length; i++)
        {
            if (!Input.GetKeyDown(key_selector.keyCodes[i])) continue;

            // 범위 체크
            if (i >= gun_slot.Length)
            {
                DeselectAll();
                bullet_change.bullet_changer_test(null);
                return;
            }

            // 전체 초기화
            DeselectAll();

            // 선택 처리
            if (gun_slot[i].gun != null)
            {
                gun_slot[i].activated_ = true;
                gun_slot[i].transform.Find("select_Activate")?.gameObject.SetActive(true);

                bullet_change.bullet_changer_test(gun_slot[i].gun);
            }
            else
            {
                bullet_change.bullet_changer_test(null);
            }
        }
    }

    void DeselectAll()
    {
        for (int k = 0; k < gun_slot.Length; k++)
        {
            gun_slot[k].activated_ = false;
            gun_slot[k].transform.Find("select_Activate")?.gameObject.SetActive(false);
        }
    }

    public void AddGunSlot(Item item, GunType_selected gun)
    {
        if (item == null || gun == null) return;

        if (item.itemType != Item.ItemType.Equipment) return;

        foreach (var slot in gun_slot)
        {
            if (!slot.dropped_slot) continue;

            slot.dropped_slot = false;
            slot.AddGun(item, gun);
            return;
        }
    }
}
