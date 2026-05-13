using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    public CharacterMovement character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            character.DIE();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
