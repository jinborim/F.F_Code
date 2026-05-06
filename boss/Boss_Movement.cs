using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movement : MonoBehaviour
{
    public GameObject[] attackPrefabSet;
    public GameObject attack_obj;

    public GameObject[] EnemyPrefab;
    public GameObject enemy_obj;

    public Animator Boss_ani;

    private HP_Manager hp_manger;
    public CharacterMovement character;
    public Monster monster_;

    public int oneShoting = 10;
    public float speed = 150f;
    public float interval = 5.0f;
    float delta = 0;
    public float RestHealth = 150f;

    public int Rest_count;

    bool isLeft = true;
    public bool is_movable;
    public bool is_movetime = true;
    public bool Boss_is_Beat;
    public float BeatTime = 1.5f;
    public float movespeed = 3f;
    
    private SpriteRenderer MonsterSprite;

    public SoundEffect_Manager soundEffect;
    public Boss_Music_Changer bossMusic;


    [SerializeField]
    private GameObject Drop_prefap;

    public EndPoint_boss[] endpoint_controll;

    void Start()
    {
        Boss_ani = GetComponent<Animator>();
        MonsterSprite = transform.GetComponent<SpriteRenderer>();
        
        hp_manger = GameObject.FindObjectOfType<HP_Manager>();
        character = GameObject.FindObjectOfType<CharacterMovement>();
        soundEffect = GameObject.FindObjectOfType<SoundEffect_Manager>();
        endpoint_controll = GameObject.FindObjectsOfType<EndPoint_boss>();
        bossMusic = GameObject.FindObjectOfType<Boss_Music_Changer>();

    }

    void Update()
    {
        if (Inventory.invectoryActivated) return;
        if (!is_movable) return;

        delta += Time.deltaTime;

        if (delta > interval)
        {
            delta = 0f;
            StartCoroutine(wait_howl());
            interval = Random.Range(5f, 10f);
        }

        if (is_movetime)
        {
            transform.Translate(Vector2.left * movespeed * Time.deltaTime);
        }
    }

       private IEnumerator wait_howl()
    {
        is_movetime = false;
        soundEffect.Effect_Sound("BOSSHOWL");
        Boss_ani.SetTrigger("Howl");
        yield return new WaitForSeconds(0.03f);
        boss_pattern();
        yield return new WaitForSeconds(0.5f);
        is_movetime = true;
    }

    public void ismovable()
    {
        is_movable = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    { if (collision.gameObject.CompareTag("Character"))
        {
            if (!character.is_Beat)
            {
                hp_manger.Damaged(monster_.damage);
            }
        }

        if (collision.gameObject.CompareTag("endpoint"))
        {
            Flip();
        }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.CompareTag("endpoint"))
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (isLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        isLeft = !isLeft;
    }


    public void boss_pattern()
    {
      int pattern = Random.Range(0, 3);

        if (pattern == 0)
        {
            StartCoroutine(Monster_Spawner(Random.Range(1, 6)));
        }
        else
        {
            StartCoroutine(Shooting(Random.Range(1, 10)));
        }
    }


    public void Change_Weapon()
    {
       if (RestHealth <= 70)
        {
            speed = 300f;
            attack_obj = attackPrefabSet[Random.Range(0, attackPrefabSet.Length)];
        }
        else
        {
            attack_obj = attackPrefabSet[0];
        }
        
    }

    public void Change_Enemy()
    {
       enemy_obj = EnemyPrefab[Random.Range(0, EnemyPrefab.Length)];
    }

    public IEnumerator Monster_Spawner(int count_)
    {
         if (Rest_count > 0) yield break;

        Rest_count = count_;

        for (int i = 0; i < count_; i++)
        {
            Change_Enemy();
            Instantiate(enemy_obj, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator Shooting(int count_)
         Change_Weapon();

        for (int j = 0; j < count_; j++)
        {
            for (int i = 0; i < oneShoting; i++)
            {
                GameObject obj = Instantiate(attack_obj, transform.position, Quaternion.identity);

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                Vector2 dir = new Vector2(
                    Mathf.Cos(Mathf.PI * 2 * i / oneShoting),
                    Mathf.Sin(Mathf.PI * 2 * i / oneShoting)
                );

                rb.AddForce(dir * speed);
                obj.transform.Rotate(0f, 0f, 360 * i / oneShoting - 90);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator BossOnBeatTime()
    {
       if (MonsterSprite == null) yield break;

        for (int i = 0; i < BeatTime * 5; i++)
        {
            MonsterSprite.color = (i % 2 == 0)
                ? new Color32(130, 140, 200, 90)
                : new Color32(130, 140, 200, 180);

            yield return new WaitForSeconds(0.3f);
        }

        MonsterSprite.color = Color.white;
        Boss_is_Beat = false;
        
    }

    public void BOSSDIE()
    {
      if (soundEffect != null)
            soundEffect.Effect_Sound("BOSSDIE");

        if (bossMusic != null)
            bossMusic.Audio_Source.Pause();

        Instantiate(Drop_prefap, transform.position, transform.rotation);

        foreach (var end in endpoint_controll)
        {
            end.End_collider.isTrigger = true;
        }

        Destroy(gameObject, 0.3f);
    }

}
