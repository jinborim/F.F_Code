using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoshoot : MonoBehaviour
{
    private Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    //public enemyDead enemy;
    public GameManager gamemanager;


    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnPoint = this.transform.Find("BulletSpawnPoint");
        gamemanager = GameObject.FindObjectOfType<GameManager>();
        //enemy = GameObject.FindObjectOfType<enemyDead>();
        StartCoroutine(AutoBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AutoBullet()
    {
        for(int i = 0; i<3; i++)
        {
            //ÃÑ¾ËÀ» ¸¸µç´Ù 
            /* var bulletGo = Instantiate<GameObject>(this.bulletPrefab);
            bulletGo.transform.position = this.bulletSpawnPoint.position; */
            GameObject BulletGo = Instantiate(bulletPrefab, this.transform.position, transform.rotation);

            yield return new WaitForSeconds(0.6f);// + Á¶°Ç
        }
        gamemanager.Scene22(true);
        yield break;


    }
}
