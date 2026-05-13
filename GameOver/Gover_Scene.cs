using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gover_Scene : MonoBehaviour
{
    public GameObject Status;
    public GameObject[] Status_child;
    // Start is called before the first frame update
    void Start()
    {
        Status = GameObject.Find("STATUS");
        Status_child = new GameObject[Status.transform.childCount];
        for(int i=0; i < Status.transform.childCount; i++)
        {
            Status_child[i] = Status.transform.GetChild(i).gameObject;
        }
        
    }

    public void ActiveFalse()
    {
        for (int i = 0; i < Status_child.Length; i++)
        {
            
            Status_child[i].transform.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
