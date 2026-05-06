using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Door : MonoBehaviour
{
    public string SceneName; 

    private Door_Test door;
    public Select_Yes selected;

    public bool is_Enter = false; 
    public string[] Door_dialogue = new string[]{"다음 스테이지로 이동할까?"};


   void Start()
    {
        door = GameObject.FindObjectOfType<Door_Test>();

        if (door != null)
        {
            selected = door.select_.GetComponentInChildren<Select_Yes>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Character"))
            return;

        is_Enter = true;

        if (selected != null)
            selected.GetSceneName(SceneName);

        if (door != null)
            door.Get_Dialogue(is_Enter, Door_dialogue, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Character"))
            return;

        is_Enter = false;

        if (door != null)
            door.Get_Dialogue(false, null, false);
    }
}
