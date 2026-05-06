using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Select_Yes : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Color color_; 
    public Image Panel_Img; 


    public string sceneName;
    public bool Panel_activated = false;


    public void GetSceneName(string _sceneName)
    {
        sceneName = _sceneName;
        //Debug.Log(sceneName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Panel_activated = true;
        Panel_ColorChanger(true); 

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        Panel_activated = false; 
        Panel_ColorChanger(false);

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left && Panel_activated)
        {

                GameObject.Find("DialogBase").SetActive(false);
                GameObject.Find("SelectedBase").SetActive(false);

                LoadSceneManager.LoadScene(sceneName);

                sceneName = "";
        }
}


        public void Panel_ColorChanger(bool isActivated)
        {
             color_ = Color.white;
        color_.a = isActivated ? 1f : 0.8f;

        if (Panel_Img != null)
            Panel_Img.color = color_;
        }

        void Start()
        {
              if (Panel_Img == null)
            Panel_Img = GetComponent<Image>();
        }
    }
