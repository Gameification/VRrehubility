using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandGameManager : MonoBehaviour
{
    public Slider input;
    public GameObject setup;
    public GameObject winner;
    public TextMeshProUGUI timetext;
    private HandsEvent _eventSender = null;
    


    private void Start()
    {
        _eventSender = GameObject.FindObjectOfType<HandsEvent>();
        _eventSender.GameTimeChange += GameTimeChangeHandler;
        setup.SetActive(true);
    }

    private void GameTimeChangeHandler(float newVal)
    {
        if (_eventSender.GameTimeD < 0)
        {
            CancelInvoke("RunGameTimer");
            Debug.Log("End game");
            winner.SetActive(true);
        }
    }

    void RunGameTimer()
    {
        _eventSender.GameTimeD -= 1;
    }

    public void StartGame()
    {
        _eventSender.GameTimeD = input.value;
        InvokeRepeating("RunGameTimer", 1, 1);
    }
    public void TimeTextUpdate()
    {
        timetext.text = ((int)input.value).ToString();
    }

    public void SaveResult()
    {

    }
}
