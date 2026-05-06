using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDead : MonoBehaviour
{
    private PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public void Call0()
    {
        if (player != null)
            StartCoroutine(player.Scene33(true));
    }
}
