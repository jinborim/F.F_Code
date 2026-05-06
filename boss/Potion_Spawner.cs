using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Spawner : MonoBehaviour
{
    public GameObject[] Potions;
    public GameObject potion;

    public Vector3 Spawner1;
    public Vector3 Spawner2;
    public Vector3 Spawner3;

    public float delta = 0;
    public float interval = 5f;

    public int potionCount = 0;


    void Start()
    {
        this.gameObject.SetActive(false);
        Spawner1.Set(146, 2, 0);
        Spawner2.Set(156, 0, 0);
        Spawner3.Set(166, 2, 0);
    }

       void Update()
    {
        delta += Time.deltaTime;
        if (delta > interval)
        {
            if(potionCount == 0)
            {
                delta = 0;
                SpawnerChange();
            }
            
        }
    }

    void SpawnerChange()
    {
        PrefabChanger();
        int i = Random.Range(0, 3);
        switch (i)
        {
            case 0:
                Instantiate(potion, Spawner1, Quaternion.identity);
                potionCount += 1;
                break;
            case 1:
                Instantiate(potion, Spawner2, Quaternion.identity);
                potionCount += 1;
                break;
            case 2:
                Instantiate(potion, Spawner3, Quaternion.identity);
                potionCount += 1;
                break;

        }
    }

    void PrefabChanger()
    {
        int j = Random.Range(0, Potions.Length);
        potion = Potions[j];
    }
}
