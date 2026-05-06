using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text gtext;

    public bool isAction;

    public GameObject monster;
    public enemyDead enemyControl;
    public PlayerMovement player;

    void Start()
    {
        isAction = false;

        talkPanel = GameObject.Find("talkPanel");

        if (talkPanel != null)
            gtext = talkPanel.GetComponentInChildren<Text>();

        enemyControl = FindObjectOfType<enemyDead>();

        if (enemyControl != null)
            monster = enemyControl.gameObject;

        player = FindObjectOfType<PlayerMovement>();

        if (talkPanel != null)
            talkPanel.SetActive(false);
    }

    public void Action()
    {
        isAction = !isAction;

        if (talkPanel != null)
            talkPanel.SetActive(isAction);
    }

    public void Scene22(bool scene2)
    {
        if (scene2 && monster != null)
        {
            Destroy(monster, 0.5f);
        }

        Invoke(nameof(Call0), 1f);
    }

    private void Call0()
    {
        if (player != null)
            StartCoroutine(player.Scene33(true));
    }

    public void panel(bool is_true)
    {
        if (talkPanel == null) return;

        talkPanel.SetActive(is_true);
    }
}
