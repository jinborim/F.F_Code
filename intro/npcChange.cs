using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcChange : MonoBehaviour
{
    public Image img;
    public Sprite after_img;

    public RectTransform rect;


    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeImage()
    {
        //img.sprite = after_img;
        rect.eulerAngles= new Vector3(0,180,0);
    }
}
