using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Gover_Btn : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private STAGE_Maintain stage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LoadSceneManager.LoadScene(stage.sceneNow);
        }
    }
}
