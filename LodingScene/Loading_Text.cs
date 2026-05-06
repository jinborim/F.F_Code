using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loading_Text : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialog_context;

    void Start()
    {
        if(dialog_context == null)
        {
            dialog_context = GetComponentInChildren<TextMeshProUGUI>();
        }
        dialog_context.text = "";
        StartCoroutine(Typing("Now Loading...")); 
    }

    IEnumerator Typing(string dialogue)
    {
      while (true)
        {
            dialog_context.text = "";

            for (int i = 0; i < dialogue.Length; i++)
            {
                dialog_context.text += dialogue[i];
                yield return new WaitForSeconds(0.15f);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
