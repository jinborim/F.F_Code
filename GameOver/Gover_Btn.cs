using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Gover_Btn : MonoBehaviour, IPointerClickHandler
{
    public STAGE_Maintain stage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //SceneManager.LoadScene(stage.sceneNow);
            LoadSceneManager.LoadScene(stage.sceneNow);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stage = GameObject.FindObjectOfType<STAGE_Maintain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
