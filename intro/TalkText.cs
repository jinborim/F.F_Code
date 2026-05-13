using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{

    public GameObject talkPanel;
    public Text text;
    public GameManager manager;



    public PlayergoOut player;
    public float delta = 0;
    public float interval = 2.5f;


    public int clickCount = 0;
    public bool lastClick = false;


    // Start is called before the first frame update
    void Start()
    {

        talkPanel = GameObject.FindObjectOfType<GameManager>().talkPanel;
        manager = GameObject.FindObjectOfType<GameManager>();
        text = manager.gtext;
        text.text = "안녕, 네가 황금포도를 찾는다던 여우해결사지?";

        player = GameObject.FindObjectOfType<PlayergoOut>();
        player.tt = this.gameObject.GetComponent<TalkText>();

        //Scene55(true);
    }
    // Update is called once per frame
    void Update()
    {
        test();
        delta += Time.deltaTime;
        
    }


    public void Scene55(bool is_true)
    {

        //manager.panel(true);
        if (is_true == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (clickCount == 0)
                {
                    text.text = "의뢰를 하러 왔어. 당분간 일을 안 받는다는 건 알지만... 그래도 거절할 순 없을 걸?";
                    clickCount++; //1

                }

                else if (clickCount == 1)
                {
                    text.text = "나는 황금포도의 위치를 알고 있거든. 황금포도는 숲 속의 알파 늑대가 가지고 있어.";
                    clickCount++; //2

                }
                else if (clickCount == 2)
                {

                    text.text = "믿어도 좋아. 늑대에 대한 거라면 나만큼 정확한 사람은 없을 걸?";
                    clickCount++; //2

                }
                else if (clickCount == 3)
                {
                    text.text = "보수는 늑대 무리의 괴멸이야. 그리 찾던 포도 값으론 꽤 싸지? 너한테 그런 건 일도 아니잖아.";
                    clickCount++; //2

                }
                else if (clickCount == 4)
                {

                    text.text = "근데 포도는 왜 그리 찾는 거야? 그걸 먹으면 잃어버린 기억을 되찾기라도 해?";
                    clickCount++; //2

                }
                else if (clickCount == 5)
                {

                    text.text = "...터무니 없네. 뭐, 좋아. 그럼 지체할 것 없겠지. 바로 가자.";
                    clickCount++; //2
                }
                else if (clickCount == 6)
                {
                    clickCount++;
                    manager.panel(false);

                }

            }
        }
    }
    //대사 스크립트


    public void test()
    {
        Scene55(true);

    }

    public void Call3()
    {
        StartCoroutine(player.Scene66(true));
    }
}
