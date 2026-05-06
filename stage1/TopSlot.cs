using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TopSlot : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Item Data")]
    public Item item;
    public GunType_selected gun;
    public int itemCount;

    [Header("UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Text text_Count;
    [SerializeField] private GameObject go_CountImage;

    [SerializeField] private Transform parentTransform;

    [Header("References")]
    private Inventory inventory;
    private SelectedGunInventory gunInventory;
    private GunSlot[] gunSlots;

    private bool isDragging = false;

    #region Init

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        gunInventory = FindObjectOfType<SelectedGunInventory>();

        gunSlots = inventory != null
            ? inventory.GetComponentsInChildren<GunSlot>()
            : null;

        go_CountImage.SetActive(false);

        if (parentTransform != null)
        {
            Transform t = parentTransform.GetChild(0).Find("Slot_Activate");
            if (t != null) t.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Pointer Events

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetHighlight(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null || gun == null) return;
        if (eventData.button != PointerEventData.InputButton.Right) return;

        ReturnToGunSlot();
        ClearSlot();
    }

    #endregion

    #region Drag

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) return;

        isDragging = true;

        TopDragSlot.instance.dragSlot = this;
        TopDragSlot.instance.DragSetImage(itemImage);
        TopDragSlot.instance.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || item == null) return;

        TopDragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (TopDragSlot.instance != null)
        {
            TopDragSlot.instance.SetColor(0);
            TopDragSlot.instance.dragSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (TopDragSlot.instance.dragSlot == null) return;

        SwapSlot(TopDragSlot.instance.dragSlot);
    }

    #endregion

    #region Core Logic

    private void SwapSlot(TopSlot from)
    {
        if (from == null) return;

        Item tempItem = item;
        GunType_selected tempGun = gun;
        int tempCount = itemCount;

        AddItem(from.item, from.gun, from.itemCount);

        if (tempItem != null)
        {
            from.AddItem(tempItem, tempGun, tempCount);
        }
        else
        {
            from.ClearSlot();
        }
    }

    private void ReturnToGunSlot()
    {
        if (gunSlots == null) return;

        foreach (var slot in gunSlots)
        {
            if (slot.gun == null)
            {
                slot.AddGun(item, gun);
                ClearSlot();
                return;
            }
        }
    }

    #endregion

    #region Slot Control

    public void AddItem(Item _item, GunType_selected _gun, int _count = 1)
    {
        if (_item == null) return;

        item = _item;
        gun = _gun;
        itemCount = _count;

        if (itemImage != null)
            itemImage.sprite = _item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    public void ClearSlot()
    {
        item = null;
        gun = null;
        itemCount = 0;

        if (itemImage != null)
            itemImage.sprite = null;

        go_CountImage.SetActive(false);

        if (text_Count != null)
            text_Count.text = "0";

        SetColor(0);
    }

    #endregion

    #region UI

    private void SetHighlight(bool active)
    {
        if (parentTransform == null) return;

        Transform t = parentTransform.GetChild(0).Find("Slot_Activate");
        if (t != null)
            t.gameObject.SetActive(active);
    }

    private void SetColor(float alpha)
    {
        if (itemImage == null) return;

        Color c = itemImage.color;
        c.a = alpha;
        itemImage.color = c;
    }

    #endregion
}
