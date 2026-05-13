using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunDragSlot : MonoBehaviour
{
    static public GunDragSlot instance;
    public GunSlot dragSlot;

    [SerializeField]
    public Image imagegun;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        imagegun.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = imagegun.color;
        color.a = _alpha;
        imagegun.color = color;
    }
}
