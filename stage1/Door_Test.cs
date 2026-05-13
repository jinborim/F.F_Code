using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_Test : MonoBehaviour
{
    public Dialog_Test dialog_;
    public bool is_Enter=false;
    public bool is_Interactioning = false;
    //GameObject Dialog;

    string[] Renew_Dialogue;
    public bool Select_need = false;

    public GameObject dialog_parent;
    public GameObject dialog;
    public GameObject select_;



    public void Get_Dialogue(bool is_enter, string[] _dialogue, bool _needSelect)
    {
        is_Enter = is_enter;
        Renew_Dialogue = _dialogue;
        Select_need = _needSelect;
        
    }


    public void Interaction()
    {
        //스페이스바를 누르면 interaction 값을 받아줄, update 안에 넣을 함수 필요(IS_ENTER함수 재활용하자..
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {

            if (is_Interactioning == false)
            {
                dialog.SetActive(true);
                is_Interactioning = true;
                
                dialog_.Typing_trigger(Renew_Dialogue, Select_need);
                

            }
            else if (is_Interactioning == true)
            {
                dialog_.dialogObj.SetActive(false);
                is_Interactioning = false;
                dialog_parent.transform.Find("SelectedBase").gameObject.SetActive(false);

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //dialog_= GameObject.FindObjectOfType<Dialog_Test>();
        Renew_Dialogue=null;
        Select_need = false;
        //select_ = GameObject.Find("Dialog").transform.Find("SelectedBase").gameObject;
        dialog_parent = GameObject.Find("Dialog");
        dialog = dialog_parent.transform.Find("DialogBase").gameObject;
        dialog_ = dialog.transform.GetComponent<Dialog_Test>();
        select_ = dialog_parent.transform.Find("SelectedBase").gameObject;

    }



    // Update is called once per frame
    void Update()
    {
        if ((is_Enter == true)||((is_Enter==false) &&(is_Interactioning==true) ))
        {
            //기본적으로 문과 닿아있을 때 스페이스를 눌렀는지 확인할 수 있음
            //그러나 문과 닿는 순간 대화창을 켰는데 움직임이 멈췄을 때 문과 닿아있지 않으면 움직이지도 대화창을 끄지도 못함
            // >> 이 문제를 해결하기 위에 뒤의 코드에서 문과 닿아있지 않지만 대화창은 켜져 있는 경우에도 대화창을 끌 수 있게 함
            Interaction();
        }

    }
}
