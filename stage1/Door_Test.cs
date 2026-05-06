using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_Test : MonoBehaviour
{
   public Dialog_Test dialog_;

    public bool isEnter = false;
    public bool isInteractioning = false;

    private string[] currentDialogue;
    private bool needSelection = false;

    private GameObject dialogParent;
    private GameObject dialog;
    private GameObject selectUI;

    void Start()
    {
        dialogParent = GameObject.Find("Dialog");
        dialog = dialogParent.transform.Find("DialogBase").gameObject;
        dialog_ = dialog.GetComponent<Dialog_Test>();
        selectUI = dialogParent.transform.Find("SelectedBase").gameObject;

        currentDialogue = null;
        needSelection = false;
    }

    void Update()
    {
        if (isEnter || isInteractioning)
        {
            HandleInteraction();
        }
    }

    public void Get_Dialogue(bool is_enter, string[] dialogue, bool useSelection)
    {
        isEnter = is_enter;
        currentDialogue = dialogue;
        needSelection = useSelection;
    }

    private void HandleInteraction()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        if (!isInteractioning)
        {
            StartDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        if (dialog == null || dialog_ == null) return;

        dialog.SetActive(true);
        isInteractioning = true;

        dialog_.Typing_trigger(currentDialogue, needSelection);
    }

    private void EndDialogue()
    {
        if (dialog != null)
            dialog.SetActive(false);

        if (selectUI != null)
            selectUI.SetActive(false);

        isInteractioning = false;
    }
}
