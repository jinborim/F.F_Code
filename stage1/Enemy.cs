using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] private float speed;

    private bool isMovingLeft = true;

    void Start()
    {
        speed = Random.Range(2f, 6f);
    }

    void Update()
    {
        if (Inventory.invectoryActivated) return;

        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void TurnAround()
    {
        isMovingLeft = !isMovingLeft;

        float yRotation = isMovingLeft ? 0f : 180f;
        transform.eulerAngles = new Vector3(0f, yRotation, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("endpoint")) return;

        TurnAround();
    }
}
