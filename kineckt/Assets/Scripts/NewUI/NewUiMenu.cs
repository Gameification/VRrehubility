using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class NewUiMenu : MonoBehaviour
{
    public TextMeshProUGUI timetext;
    public Toggle Tutorial_toggle;
    // Update is called once per frame
    void Update()
    {
        if(System.DateTime.Now.Minute < 10)
        timetext.text = System.DateTime.Now.Hour.ToString() + ":0" + System.DateTime.Now.Minute.ToString();
        else
        timetext.text = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString();
    }

    public void LoadLevel(int id)
    {
        GeneralSet._GameId = id;
        SceneSetup(id);
    }

    public void SceneSetup(int id)
    {
        if(GeneralSet.tutor)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene(id);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Tutor_set()
    {
        GeneralSet.tutor = Tutorial_toggle.isOn;
    }
}
