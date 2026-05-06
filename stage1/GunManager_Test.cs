using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager_Test : MonoBehaviour
{
     public SelectedGunInventory gunventory;

    [SerializeField] private GameObject gunSlotsParent;

    private GunSlot[] gunSlots;
    private bulletTest bulletChanger;

    private GunSlot currentSlot; // 현재 선택된 슬롯 캐싱

    void Start()
    {
        gunventory = GameObject.FindObjectOfType<SelectedGunInventory>();
        gunSlotsParent = gunventory.gun_SlotsParent;

        gunSlots = gunSlotsParent.GetComponentsInChildren<GunSlot>();
        bulletChanger = GameObject.FindObjectOfType<bulletTest>();
    }

    void Update()
    {
        UpdateSelectedSlot();
    }

    private void UpdateSelectedSlot()
    {
        GunSlot activeSlot = GetActiveSlot();

        // 선택된 슬롯이 바뀌지 않았다면 아무것도 하지 않음
        if (activeSlot == currentSlot) return;

        currentSlot = activeSlot;

        ApplyGun(currentSlot);
    }

    private GunSlot GetActiveSlot()
    {
        for (int i = 0; i < gunSlots.Length; i++)
        {
            if (gunSlots[i].activated_)
                return gunSlots[i];
        }

        return null;
    }

    private void ApplyGun(GunSlot slot)
    {
        if (slot == null || slot.gun == null)
        {
            ClearSlot(slot);
            bulletChanger.bullet_changer_test(null);
            return;
        }

        bulletChanger.bullet_changer_test(slot.gun);
    }

    private void ClearSlot(GunSlot slot)
    {
        if (slot == null) return;

        Transform select = slot.transform.Find("select_Activate");
        if (select != null)
            select.gameObject.SetActive(false);

        slot.activated_ = false;
    }
}
