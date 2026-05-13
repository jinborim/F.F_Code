using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target; // 카메라가 따라갈 대상
    public GameObject BossTarget;

    public Vector3 bossTransform;

    public float moveSpeed = 3;// 카메라가 따라갈 속도
    private Vector3 targetPosition; // 대상의 현재 위치
    //public GameObject character;

    public Boss_Trigger is_boss;

    // Start is called before the first frame update
    void Start()
    {
        is_boss = GameObject.FindObjectOfType<Boss_Trigger>();

        BossTarget = GameObject.Find("Boss");
        target = GameObject.Find("Character");
        if (BossTarget != null)
        {
            //bossTransform = BossTarget.transform;
            bossTransform.Set(BossTarget.transform.position.x, -1, this.transform.position.z);
        }
        
    }

    public void Character_movement()
    {
        // this는 카메라를 의미 (z값은 카메라값을 그대로 유지)
        targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

        // vectorA -> B까지 T의 속도로 이동
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
    }

    public IEnumerator Fixed_Boss()
    {
        do
        {
            this.transform.position = Vector3.Lerp(this.transform.position, bossTransform, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);

        } while (this.transform.position != bossTransform);

        this.transform.position=bossTransform;
    }


    // Update is called once per frame
    public void Update()
    {
        
        // 대상이 있는지 체크
        if (target.gameObject != null)
        {
            if (is_boss != null)
            {
                if (is_boss.Is_bossStage == false)
                {
                    Character_movement();
                }
                
            }
            else if (is_boss == null)
            {
                Character_movement();
            }
            
            
        }
        

    }
}
