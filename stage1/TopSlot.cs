using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TopSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,  IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{//IPointerClickHandler,

    public Inventory inventory;
    [SerializeField]
    public GameObject TopInventory_Parent;
    public TopSlot[] topslots_topslot;

    public Item item; // 획득한 아이템
    public GunType_selected gun;
    public GunSlot gunslot;
    public int itemCount; // 획득한 아이템의 개수
    public Image itemImage;  // 아이템의 이미지
    
    //public Image gunImage;
    public GameObject selected_bulletPrefab;
    public GunType_selected.Gun_Type gunType_;
    //public static bool is_drag=false;
    
    public bool is_top_drag = false;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    [SerializeField]
    private GameObject gun_SlotsParent;
    private GunSlot[] Gun_slot;

    [SerializeField]
    Transform Parent_transform;

    [SerializeField]
    private SelectedGunInventory gunInventory;

    private bulletTest bullet_change;




    //https://daily50.tistory.com/508 참고해서 마우스 오버 이벤트...(마우스 오버하면 해당 슬롯의 하위 Activate부분이 활성화되면서 시각적 효과를 줌)
    public void OnPointerEnter(PointerEventData eventData)
    {

        Parent_transform.GetChild(0).transform.Find("Slot_Activate").gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {


        Parent_transform.GetChild(0).transform.Find("Slot_Activate").gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null && gun != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                for (int i = 0; i < Gun_slot.Length; i++)
                {
                    if (Gun_slot[i].gun == null)
                    {
                        Gun_slot[i].AddGun(this.transform.GetComponent<TopSlot>().item, this.transform.GetComponent<TopSlot>().gun);
                        this.ClearSlot();
                        return;
                    }
                }
            }
        }
            
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.is_top_drag = true;
            TopDragSlot.instance.dragSlot = this;
            TopDragSlot.instance.DragSetImage(itemImage);
            TopDragSlot.instance.transform.position = eventData.position;
            

        }
    }

    // 마우스 드래그 중일 때 계속 발생하는 이벤트
    public void OnDrag(PointerEventData eventData)
    {

        if (item != null)
        {

            TopDragSlot.instance.transform.position = eventData.position;


        }

    }

    // 마우스 드래그가 끝났을 때 발생하는 이벤트
    public void OnEndDrag(PointerEventData eventData)
    {
        if (GunSlot.is_drop == true)
        {
            
            for(int i=0; i<Gun_slot.Length; i++)
            {
                if (Gun_slot[i].dropped_slot == true&&Gun_slot[i].item!=null)
                {
                    Item temp_item= Gun_slot[i].transform.GetComponent<GunSlot>().item;
                    GunType_selected temp_gun = Gun_slot[i].transform.GetComponent<GunSlot>().gun;

                    Gun_slot[i].GunChange(this.transform.GetComponent<TopSlot>().item, this.transform.GetComponent<TopSlot>().gun);
                    this.ClearSlot();
                    this.AddItem(temp_item, temp_gun);
                    Gun_slot[i].dropped_slot = false;

                    //gunInventory.AddGunSlot(this.transform.GetComponent<TopSlot>().item, this.transform.GetComponent<TopSlot>().gun);

                    
                }
                else if(Gun_slot[i].dropped_slot == true && Gun_slot[i].item == null)
                {
                    gunInventory.AddGunSlot(this.transform.GetComponent<TopSlot>().item, this.transform.GetComponent<TopSlot>().gun);
                    this.ClearSlot();
                    //GunSlot.is_drop = false;
                }
            }
            GunSlot.is_drop = false;
        }

        TopDragSlot.instance.SetColor(0);
        TopDragSlot.instance.dragSlot = null;
        
        
        //is_drag = false;

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (TopDragSlot.instance.dragSlot != null)
        {
            ChangeSlot();

        }
        this.is_top_drag = false; //탑 슬롯에서 탑슬롯으로 드래그했을 때 아무 의미 없이 false로 바꿔줌..
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;
        GunType_selected _tempgun = gun;

        AddItem(TopDragSlot.instance.dragSlot.item, TopDragSlot.instance.dragSlot.gun, TopDragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            TopDragSlot.instance.dragSlot.AddItem(_tempItem, _tempgun, _tempItemCount);
        } 
        else
        {
            TopDragSlot.instance.dragSlot.ClearSlot();
        }
            
    }


    private void Start()
    {
        go_CountImage.SetActive(false);
        //아무것도 안들어있는 인벤토리에는 숫자가 안뜨게끔..
        Gun_slot = gun_SlotsParent.GetComponentsInChildren<GunSlot>();
        Parent_transform.GetChild(0).transform.Find("Slot_Activate").gameObject.SetActive(false);
        bullet_change = GameObject.FindObjectOfType<bulletTest>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        TopInventory_Parent = inventory.Top_SlotsParent;
        topslots_topslot = TopInventory_Parent.GetComponentsInChildren<TopSlot>();
        //is_drag = false;


    }

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, GunType_selected _gun, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        gun = _gun;
        
        

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }

    }

    // 해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        gun = null;
        //gunImage.sprite = null;
        selected_bulletPrefab = null;
        gunType_ = 0;
        SetColor(0);


        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }
}
