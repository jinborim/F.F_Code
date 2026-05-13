using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_ : MonoBehaviour
{
    public float StartHealth;
    public float Health;
    public MonsterMovement monster_move;

    public GameObject HealthBar;
    void Awake()
    {
        
    }

    private void Start()
    {
        monster_move = this.GetComponent<MonsterMovement>();
        StartHealth = monster_move.M_health;
        Health = monster_move.M_health;
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        HealthBar.GetComponent<Image>().fillAmount = Health / StartHealth;

    }


}
