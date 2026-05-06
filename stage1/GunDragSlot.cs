using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunDragSlot : MonoBehaviour
{
   public static GunDragSlot Instance;

    public GunSlot dragSlot;

    [SerializeField] private Image dragImage;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetDragImage(Image sourceImage)
    {
        if (sourceImage == null || dragImage == null) return;

        dragImage.sprite = sourceImage.sprite;
        SetAlpha(1f);
    }

    public void SetAlpha(float alpha)
    {
        if (dragImage == null) return;

        Color color = dragImage.color;
        color.a = alpha;
        dragImage.color = color;
    }

    public void Clear()
    {
        if (dragImage == null) return;

        dragImage.sprite = null;
        SetAlpha(0f);
    }
}
