using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class endingScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int clickCount = 0;
    public bool lastClick = false;

    public Text text;

    public SoundEffect_Manager soundEffect;

    void Start()
    {
        soundEffect = GameObject.FindObjectOfType<SoundEffect_Manager>();
        StartCoroutine(Typing("황금포도를 손에 넣었다.", 0.05f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickCount == 0)
            {
                //text.text = "포도를 먹고 곧바로 기억을 되찾는 것은 아니었으니,\n아마 며칠은 기다려야 할 터였다.";
                StartCoroutine(Typing("포도를 먹고 곧바로 기억을 되찾는 것은 아니었으니,\n 아마 며칠은 기다려야 할 터였다.",0.05f));
                clickCount++; //1
                
            }

            else if (clickCount == 1)
            {
                //text.text = "그리고 빨간 망토는... ";
                StartCoroutine(Typing("그리고 빨간 망토는... ", 0.05f));
                clickCount++; //2
                

            }
            else if (clickCount == 2)
            {

                //text.text = "한참이나 늑대의 사체를 노려보다, 이내 자기가 처리하게 해달라고 요구했다.";
                StartCoroutine(Typing("한참이나 늑대의 사체를 노려보다, 이내 자기가 처리하게 해달라고 요구했다.", 0.05f));
                clickCount++; //2
               

            }
            else if (clickCount == 3)
            {
                //text.text = "이 쪽 목적은 달성했으니 승낙했다. 아무래도 상관 없었다.";
                StartCoroutine(Typing("이 쪽 목적은 달성했으니 승낙했다. 아무래도 상관 없었다.", 0.05f));
                clickCount++; //2
               

            }
            else if (clickCount == 4)
            {

                //text.text = "당분간 제 일상에 큰 변화는 없을 것이다.";
                StartCoroutine(Typing("당분간 제 일상에 큰 변화는 없을 것이다.", 0.05f));
                clickCount++; //2
                

            }
            else if (clickCount == 5)
            {

                //text.text = "아마도.";
                StartCoroutine(Typing("아마도.", 0.05f));
                clickCount++; //2
               

            }
            else if (clickCount == 6)
            {

                //text.text = "END.";
                StartCoroutine(Typing("END.", 0.05f));
                clickCount++; //2


            }
            else if (clickCount == 7)
            {

                //text.text = "...AND? ";
                StartCoroutine(Typing("...AND? ", 0.2f));
                clickCount++; //2


            }
        }
    }

    IEnumerator Typing(string dialogue, float text_speed)
    {
        text.text = null;
        for (int j = 0; j < dialogue.Length; j++)
        {
            text.text += dialogue[j];

            soundEffect.Effect_Sound("DIALOG1");
            yield return new WaitForSeconds(text_speed);
        }
        
        
    }

}
