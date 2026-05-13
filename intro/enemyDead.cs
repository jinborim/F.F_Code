using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDead : MonoBehaviour
{
    public GameObject enemy;
    public PlayerMovement player;

    public float delta = 0;
    public float interval = 2.5f;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        
    }

    

    public void Call0()
    {
        StartCoroutine(player.Scene33(true));
    }

    

}

    
