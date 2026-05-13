using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement_ : MonoBehaviour
{
    Rigidbody2D playerRd; //rigid body 이름을 playerRd로
    public float speed = 5.0f;
    public float jumppower = 10f;
    public bool isground = false;//땅에 닿았는가
    public bool doublejumpable = false;//더블점프 아이템을 먹었는가
    public bool firegun = false;//빨간총 아이템 활성화
    public int JumpCount = 1;
    public int DoubleJumpCount = 2;
    //아래는 총알 방향을 결정하기 위한 bool 변수
    //다른 스크립트에서 참조 가능하도록 앞에 static public을 붙여줌
    public bool left = false;
    public bool right = true;
    //인벤토리 테스트
    //private GunInventory guninventory;


    // Start is called before the first frame update
    void Start()
    {
        //guninventory = GetComponent<GunInventory>();
        playerRd = GetComponent<Rigidbody2D>();
        //JumpCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("DoubleJump"))
        {
            Destroy(collision.gameObject);
            //collision.gameObject.SetActive(false);
            doublejumpable = true;
            /*Collision 충돌 처리할 때는 두 객체 모두 컴포넌트에 RigidBody를 가지고 있고,
            IsTrigger 와 Kinematic 속성이 비활성화 상태이고 Collier 컴포넌트를 둘다 가지고 있을때 사용 가능하다.
            Trigger 사용 할 때는 두 객체 모두 Collider가 있어야하고, 둘 중 하나는 IsTrigger 가 체크
            그리고 RigidBody를 가지고 있어야한다
            >> 따라서 OncollisionEnter로 하면 현재 아이템에 isTrigger가 체크 되어있으므로 실행이 안됨...그래서 OnTriggerEnter로 처리해야 isTrigger를 체크하고도 doublejumpable을 바꿀 수 잇음
            */
        }
        if (collision.gameObject.CompareTag("Gun"))
        {
            collision.gameObject.SetActive(false);

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            isground = true;
            if (doublejumpable == false) { JumpCount = 1; }
            else if (doublejumpable == true) { DoubleJumpCount = 2; }
        }


    }



    // Update is called once per frame
    void Update()
    {

        Movement();

    }


    void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            right = false;
            left = true;

            transform.localScale = new Vector3(-1, 1, 1); // 왼쪽 바라보기
            playerRd.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);

        }

        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            left = false;
            right = true;
            transform.localScale = new Vector3(1, 1, 1); // 오른쪽 바라보기
            playerRd.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
        }

        if (isground)
        {
            if (doublejumpable == true)
            {
                if (DoubleJumpCount > 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) == true) //GetkeyDown 으로 안하고 그냥 Getkey로만 하면 뗄 때도 JumpCount가 줄어들어서 점프가 안됨
                    {
                        playerRd.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
                        DoubleJumpCount--;
                    }
                }
            }
            else
            {
                if (JumpCount > 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) == true) //GetkeyDown 으로 안하고 그냥 Getkey로만 하면 뗄 때도 JumpCount가 줄어들어서 점프가 안됨
                    {
                        playerRd.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
                        JumpCount--;


                    }
                }
            }

        }

        else
        {

        }
    }


}
