using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetStartPoint : MonoBehaviour
{
    //public string startPoint;
    private GameObject thePlayer;
    public float Xpoint;
    public float Ypoint;
    public Vector2 startpoint;

    // Start is called before the first frame update
    void Start()
    {
        startpoint = new Vector2(Xpoint, Ypoint);
        // 플레이어 참조가 없을 경우 자동으로 찾아 할당
        if (thePlayer == null)
        {
            thePlayer = GameObject.Find("Character");
        }
        
        // 플레이어를 해당 씬의 시작 지점으로 즉시 이동
        thePlayer.transform.position = startpoint;


        // 씬 전환 시 남아있는 대화창 및 선택지 UI를 가에 비활성화
        // 중복 UI출력을 방지하여 화면 가독성과 안전성 설계
        if(Dialogue_Maintain.Instance.GetComponentInChildren<Dialog_Test>()&& Dialogue_Maintain.Instance.GetComponentInChildren<Select_base>()!= null)
        {
            Dialogue_Maintain.Instance.GetComponentInChildren<Dialog_Test>().gameObject.SetActive(false);
            Dialogue_Maintain.Instance.GetComponentInChildren<Select_base>().gameObject.SetActive(false);
        }
        

    }
}
