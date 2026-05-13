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

    // Start is called before the first frame update
    void Start()
    {
        gunventory = GameObject.FindObjectOfType<SelectedGunInventory>();
        gun_SlotsParent = gunventory.gun_SlotsParent;
        gun_slot = gun_SlotsParent.GetComponentsInChildren<GunSlot>();
        bullet_change = GameObject.FindObjectOfType<bulletTest>();

    }

    // Update is called once per frame
    void Update()
    {
        Slot_Renew();
    }

    void Slot_Renew()
    {
        for (int i = 0; i < gun_slot.Length; i++)
        {
            
            if (gun_slot[i].activated_ == true && gun_slot[i].gun != null)//버튼 활성화 된 곳에 아이템이 있다면 그 자리 프리팹으로 바꺼줌
            {
                bullet_change.bullet_changer_test(gun_slot[i].gun);
            }
            if (gun_slot[i].activated_ == true && gun_slot[i].gun == null)//버튼은 눌렸는데 그 자리에 아이템이 없음(무슨 이유든)
            {
                gun_slot[i].transform.Find("select_Activate").gameObject.SetActive(false);
                bullet_change.bullet_changer_test(null);
                gun_slot[i].activated_ = false;
            }
            
        }
    }

}
