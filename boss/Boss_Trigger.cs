using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Trigger : MonoBehaviour
{
    private CharacterMovement character;

    public bool Is_bossStage;
    public string[] boss_Dialogue = new string[2];

    public Boss_Dialog dialog_;
    GameObject Dialog;

    private CameraController camera_;
    public Boss_Health boss_health;

    public Boss_Movement boss_move;
    public EndPoint_boss[] endpoint_controll;

    public Boss_Music_Changer bossMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            if (Is_bossStage == false)
            {
                bossMusic.Audio_Source.Pause();

                character.movable = false;
                StartCoroutine(camera_.Fixed_Boss()); //카메라에서 가져온 코루틴 함수로 보스에게 자연스럽게 카메라 토스할거임

                dialog_.dialog_context.gameObject.SetActive(true);
                dialog_.Typing_trigger(boss_Dialogue);
                //boss_health.boss_health_parent.transform.GetChild(0).gameObject.SetActive(true);
                Is_bossStage = true;
                //StartCoroutine(camera_.Fixed_Boss()); //카메라에서 가져온 코루틴 함수로 보스에게 자연스럽게 카메라 토스할거임
            }


        }
    }

    public void Start_boss_stage(bool is_Start)
    {
        if (is_Start == true)
        {
            bossMusic.BossMusic("BOSS");
            GameObject.Find("Block").transform.Find("Block_manager").gameObject.SetActive(true);
            character.movable = true;
            boss_health.boss_health_parent.transform.GetChild(0).gameObject.SetActive(true);
            boss_move.is_movable = true;
            for(int i=0; i<endpoint_controll.Length; i++)
            {
                endpoint_controll[i].End_collider.isTrigger = false;
            }
            
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        bossMusic = GameObject.FindObjectOfType<Boss_Music_Changer>();
        Is_bossStage = false;
        dialog_ = GameObject.FindObjectOfType<Boss_Dialog>();
        boss_Dialogue = new string[] { "이 포도 하나가 뭐라고 \n내 영역까지 침범해 온 건가...", "여우가 늑대를 이길 수 있다 생각하다니.", "내 영역에 들어온 이상 나갈 순 없을 것이다." };
        camera_ = Camera.FindObjectOfType<CameraController>();
        boss_health = GameObject.FindObjectOfType<Boss_Health>();
        boss_move = GameObject.FindObjectOfType<Boss_Movement>();
        character = GameObject.FindObjectOfType<CharacterMovement>();
        endpoint_controll = GameObject.FindObjectsOfType<EndPoint_boss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
