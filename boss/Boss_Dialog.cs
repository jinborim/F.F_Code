using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Boss_Dialog : MonoBehaviour
{
    public static GameObject Dialogue;

    [SerializeField] private TextMeshProUGUI dialog_context;

    public Boss_Health boss_health;
    public Boss_Trigger boss_trigger;
    public bool dialogue_END;

    public SoundEffect_Manager soundEffect;

    private void Awake()
    {
        Dialogue = this.gameObject;
    }

    void Start()
    {   
       if (dialog_context == null)
            dialog_context = GetComponentInChildren<TextMeshProUGUI>();

        if (dialog_context != null)
            dialog_context.gameObject.SetActive(false);

        dialogue_END = false;

        if (boss_trigger == null)
            boss_trigger = FindObjectOfType<Boss_Trigger>();

        if (soundEffect == null)
            soundEffect = FindObjectOfType<SoundEffect_Manager>();
    }
    public void Typing_trigger(string[] _dialog)
    {

        StartCoroutine(Typing_Test(_dialog));
    }

    IEnumerator Typing_Test(string[] dialogue)
    {
          if (dialog_context == null) yield break;

        dialog_context.gameObject.SetActive(true);
        dialog_context.text = string.Empty;

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialog_context.text = string.Empty;
            string talk = dialogue[i];

            for (int j = 0; j < talk.Length; j++)
            {
                dialog_context.text += talk[j];

                if (soundEffect != null)
                    soundEffect.Effect_Sound("BOSSDIALOG");

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
        }

        dialog_context.gameObject.SetActive(false);
        dialogue_END = true;

        if (boss_trigger != null)
            boss_trigger.Start_boss_stage(true);
    }
}
