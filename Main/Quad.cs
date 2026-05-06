using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour
{
    private MeshRenderer render; 

    [SerializeField] private float speed;
    private float offset;
 
    void Start()
    { 
        render = GetComponent<MeshRenderer>(); 
    }
    
    void Update()
    {
        if (render == null) return;

        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, 0f); 
    }
}
