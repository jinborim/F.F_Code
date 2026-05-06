using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 public npcMovement npc;
    public Animator ani;

    private Vector3 target;

    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        npc = FindObjectOfType<npcMovement>();
        ani = GetComponent<Animator>();

        target = new Vector3(250f, 88f, transform.position.z);
    }

    public IEnumerator Scene33(bool scene3)
    {
        if (!scene3) yield break;

        if (ani != null)
            ani.SetTrigger("walkTest");

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = target;

        Call1();
    }

    public void Call1()
    {
        if (npc != null)
            StartCoroutine(npc.Scene44(true));
    }
}
