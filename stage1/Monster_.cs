using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_ : MonoBehaviour
{
    public float StartHealth; // 최대 체력
    public float Health; //현재 체력
    
    private MonsterMovement monster_move; 

    [SerializeField]       
    public GameObject HealthBar; // 체력 바 UI

   private void Start()
    {
        monster_move = GetComponent<MonsterMovement>();

        if (monster_move != null)
        {
            StartHealth = monster_move.M_health;
            Health = StartHealth;
        }

        UpdateUI();
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, StartHealth);

        UpdateUI();

        if (Health <= 0)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        if (healthBar == null) return;

        healthBar.fillAmount = Health / StartHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
