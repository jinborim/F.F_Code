using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text gtext;
    public bool isAction; //대화창 활성화 상태 

    //씬 2의 몬스터 삭제
    public GameObject monster;
    public enemyDead enemyControl;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        isAction = false;
        talkPanel = GameObject.Find("talkPanel");
        gtext = talkPanel.gameObject.GetComponentInChildren<Text>();
        enemyControl = GameObject.FindObjectOfType<enemyDead>();
        monster = enemyControl.transform.gameObject;
        player = GameObject.FindObjectOfType<PlayerMovement>();

        talkPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        if (isAction) // 실행중인데 또 액션을 실행한경우 -> 이미 true인경우
        {
            isAction = false; //false로 변경

        }
        else //실행중 -> 대화창 띄우기 
        {
            isAction = true;
        }

        talkPanel.SetActive(isAction); //대화창 활성화 상태에 따라 대화창 활성화 변경
    }

    public void Scene22(bool scene2)
    {
        if (scene2 == true)
        {
            //bulletReach = 3;
            Destroy(monster, 0.5f);

            //Invoke("OnInvoke", 30.0f);
        }
        Invoke("Call0", 1f);
    }
    public void Call0()
    {
        StartCoroutine(player.Scene33(true));
    }

    public void panel(bool is_true)
    {
        if (is_true == true)
        {
            talkPanel.gameObject.SetActive(true);
        }
        else if (is_true == false)
        {
            talkPanel.gameObject.SetActive(false);
        }
    }

}
