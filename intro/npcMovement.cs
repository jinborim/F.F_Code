using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{
  public TalkText ttext;
    public GameManager manager;

    private Vector3 target;

    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        ttext = FindObjectOfType<TalkText>();

        target = new Vector3(500f, 88f, transform.position.z);
    }

    public IEnumerator Scene44(bool scene4)
    {
        if (!scene4) yield break;

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

        if (manager != null)
            manager.panel(true);

        yield return new WaitForSeconds(0.5f);

        if (ttext != null)
            ttext.Scene55(true);
    }
}
