using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GunSlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public GunType_selected gun;

    [SerializeField] private Image gunImage;

    [SerializeField] private GameObject gunSlotsParent;
    [SerializeField] private GameObject topSlotsParent;

    private GunSlot[] gunSlots;
    private TopSlot[] topSlots;

    private bulletTest bulletChanger;
    private keycode_selector keySelector;

    public bool activated_;
    public bool droppedSlot;

    public static bool isDrop;

    #region Unity

    private void Awake()
    {
        gunSlots = gunSlotsParent.GetComponentsInChildren<GunSlot>();
        topSlots = topSlotsParent.GetComponentsInChildren<TopSlot>();

        bulletChanger = GameObject.FindObjectOfType<bulletTest>();
        keySelector = GameObject.FindObjectOfType<keycode_selector>();

        SetSelected(false);
        isDrop = false;
    }

    #endregion

    #region Click

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;
        if (gun == null) return;

        ReturnToTopSlot();
        ClearSlot();

        bulletChanger.bullet_changer_test(null);
        activated_ = false;
    }

    private void ReturnToTopSlot()
    {
        foreach (var slot in topSlots)
        {
            if (slot.item != null) continue;

            slot.AddItem(item, gun);
            break;
        }
    }

    #endregion

    #region Drag

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gun == null) return;

        GunDragSlot.Instance.dragSlot = this;
        GunDragSlot.Instance.SetDragImage(gunImage);
        GunDragSlot.Instance.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gun == null) return;

        GunDragSlot.Instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GunDragSlot.Instance.SetAlpha(0f);
        GunDragSlot.Instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GunDragSlot.Instance.dragSlot == null) return;

        isDrop = true;
        droppedSlot = true;

        SwapSlot(GunDragSlot.Instance.dragSlot);
    }

    #endregion

    #region Slot Logic

    public void AddGun(Item newItem, GunType_selected newGun)
    {
        item = newItem;

        GunSlot sameTypeSlot = FindSameTypeSlot(newGun);

        if (sameTypeSlot != null)
        {
            sameTypeSlot.ClearSlot();
        }

        ApplyGun(newItem, newGun);

        if (activated_)
        {
            bulletChanger.bullet_changer_test(newGun);
        }
    }

    private GunSlot FindSameTypeSlot(GunType_selected newGun)
    {
        foreach (var slot in gunSlots)
        {
            if (slot.gun == null) continue;
            if (slot.gun.gun_Type == newGun.gun_Type)
                return slot;
        }

        return null;
    }

    private void ApplyGun(Item newItem, GunType_selected newGun)
    {
        item = newItem;
        gun = newGun;

        gunImage.sprite = newGun.BulletImage;
        SetAlpha(1f);
    }

    public void ClearSlot()
    {
        item = null;
        gun = null;

        gunImage.sprite = null;
        SetAlpha(0f);

        SetSelected(false);
    }

    private void SwapSlot(GunSlot from)
    {
        Item tempItem = item;
        GunType_selected tempGun = gun;

        ApplyGun(from.item, from.gun);

        if (tempGun != null)
            from.ApplyGun(tempItem, tempGun);
        else
            from.ClearSlot();
    }

    #endregion

    #region UI

    private void SetAlpha(float alpha)
    {
        Color color = gunImage.color;
        color.a = alpha;
        gunImage.color = color;
    }

    private void SetSelected(bool value)
    {
        activated_ = value;

        Transform select = transform.Find("select_Activate");
        if (select != null)
            select.gameObject.SetActive(value);
    }

    #endregion
}
