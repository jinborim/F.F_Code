using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoshoot : MonoBehaviour
{
    private Transform bulletSpawnPoint;

    [SerializeField] private GameObject bulletPrefab;
    private GameManager gamemanager;

    void Start()
    {
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");

        gamemanager = FindObjectOfType<GameManager>();

        StartCoroutine(AutoBullet());
    }

    public IEnumerator AutoBullet()
    {
        if (bulletPrefab == null) yield break;

        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPos = bulletSpawnPoint != null 
                ? bulletSpawnPoint.position 
                : transform.position;

            Instantiate(bulletPrefab, spawnPos, transform.rotation);

            yield return new WaitForSeconds(0.6f);
        }

        if (gamemanager != null)
            gamemanager.Scene22(true);
    }
}
