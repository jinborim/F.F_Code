using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loading_Text : MonoBehaviour
{
    public TextMeshProUGUI dialog_context;

    // Start is called before the first frame update
    void Start()
    {
        dialog_context = this.GetComponentInChildren<TextMeshProUGUI>();
        dialog_context.text = null;
        StartCoroutine(Typing("Now Loading..."));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Typing(string dialogue)
    {
        //이 함수가 시작할 때 한 번 내용물을 싹 비워준다(안그러면 전의 대사에 추가해서 나옴)
        //위의 Typing_trigger에서 dialogue를 가져와서 talk라는 지역변수?로 사용
        do
        {
            //dialog_context.text = null;


            for (int i = 0; i < dialogue.Length; i++)
            {
                dialog_context.text = null;
                string talk = dialogue;
                for (int j = 0; j < talk.Length; j++)
                {
                    dialog_context.text += talk[j];
                    yield return new WaitForSeconds(0.2f); //코러틴 함수에서 사용하는 함수로, 지정된 시간에 한번씩 돌아가게 됨 > 글자 출력 속도랑 같다고 볼 수 있음
                }
                yield return new WaitForSeconds(0.3f);
            }
        } while (true);

    }
}
