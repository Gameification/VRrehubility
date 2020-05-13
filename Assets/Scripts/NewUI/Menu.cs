using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject butstart;
    public GameObject settings;
    public GameObject exit;
    public GameObject butback;
    public GameObject scrollbar_ref;
    public GameObject scrollview;
    public GameObject arrowleft;
    public GameObject arrowrigth;
    public GameObject Exercise_text;
    public GameObject VrRehab;
    private Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
       scrollbar = scrollbar_ref.GetComponent<Scrollbar>();
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Button2()
    {
        Application.Quit();
    }

    public void Button3()
    {
        butstart.SetActive(false);
        settings.SetActive(false);
        exit.SetActive(false);
        butback.SetActive(true);
        scrollbar_ref.SetActive(true);
        arrowleft.SetActive(true);
        arrowrigth.SetActive(true);
        Exercise_text.SetActive(true);
        scrollview.SetActive(true);
        VrRehab.SetActive(false);
        scrollbar.value = 0;
    }

    public void ButtonSlide_Rigth()
    {
        if(scrollbar.value == 1)
        {
            scrollbar.value = 0;
        }
        else
        scrollbar.value += 0.25f;
    }
    public void ButtonSlide_Left()
    {
        if (scrollbar.value == 0)
        {
            scrollbar.value = 1;
        }
        else
        scrollbar.value -= 0.25f;
    }

    public void Button_Back()
    {
        butstart.SetActive(true);
        settings.SetActive(true);
        exit.SetActive(true);
        butback.SetActive(false);
        scrollbar_ref.SetActive(false);
        arrowleft.SetActive(false);
        arrowrigth.SetActive(false);
        Exercise_text.SetActive(false);
        VrRehab.SetActive(true);
        scrollview.SetActive(false);
    }

    public void Button_Slide1()
    {
        SceneManager.LoadScene(1);
    }
}
