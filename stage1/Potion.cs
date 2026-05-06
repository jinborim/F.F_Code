using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    Potion_Spawner potionSpawner;

    void Start()
    {
        potionSpawner = GameObject.FindObjectOfType<Potion_Spawner>();
        
    }

    

}
