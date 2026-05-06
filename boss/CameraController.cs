using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target; 
    public GameObject BossTarget;

    public Vector3 bossTransform;

    public float moveSpeed = 3;
    private Vector3 targetPosition; 

    public Boss_Trigger is_boss;

    void Start()
    {
        is_boss = GameObject.FindObjectOfType<Boss_Trigger>();

        BossTarget = GameObject.Find("Boss");
        target = GameObject.Find("Character");
        
        if (BossTarget != null)
        {
            bossTransform = new Vector3(
                BossTarget.transform.position.x, -1f, transform.position.z);
        }
    }

    public void Character_movement()
    {
        targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
    }

    public IEnumerator Fixed_Boss()
    {
        while (Vector3.Distance(transform.position, bossTransform) > 0.01f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                bossTransform,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = bossTransform;
    }

    public void Update()
    {
       if (target == null) return;

        if (is_boss != null && is_boss.Is_bossStage)
            return;

        Character_movement();

    }
}
