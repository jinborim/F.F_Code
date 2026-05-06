using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog_Test : MonoBehaviour
{
 public static GameObject Dialogue;

    [SerializeField] private GameObject dialogObj;
    [SerializeField] private Text dialogContext;
    [SerializeField] private GameObject selectBase;

    private SoundEffect_Manager soundEffect;

    public bool dialogueEnd;

    private void Awake()
    {
        Dialogue = gameObject;
    }

    void Start()
    {
        dialogueEnd = false;
        soundEffect = FindObjectOfType<SoundEffect_Manager>();
    }

    public void Typing_trigger(string[] dialogues, bool useSelection)
    {
        StartCoroutine(Typing(dialogues, useSelection));
    }

    private IEnumerator Typing(string[] dialogues, bool useSelection)
    {
        dialogContext.text = string.Empty;

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogContext.text = string.Empty;
            string line = dialogues[i];

            for (int j = 0; j < line.Length; j++)
            {
                dialogContext.text += line[j];

                soundEffect?.Effect_Sound("DIALOG1");
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.5f);
        }

        dialogueEnd = true;

        if (useSelection && selectBase != null)
        {
            selectBase.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
