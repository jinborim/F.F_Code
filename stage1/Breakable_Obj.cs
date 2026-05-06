using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Obj : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;

    private SoundEffect_Manager soundEffect;

    void Start()
    {
        soundEffect = FindObjectOfType<SoundEffect_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;

        if (soundEffect != null)
            soundEffect.Effect_Sound("ITEMBREAK");

        if (dropPrefab != null)
            Instantiate(dropPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
