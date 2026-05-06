using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 public GameObject target;
    public GameObject BossTarget;

    private Vector3 bossTransform;
    private Vector3 targetPosition;

    [SerializeField] private float moveSpeed = 3f;

    public Boss_Trigger is_boss;

    void Start()
    {
        is_boss = FindObjectOfType<Boss_Trigger>();
        BossTarget = GameObject.Find("Boss");
        target = GameObject.Find("Character");

        if (BossTarget != null)
        {
            bossTransform = new Vector3(
                BossTarget.transform.position.x,
                -1f,
                transform.position.z
            );
        }
    }

    void Update()
    {
        if (target == null) return;

        if (is_boss != null && is_boss.Is_bossStage)
            return;

        FollowTarget();
    }

    private void FollowTarget()
    {
        targetPosition = new Vector3(
            target.transform.position.x,
            target.transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    public IEnumerator Fixed_Boss()
    {
        while (Vector3.Distance(transform.position, bossTransform) > 0.05f)
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
}
