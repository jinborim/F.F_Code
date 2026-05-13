using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Boss_Dialog : MonoBehaviour
{

    public static GameObject Dialogue;

    //public GameObject dialogObj;
    public TextMeshProUGUI dialog_context; 
    string[] dialogue;
    //[SerializeField]
    //private GameObject select_base;

    public Boss_Health boss_health;
    public Boss_Trigger boss_trigger;

    public bool dialogue_END;

    public SoundEffect_Manager soundEffect;



    // Start is called before the first frame update
    void Start()
    {
        dialog_context=this.GetComponentInChildren<TextMeshProUGUI>();
        dialog_context.gameObject.SetActive(false);
        //dialogObj.gameObject.SetActive(false);
        dialogue_END = false;
        boss_trigger = GameObject.FindObjectOfType<Boss_Trigger>();
        soundEffect = GameObject.FindObjectOfType<SoundEffect_Manager>();
    }



    // Update is called once per frame
    private void Awake()
    {
        Dialogue = this.gameObject;
    }

    public void Typing_trigger(string[] _dialog)
    {
        // 다른 스크립트에서 현재 스크립트를 참조해서 Typing_trigger 사용
        // bool 형은 사실상 필요없어보이긴 한데, 나중에 다른곳에서 bool형 변수를 그대로 가져다 참조하면 편할거같아서 미리 만듦(문제 시 삭제하면 됨)
        // 다른 스크립트에서 이 함수를 불러올 때 '출력할 텍스트'를 미리 지정해서 변수로 넣어주면 그대로 가져와서 dialogue에 저장 후 이 dialogue를 Typing_Test에 넣어 한글자씩 출력하는 코러틴으로 만들어줌
        //dialogue = _dialog;

        for (int i = 0; i < _dialog.Length; i++)
        {
            //Debug.Log(_dialog[i]);
        }
        
        StartCoroutine(Typing_Test(_dialog));






    }

    IEnumerator Typing_Test(string[] dialogue)
    {
        //이 함수가 시작할 때 한 번 내용물을 싹 비워준다(안그러면 전의 대사에 추가해서 나옴)
        //위의 Typing_trigger에서 dialogue를 가져와서 talk라는 지역변수?로 사용

        dialog_context.text = null;


        for (int i = 0; i < dialogue.Length; i++)
        {
            dialog_context.text = null;
            string talk = dialogue[i];
            for (int j = 0; j < talk.Length; j++)
            {
                //입력받은 대사의 길이만큼 반복
                //위에서 Text형식으로 지정해준 dialog_context의 텍스트에 String 형식인 talk를 한글자씩 집어넣어줌
                // >> 결과적으로 텍스트에 한글자씩 추가되는 것이므로 화면상으론 한글자씩 말하는거처럼 보이게 됨
                dialog_context.text += talk[j];
                soundEffect.Effect_Sound("BOSSDIALOG");
                yield return new WaitForSeconds(0.1f); //코러틴 함수에서 사용하는 함수로, 지정된 시간에 한번씩 돌아가게 됨 > 글자 출력 속도랑 같다고 볼 수 있음
            }
            yield return new WaitForSeconds(0.5f);
        }

        dialog_context.gameObject.SetActive(false);
        boss_trigger.Start_boss_stage(true);
    }

}
