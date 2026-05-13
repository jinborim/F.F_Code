using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Heart : MonoBehaviour
{
    public GameObject hp_heart;
    public Image hp_Heart;
    public float filled_amount;
    public int Heart_Health = 100;

    // Start is called before the first frame update
    void Start()
    {
        hp_heart = this.gameObject;
        hp_Heart = hp_heart.GetComponent<Image>();
        //filled_amount = hp_Heart.fillAmount;
    }
    private void Awake()
    {
        //hp_heart.GetComponent<Image>().enabled = true;
        Heart_Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
