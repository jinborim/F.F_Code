using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager_Test : MonoBehaviour
{
    public SelectedGunInventory gunventory;

    [SerializeField]
    private GameObject gun_SlotsParent;
    
    private GunSlot[] gun_slot;
    private bulletTest bullet_change;

    void Start()
    {
        gunventory = GameObject.FindObjectOfType<SelectedGunInventory>();
        gun_SlotsParent = gunventory.gun_SlotsParent;
        gun_slot = gun_SlotsParent.GetComponentsInChildren<GunSlot>();
        bullet_change = GameObject.FindObjectOfType<bulletTest>();

    }

    void Update()
    {
        Slot_Renew();
    }

    void Slot_Renew()
    {
        for (int i = 0; i < gun_slot.Length; i++)
        {
            
            if (gun_slot[i].activated_ == true && gun_slot[i].gun != null)
            {
                bullet_change.bullet_changer_test(gun_slot[i].gun);
            }
            if (gun_slot[i].activated_ == true && gun_slot[i].gun == null)
            {
                gun_slot[i].transform.Find("select_Activate").gameObject.SetActive(false);
                bullet_change.bullet_changer_test(null);
                gun_slot[i].activated_ = false;
            }
            
        }
    }

}
