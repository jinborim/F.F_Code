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

    public int clickCount = 0;

    private readonly string[] dialogues =
    {
        "의뢰를 하러 왔어. 당분간 일을 안 받는다는 건 알지만... 그래도 거절할 순 없을 걸?",
        "나는 황금포도의 위치를 알고 있거든. 황금포도는 숲 속의 알파 늑대가 가지고 있어.",
        "믿어도 좋아. 늑대에 대한 거라면 나만큼 정확한 사람은 없을 걸?",
        "보수는 늑대 무리의 괴멸이야. 그리 찾던 포도 값으론 꽤 싸지? 너한테 그런 건 일도 아니잖아.",
        "근데 포도는 왜 그리 찾는 거야? 그걸 먹으면 잃어버린 기억을 되찾기라도 해?",
        "...터무니 없네. 뭐, 좋아. 그럼 지체할 것 없겠지. 바로 가자."
    };

    void Start()
    {
        manager = FindObjectOfType<GameManager>();

        if (manager != null)
        {
            talkPanel = manager.talkPanel;
            text = manager.gtext;
        }

        player = FindObjectOfType<PlayergoOut>();

        if (player != null)
            player.tt = this;

        if (text != null)
            text.text = "안녕, 네가 황금포도를 찾는다던 여우해결사지?";
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        HandleDialogue();
    }

    private void HandleDialogue()
    {
        if (text == null || manager == null) return;

        if (clickCount < dialogues.Length)
        {
            text.text = dialogues[clickCount];
            clickCount++;
        }
        else
        {
            manager.panel(false);
        }
    }

    public void Call3()
    {
        if (player != null)
            StartCoroutine(player.Scene66(true));
    }
}
