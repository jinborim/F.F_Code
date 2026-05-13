using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{
    Rigidbody2D monsterRd;
    public int nextMove;
    public float speed = 2f;
    public int M_health;

    public bool is_endpoint;
    public HP_Manager hp_manger;

    public Monster monster_;
    public CharacterMovement character;
    public Boss_Movement Boss;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //캐릭터는 기본적으로 isTrigger가 활성화되어있지 않기때문에 이 함수를 사용한다.
        if (collision.gameObject.CompareTag("Character"))
        {
            if (character.is_Beat == false)
            {
                if (hp_manger == null)
                {
                    hp_manger = GameObject.FindObjectOfType<HP_Manager>();
                }
                hp_manger.Damaged(monster_.damage);
                //StartCoroutine(character.OnBeatTime());

            }
            

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            if (character.is_Beat == false)
            {
                if (hp_manger == null)
                {
                    hp_manger = GameObject.FindObjectOfType<HP_Manager>();
                }
                hp_manger.Damaged(monster_.damage);
                //StartCoroutine(character.OnBeatTime());

            }


        }
        
    }

 
    public void health_manager(int damage)
    {
        this.M_health -= damage;
        if (this.M_health <= 0)
        {
            Destroy(gameObject);
            if (Boss != null)
            {
                Boss.Rest_count -= 1;
            }

        }
    }
   

    private void Start()
    {
        is_endpoint = false;
        hp_manger = GameObject.FindObjectOfType<HP_Manager>();
        M_health = monster_.M_health;
        character = GameObject.FindObjectOfType<CharacterMovement>();
        Boss = GameObject.FindObjectOfType<Boss_Movement>();
    }

    private void Awake()
    {
        monsterRd = GetComponent<Rigidbody2D>();
        //Invoke("Think", 5);//초기화 함수 안에 넣어서 실행될 때 마다 nextMove변수가 초기화됨
    }

}
