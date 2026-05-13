using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{
    public TalkText ttext;
    private Vector3 target;
    //Vector2 target = new Vector2(500, 88);
    public GameManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        target.Set(500, 88, this.transform.position.z);
    }

    void Update()
    {
        
    }

    public IEnumerator Scene44(bool scene4)
    {
        do
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 2f);
            yield return new WaitForSeconds(0.01f);

        } while (this.transform.position != target) ;

        this.transform.position = target;

        if (scene4 == true)
        {
            manager.panel(true);
            ttext = GameObject.FindObjectOfType<TalkText>();

            yield return new WaitForSeconds(0.5f);
        }
        ttext.Scene55(true);



    }
    public void Call2()
    {
        
        ttext.Scene55(true);

    }

}
