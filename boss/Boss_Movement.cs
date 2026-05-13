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

    public int oneShoting;
    public float speed;
    public float interval=5.0f;
    float delta = 0;
    public float RestHealth;

    public int Rest_count;

    bool isLeft = true;
    public bool is_movable;
    public bool is_movetime;
    public bool Boss_is_Beat;
    public float BeatTime;
    public float movespeed=3f;
    private SpriteRenderer MonsterSprite;

    public SoundEffect_Manager soundEffect;
    public Boss_Music_Changer bossMusic;


    [SerializeField]
    GameObject Drop_prefap;

    public EndPoint_boss[] endpoint_controll;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //캐릭터는 기본적으로 isTrigger가 활성화되어있지 않기때문에 이 함수를 사용한다.
        if (collision.gameObject.CompareTag("Character"))
        {
            if (character.is_Beat == false)
            {
                hp_manger.Damaged(monster_.damage);
                //StartCoroutine(character.OnBeatTime());

            }


        }
        if (collision.gameObject.CompareTag("endpoint"))
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        Boss_ani = GetComponent<Animator>();
        oneShoting = 10;
        speed = 150f;
        Boss_is_Beat = false;
        is_movable = false;
        is_movetime = true;
        BeatTime = 1.5f;
        MonsterSprite = transform.GetComponent<SpriteRenderer>();
        //StartCoroutine(Shooting(5));
        Rest_count = 0;
        RestHealth = 150;
        
        hp_manger = GameObject.FindObjectOfType<HP_Manager>();
        
        character = GameObject.FindObjectOfType<CharacterMovement>();

        soundEffect = GameObject.FindObjectOfType<SoundEffect_Manager>();

        endpoint_controll = GameObject.FindObjectsOfType<EndPoint_boss>();

        bossMusic = GameObject.FindObjectOfType<Boss_Music_Changer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.invectoryActivated == false)
        {
            if (is_movable == true)
            {
                delta += Time.deltaTime;
                if (delta > interval)
                {
                    delta = 0;
                    //Boss_ani.SetTrigger("Howl");
                    StartCoroutine(wait_howl());


                    interval = Random.Range(5f, 10f);
                }
                if (is_movetime == true)
                {
                    transform.Translate(Vector2.left * movespeed * Time.deltaTime);
                }

            }
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
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "endpoint")
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }
    }




    public void boss_pattern()
    {
        int random_pattern = Random.Range(0, 3);
        switch (random_pattern)
        {
            case 0:
                Debug.Log("몬스터 스폰");
                int random_index_M = Random.Range(1, 6);
                StartCoroutine(Monster_Spawner(random_index_M));
                return;
            case 1 or 2:
                Debug.Log("탄막");
                int random_index_T = Random.Range(1, 10);
                StartCoroutine(Shooting(random_index_T));
                return;
        }
    }


    public void Change_Weapon()
    {
        if (RestHealth <= 70)
        {
            speed = 300f;
            int index = Random.Range(0, attackPrefabSet.Length);
            attack_obj = attackPrefabSet[index];
        }
        else
        {
            attack_obj = attackPrefabSet[0];
        }
        
    }

    public void Change_Enemy()
    {
        int index = Random.Range(0, EnemyPrefab.Length);
        enemy_obj = EnemyPrefab[index];
    }




    public IEnumerator Monster_Spawner(int count_)//소환할 몬스터 수를 인자로 받기
    {
        if (Rest_count <= 0)
        {
            Rest_count = count_;
            
            for (int i = 0; i < count_; i++)
            {
                Change_Enemy();
                GameObject obj = Instantiate(enemy_obj, this.transform.position, Quaternion.identity);

            }
            yield return new WaitForSeconds(0.5f);
            
        }
        
        

    }





    public IEnumerator Shooting(int count_)//나중에 어떤 숫자를 받아서 do-while문 바꾸기(for문으로만 반복해서, 몇번이나 쏠건지를 정한다)
    {
        float angle = 360 / oneShoting;
        Change_Weapon();

        for(int j=0; j<count_; j++)
        {
            for (int i = 0; i < oneShoting; i++)
            {
                GameObject obj = Instantiate(attack_obj, this.transform.position, Quaternion.identity);

                Rigidbody2D obj_RD = obj.GetComponent<Rigidbody2D>();
                obj_RD.AddForce(new Vector2(speed * Mathf.Cos(Mathf.PI * 2 * i / oneShoting), speed * Mathf.Sin(Mathf.PI * i * 2 / oneShoting)));

                obj.transform.Rotate(new Vector3(0f, 0f, 360 * i / oneShoting - 90));
            }
            yield return new WaitForSeconds(1f);
        }
        

    }

    public IEnumerator BossOnBeatTime()
    {
        if (MonsterSprite != null)
        {
            for (int i = 0; i < BeatTime * 5; ++i)
            {
                if (i % 2 == 0)
                    MonsterSprite.color = new Color32(130, 140, 200, 90);
                else
                    MonsterSprite.color = new Color32(130, 140, 200, 180);

                yield return new WaitForSeconds(0.3f);
            }

            //Alpha Effect End
            MonsterSprite.color = new Color32(255, 255, 255, 255);

            Boss_is_Beat = false;

            yield return null;
        }
        
    }


    public void BOSSDIE()
    {
        soundEffect.Effect_Sound("BOSSDIE");
        bossMusic.Audio_Source.Pause();
        GameObject drop_item = Instantiate(Drop_prefap, transform.position, transform.rotation);
        for (int i = 0; i < endpoint_controll.Length; i++)
        {
            endpoint_controll[i].End_collider.isTrigger = true;
        }

        GameObject.Find("Block").transform.Find("Block_manager").gameObject.SetActive(false);
        Potion[] potion = GameObject.FindObjectsOfType<Potion>();
        Enemy[] enemy = GameObject.FindObjectsOfType<Enemy>();
        for(int i=0; i<potion.Length; i++)
        {
            Destroy(potion[i].gameObject);
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i].gameObject);
        }
        Destroy(gameObject, 0.3f);
    }

}
