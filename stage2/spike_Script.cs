using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike_Script : MonoBehaviour
{
    private HP_Manager hp_manger;
    public CharacterMovement character;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //캐릭터는 기본적으로 isTrigger가 활성화되어있지 않기때문에 이 함수를 사용한다.
        if (collision.gameObject.CompareTag("Character"))
        {
            if (character.is_Beat == false)
            {
                hp_manger.Damaged(10);

            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            if (character.is_Beat == false)
            {
                hp_manger.Damaged(10);

            }


        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hp_manger = GameObject.FindObjectOfType<HP_Manager>();
        character = GameObject.FindObjectOfType<CharacterMovement>();
    }

}
