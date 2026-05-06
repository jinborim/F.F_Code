using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class endingScript : MonoBehaviour
{
    public int clickCount = 0;
    
   [SerializeField] private Text text;
    private SoundEffect_Manager soundEffect;

    private bool isTyping = false;

     private readonly string[] dialogues =
    {
        "황금포도를 손에 넣었다.",
        "포도를 먹고 곧바로 기억을 되찾는 것은 아니었으니,\n 아마 며칠은 기다려야 할 터였다.",
        "그리고 빨간 망토는... ",
        "한참이나 늑대의 사체를 노려보다, 이내 자기가 처리하게 해달라고 요구했다.",
        "이 쪽 목적은 달성했으니 승낙했다. 아무래도 상관 없었다.",
        "당분간 제 일상에 큰 변화는 없을 것이다.",
        "아마도.",
        "END.",
        "...AND? "
    };

      private readonly float[] speeds =
    {
        0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.05f, 0.2f
    };

   void Start()
    {
        soundEffect = FindObjectOfType<SoundEffect_Manager>();
        StartCoroutine(Typing(dialogues[0], speeds[0]));
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (isTyping) return;
        if (clickCount >= dialogues.Length - 1) return;

        clickCount++;
        StartCoroutine(Typing(dialogues[clickCount], speeds[clickCount]));
    }

    IEnumerator Typing(string dialogue, float text_speed)
    {
        isTyping = true;

        if (text != null)
            text.text = string.Empty;

        for (int i = 0; i < dialogue.Length; i++)
        {
            text.text += dialogue[i];

            if (soundEffect != null)
                soundEffect.Effect_Sound("DIALOG1");

            yield return new WaitForSeconds(text_speed);
        }

        isTyping = false;
    }
}
