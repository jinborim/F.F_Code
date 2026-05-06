using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{

    private Rigidbody2D monsterRd;
    public int nextMove; 
    public float speed = 2f;
    public int M_healtㅗ;

    public bool is_endpoint; 
    public HP_Manager hp_manger; 

    public Monster monster_; 
    public CharacterMovement character; 
    public Boss_Movement Boss; 

    private void Awake()
    {
        monsterRd = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        is_endpoint = false;
        
        hp_manger = GameObject.FindObjectOfType<HP_Manager>();
        character = GameObject.FindObjectOfType<CharacterMovement>();
        Boss = GameObject.FindObjectOfType<Boss_Movement>();

        if (monster_ != null)
        {
            M_health = monster_.M_health;
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleDamage(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleDamage(collision.gameObject);
    }

    private void HandleDamage(GameObject target)
    {
        if (!target.CompareTag("Character"))
            return;

        if (character == null || character.is_Beat)
            return;

        if (hp_manager == null || monster_ == null)
            return;

        hp_manager.Damaged(monster_.damage);
    }

    public void health_manager(int damage)
    {
        M_health -= damage;

        if (M_health <= 0)
        {
            if (Boss != null)
            {
                Boss.Rest_count -= 1;
            }

            Destroy(gameObject);
        }
    }
}
