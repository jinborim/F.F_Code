using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcChange : MonoBehaviour
{
     private Image img;
    private RectTransform rect;

    [SerializeField] private Sprite after_img;

    void Start()
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    public void ChangeImage()
    {
        if (img != null && after_img != null)
            img.sprite = after_img;

        if (rect != null)
            rect.localRotation = Quaternion.Euler(0f, 180f, 0f);
    }
}
