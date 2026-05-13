using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Door : MonoBehaviour
{
    public string SceneName;
    private Door_Test door;
    public Select_Yes selected;
    public bool is_Enter = false;
    public string[] Door_dialogue = new string[2];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") == true)
        {
            
            //Debug.Log("캐릭터랑 닿음");
            is_Enter = true;
            selected.GetSceneName(SceneName);
            door.Get_Dialogue(is_Enter, Door_dialogue, true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") == true)
        {
            //Debug.Log("충돌 해제");
            is_Enter = false;
            door.Get_Dialogue(is_Enter, null, false);

        }
    }



    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.FindObjectOfType<Door_Test>();
        selected = door.select_.GetComponentInChildren<Select_Yes>();
        Door_dialogue =new string[] { "다음 스테이지로 이동할까?" };
        
    }
}
