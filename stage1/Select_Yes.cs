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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Panel_activated == true)
            {
                //Debug.Log(sceneName);
                //SceneManager.LoadScene(sceneName);
                GameObject.Find("DialogBase").gameObject.SetActive(false);
                GameObject.Find("SelectedBase").gameObject.SetActive(false);
                LoadSceneManager.LoadScene(sceneName);
                sceneName = "";

                
            }
            else
            {

            }
        }
    }

    public void Panel_ColorChanger(bool Panelactivated_)
    {
        if (Panel_activated == true)
        {
            color_ = new Color(1, 1, 1, 1);
            color_.a = 1;
            Panel_Img.color = color_;
        }
        else
        {
            color_ = new Color(1, 1, 1, 1);
            color_.a = 0.8f;
            Panel_Img.color = color_;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        Panel_Img = transform.GetComponent<Image>();
        //sceneName = "";
        //Panel_Img.color = color_;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
