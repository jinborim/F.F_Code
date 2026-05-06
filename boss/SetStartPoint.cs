using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetStartPoint : MonoBehaviour
{
    private GameObject thePlayer;
    
    public float Xpoint;
    public float Ypoint;
    public Vector2 startpoint;

    void Start()
    {
       startpoint = new Vector2(Xpoint, Ypoint);

        if (thePlayer == null)
            thePlayer = GameObject.Find("Character");

        if (thePlayer != null)
            thePlayer.transform.position = startpoint;

        var dialogMaintain = Dialogue_Maintain.Instance;

        if (dialogMaintain != null)
        {
            Dialog_Test dialog = dialogMaintain.GetComponentInChildren<Dialog_Test>();
            Select_base select = dialogMaintain.GetComponentInChildren<Select_base>();

            if (dialog != null)
                dialog.gameObject.SetActive(false);

            if (select != null)
                select.gameObject.SetActive(false);
        }
    }
}
