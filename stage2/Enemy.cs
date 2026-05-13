using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    bool isLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.invectoryActivated == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

    }
    void TurnAround()
    {

        if (isLeft) // 왼쪽으로 간다면
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // Y축 방향을 180도로 반대로 돌려 오른쪽으로 가게함
            isLeft = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // 오른쪽으로 가고 있다가 충돌시 다시 왼쪽으로 몸을 돌려 걸어감
            isLeft = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 특정 endpoint와 충돌시 방향 전환
        if (collision.gameObject.CompareTag("endpoint"))
        {
            TurnAround();
        }
    }
}
