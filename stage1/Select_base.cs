using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_base : MonoBehaviour
{
    public static GameObject select_base;

    [SerializeField]
    private GameObject select_base_obj;

    private SelectPanel[] selectPanel_ar;

    void Start()
    {
        selectPanel_ar = select_base_obj.GetComponentsInChildren<SelectPanel>();
    }
    private void Awake()
    {
        select_base = this.gameObject;
    }

}
