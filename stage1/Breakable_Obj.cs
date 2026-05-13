using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Obj : MonoBehaviour
{
    [SerializeField]
    GameObject Drop_prefap;
    private SpriteRenderer Sprite_;
    public SoundEffect_Manager soundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            soundEffect.Effect_Sound("ITEMBREAK");
            GameObject drop_item = Instantiate(Drop_prefap, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        Sprite_ = GetComponent<SpriteRenderer>();
        soundEffect = GameObject.FindObjectOfType<SoundEffect_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
