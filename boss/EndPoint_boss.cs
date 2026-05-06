using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint_boss : MonoBehaviour
{
    public BoxCollider2D End_collider;
  
    void Start()
    {
        End_collider = GetComponent<BoxCollider2D>();
    }
}
