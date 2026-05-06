using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gover_Scene : MonoBehaviour
{
    [SerializeField] public GameObject Status; // 부모 오브젝트 (Inspector에서 할당)
    public GameObject[] Status_child;

    void Start()
    {
     if (Status == null) return;

        int childCount = Status.transform.childCount;
        Status_child = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Status_child[i] = Status.transform.GetChild(i).gameObject;
        }
    }

    public void ActiveFalse()
    {
        if (Status_child == null) return;

        for (int i = 0; i < Status_child.Length; i++)
        {
            Status_child[i].SetActive(false);
        }
    }
}
