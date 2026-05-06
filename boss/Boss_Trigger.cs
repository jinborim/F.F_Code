using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Trigger : MonoBehaviour
{
    private CharacterMovement character;

    public bool Is_bossStage;
    public string[] boss_Dialogue = new string[2];

    public Boss_Dialog dialog_;

    private CameraController camera_;
    public Boss_Health boss_health;
    public Boss_Movement boss_move;
    public EndPoint_boss[] endpoint_controll;
    public Boss_Music_Changer bossMusic;

    void Start()
    {
        bossMusic = GameObject.FindObjectOfType<Boss_Music_Changer>();
        dialog_ = GameObject.FindObjectOfType<Boss_Dialog>();
        boss_Dialogue = new string[] { "이 포도 하나가 뭐라고 \n내 영역까지 침범해 온 건가...", "여우가 늑대를 이길 수 있다 생각하다니.", "내 영역에 들어온 이상 나갈 순 없을 것이다." };
        camera_ = Camera.FindObjectOfType<CameraController>();
        boss_health = GameObject.FindObjectOfType<Boss_Health>();
        boss_move = GameObject.FindObjectOfType<Boss_Movement>();
        character = GameObject.FindObjectOfType<CharacterMovement>();
        endpoint_controll = GameObject.FindObjectsOfType<EndPoint_boss>();
        Is_bossStage = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (!collision.CompareTag("Character")) return;
        if (Is_bossStage) return;

        if (bossMusic != null)
            bossMusic.Audio_Source.Pause();

        if (character != null)
            character.movable = false;

        if (camera_ != null)
            StartCoroutine(camera_.Fixed_Boss());

        if (dialog_ != null)
        {
            dialog_.dialog_context.gameObject.SetActive(true);
            dialog_.Typing_trigger(boss_Dialogue);
        }

        Is_bossStage = true;
    }

    public void Start_boss_stage(bool is_Start)
    {
       
        if (!is_Start) return;

        if (bossMusic != null)
            bossMusic.BossMusic("BOSS");

        GameObject block = GameObject.Find("Block");
        if (block != null)
        {
            Transform manager = block.transform.Find("Block_manager");
            if (manager != null)
                manager.gameObject.SetActive(true);
        }

        if (character != null)
            character.movable = true;

        if (boss_health != null && boss_health.boss_health_parent != null)
        {
            boss_health.boss_health_parent.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (boss_move != null)
            boss_move.is_movable = true;

        if (endpoint_controll != null)
        {
            for (int i = 0; i < endpoint_controll.Length; i++)
            {
                endpoint_controll[i].End_collider.isTrigger = false;
            }
        }
    }
}
