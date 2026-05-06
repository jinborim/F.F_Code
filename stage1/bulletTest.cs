using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTest : MonoBehaviour
{
    GameObject Character;
    GameObject FIREGUN;
    
    public GameObject bulletPrefab;
    public GunType_selected gun;

    public SoundEffect_Manager soundEffect;

    public float bulletSpeed = 10.0f;

    private void Start()
    {
        Character = GameObject.Find("Character");
        soundEffect = GameObject.FindObjectOfType<SoundEffect_Manager>();
    }
    
    void Update()
    {
        bullet_func();
        
    }
    
    void bullet_func()
    {
        
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            if (bulletPrefab != null)
            {
                soundEffect.Effect_Sound("SHOOT");
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

                Rigidbody2D bb = bullet.GetComponent<Rigidbody2D>();

                if (Character.GetComponent<CharacterMovement>().left == true)
                {
                    bb.AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);
                }
                else if (Character.GetComponent<CharacterMovement>().right == true)
                {
                    bb.AddForce(Vector2.right * bulletSpeed, ForceMode2D.Impulse);
                }
            }
        }
    }

    public void bullet_changer_test(GunType_selected _gun)
    {

        gun = _gun;
        if (gun != null)
        {
            bulletPrefab = gun.BulletPrefab;
        }
        else
        {
            bulletPrefab = null;
        }
    }
}
