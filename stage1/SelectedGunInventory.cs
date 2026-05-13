using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedGunInventory : MonoBehaviour
{
    [SerializeField]
    public GameObject gun_SlotsParent;
    // ㄴ 슬롯을 담는 가장 배경 이미지
    [SerializeField]
    private GunSlot[] gun_slot;
    private GunType_selected gun_;
    private keycode_selector key_selector;
    //public bool is_activated;
    
    private bulletTest bullet_change;

    


    // Start is called before the first frame update

    void Start()
    {
        
        key_selector = GameObject.FindObjectOfType<keycode_selector>();
        gun_slot = gun_SlotsParent.GetComponentsInChildren<GunSlot>();
        bullet_change= GameObject.FindObjectOfType<bulletTest>();
    }

    public void AddGunSlot(Item _item, GunType_selected gun_)
    {
        if (_item != null && gun_ != null)
        {
            if (Item.ItemType.Equipment == _item.itemType)
            {
                for (int i = 0; i < gun_slot.Length; i++)
                {
                    if (gun_slot[i].dropped_slot == true)
                    {
                        gun_slot[i].dropped_slot = false;
                        gun_slot[i].AddGun(_item, gun_);


                    }
                }


            }
        }
        
        
    }




    void Selected_slot_event()
    {
        for (int j = 0; j < key_selector.keyCodes.Length; j++)
        {
            if (Input.GetKeyDown(key_selector.keyCodes[j]) && j < gun_slot.Length == true)
            {
                for (int k = 0; k < gun_slot.Length; k++)
                {
                    gun_slot[k].activated_ = false;
                    //키가 눌릴 때 모든 activated를 한번 초기화하기
                }

                for (int i = 0; i < gun_slot.Length; i++)
                {
                    
                    if (Input.GetKeyDown(key_selector.keyCodes[i]) && gun_slot[i].gun==true)
                    {
                        
                        gun_slot[i].transform.Find("select_Activate").gameObject.SetActive(true);
                        bullet_change.bullet_changer_test(gun_slot[i].transform.GetComponent<GunSlot>().gun);
                        gun_slot[i].activated_ = true; //현재 선택된 슬롯이 드래그되고 있는지를 확인할거임
                        //Debug.Log("총 활성화");
                    }
                    else
                    {
                        gun_slot[i].transform.Find("select_Activate").gameObject.SetActive(false);
                        //이게 슬롯 수만큼 무조건 돌기 때문에 선택되지 않은 슬롯까지 검색했을 때 (선택 안된 슬롯은) 비활성화 되게 해줘야함.
                        //참고로 이게 없어도 프리팹 변경은 제대로 되기 때문에 선택 슬롯 시각화 문제 빼면 큰 문제는 없음
                    }

                    if (Input.GetKeyDown(key_selector.keyCodes[i]) && gun_slot[i].gun != true)
                    {
                        //만약 슬롯 갯수와 맞는 숫자키가 눌렸지만 해당 슬롯이 활성화되어있지 않을 때
                        gun_slot[i].transform.Find("select_Activate").gameObject.SetActive(false);
                        bullet_change.bullet_changer_test(gun_slot[i].transform.GetComponent<GunSlot>().gun);
                        

                    }


                }

            }
            else if ((Input.GetKeyDown(key_selector.keyCodes[j]) && j >= gun_slot.Length) == true)
            {
                Debug.Log("바깥 숫자 키가 눌림");
                for (int k = 0; k < gun_slot.Length; k++)
                {
                    gun_slot[k].activated_ = false;
                    //키가 눌릴 때 모든 activated를 한번 초기화하기
                }

                //눌린 키가 슬롯 안의 숫자가 아닐 때
                for (int i = 0; i < gun_slot.Length; i++)
                {
                    gun_slot[i].transform.Find("select_Activate").gameObject.SetActive(false);
                    bullet_change.bullet_changer_test(gun_slot[i].transform.GetComponent<GunSlot>().gun);

                    //모든 슬롯의 하위 오브젝트(event_on: 활성화 상태를 나타낼 테두리)를 비활성화 시킨다.
                }

            }
        }



    }

    private void Update()
    {
        Selected_slot_event();
    }


}
