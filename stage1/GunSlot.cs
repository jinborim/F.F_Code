using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GunSlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지

    //private GunSlot[] gunslot;
    public GunType_selected gun; // 획득한 무기
    //public int itemCount; // 획득한 아이템의 개수
    public Image gunImage;  // 아이템의 이미지
    public GameObject selected_bulletPrefab;
    public GunType_selected.Gun_Type gunType_;
    //private keycode_selector key_selector;

    [SerializeField]
    private GameObject gun_SlotsParent;
    private GunSlot[] Gun_slot;

    private GunSlot gun_slot;
   
    private bulletTest bullet_change;


    [SerializeField]
    private GameObject top_SlotsParent;
    
    private TopSlot[] top_slot;

    private keycode_selector key_selector;
    public static bool is_drop = false;
    public bool dropped_slot = false;
    public bool activated_;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null && gun != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //Debug.Log(transform.GetComponent<GunSlot>().item);
                //우클릭 시 해당 슬롯의 아이템을 도로 topslot에 갖다놓음.
                for (int i = 0; i < top_slot.Length; i++)
                {
                    if (top_slot[i].item == null)
                    {
                        top_slot[i].AddItem(transform.GetComponent<GunSlot>().item, transform.GetComponent<GunSlot>().gun);
                        break;
                        //return;
                    }
                }

                //오른쪽 마우스를 클릭하면 해당 슬롯의 모든 gun정보와 프리팹과 활성화 상태 해제
                this.ClearGunSlot();
                transform.Find("select_Activate").gameObject.SetActive(false);
                bullet_change.bullet_changer_test(null);
                this.activated_ = false;



            }
        }
        
    }




    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gun != null)
        {
            //this.transform.Find("select_Activate").gameObject.SetActive(false);
            
            GunDragSlot.instance.dragSlot = this;
            GunDragSlot.instance.DragSetImage(gunImage);
            GunDragSlot.instance.transform.position = eventData.position;
            
        }
    }

    // 마우스 드래그 중일 때 계속 발생하는 이벤트
    public void OnDrag(PointerEventData eventData)
    {

        if (gun != null)
        {

            GunDragSlot.instance.transform.position = eventData.position;
            //Debug.Log("onDrag");


        }

    }

    // 마우스 드래그가 끝났을 때 발생하는 이벤트
    public void OnEndDrag(PointerEventData eventData)
    {
        
        GunDragSlot.instance.SetColor(0);
        GunDragSlot.instance.dragSlot = null;
  
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        is_drop = true;
        this.dropped_slot = true;
        
        

        if (GunDragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
            //bullet_change.bullet_changer_test(this.gun);//바꾼 자리가 활성화되어있다면...
        }
        
    }


    private void Awake()
    {
        activated_ = false;
        //key_selector = GameObject.FindObjectOfType<keycode_selector>();
        transform.Find("select_Activate").gameObject.SetActive(false);
        Gun_slot = gun_SlotsParent.GetComponentsInChildren<GunSlot>();
        top_slot = top_SlotsParent.GetComponentsInChildren<TopSlot>();
        bullet_change = GameObject.FindObjectOfType<bulletTest>();
        key_selector = GameObject.FindObjectOfType<keycode_selector>();
        is_drop = false;

    }

    private void Update()
    {
        
    }

    public void Gunslot_Renew()
    {
        for(int i=0; i<Gun_slot.Length; i++)
        {
            if ((Gun_slot[i].activated_ == true)&&Gun_slot[i].gun==null)
            {
                Gun_slot[i].transform.Find("select_Activate").gameObject.SetActive(false);
                bullet_change.bullet_changer_test(null);
            }
        }
    }



    public void AddGun(Item _item, GunType_selected _gun)
    {
        item = _item;
        //itemImage.sprite = item.itemImage;

        for (int i = 0; i < Gun_slot.Length; i++)
        {
            if (Gun_slot[i].gun != null && (Gun_slot[i].gunType_ == _gun.gun_Type))
            {
                //비어있지 않은 슬롯에 있는 gunType이 새로 저장할 gun의 gunType과 같을 때 : 원래 있던 부분을 지우고 새로운 gun을 드롭된 자리에 만듦
                Gun_slot[i].ClearGunSlot();
                GunChange(_item,_gun);
                if (Gun_slot[i].activated_ == true&&Gun_slot[i].gun!=null) //
                {
                    bullet_change.bullet_changer_test(_gun);
                }
                

            }
            else if (Gun_slot[i].gun != null && (Gun_slot[i].gunType_ != _gun.gun_Type))
            {
                //비어있지 않은 슬롯에 있는 gunType이 새로 저장할 gun과 다를 때: 그냥 추가
                GunChange(_item, _gun);

                
            }


            else
            {
                //비어있는 슬롯의 경우: 그냥 추가
                GunChange(_item, _gun);
                
            }

            

        }
        return;

    }

    public void GunChange(Item _item, GunType_selected _gun)
    {
        item = _item;
        //itemCount = _count;
        //itemImage.sprite = item.itemImage;

        //해당 슬롯의 총에 대한 정보를 전부 바꿔준다
        gun = _gun;
        gunImage.sprite = gun.BulletImage;
        selected_bulletPrefab = gun.BulletPrefab;
        gunType_ = gun.gun_Type;

        SetColor(1);
    }

    private void SetColor(float _alpha)
    {
        Color color = gunImage.color;
        color.a = _alpha;
        gunImage.color = color;
    }

    public void ClearGunSlot()
    {
        item = null;
        //itemCount = 0;
        //itemImage.sprite = null;
        //SetColor(0);
        //Debug.Log("clearGunSlot");
        gun = null;
        gunImage.sprite = null;
        selected_bulletPrefab = null;
        gunType_ = 0;
        SetColor(0);
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        GunType_selected _tempgun = gun;

        GunChange(GunDragSlot.instance.dragSlot.item, GunDragSlot.instance.dragSlot.gun);

        if (_tempgun != null)
        {
            GunDragSlot.instance.dragSlot.GunChange(_tempItem,_tempgun);
            

        }

        else
        {
            GunDragSlot.instance.dragSlot.ClearGunSlot();
        }
            
    }

}
