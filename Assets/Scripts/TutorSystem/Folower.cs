using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
public class Folower : MonoBehaviour
{
    public List<TutorSpriteInfo> _TutorialSprite = new List<TutorSpriteInfo>();
    public int _step;
    private Image _TutorBar = null;
    private TextMeshProUGUI _TutorToolTip;
    private EventHand _eventSender = null;
    private StickMan _stickm = null;
    public bool Lcheck = false;
    public bool HandsOn = false;

    private void Awake()
    {
        _step = 0;
        _stickm = Camera.main.GetComponent<StickMan>();
        _TutorToolTip = GameObject.Find("TutorToolTip").GetComponent<TextMeshProUGUI>();
        _TutorBar = GameObject.Find("TutorBar").GetComponent<Image>();
        _eventSender = GameObject.FindObjectOfType<EventHand>();
        _eventSender.GameTimeChange += GameTimeChangeHandler;
        
        InvokeRepeating("RunGameTimer", 1, 1);
    }


    private void RigthHandStateChangeHandler(bool newVal)
    {
        if (_eventSender.RigthHandStateD && _eventSender.LeftHandStateD)
        {
            NextStep();
            GameTimeChangeHandler(1f);
            _eventSender.LeftHandStateChange -= LeftHandStateChangeHandler;
            _eventSender.RigthHandStateChange -= RigthHandStateChangeHandler;
        }
    }

    private void LeftHandStateChangeHandler(bool newVal)
    {
        if(_eventSender.LeftHandStateD && !Lcheck)
        {
            NextStep();
            Lcheck = true;
        }
    }

    private void GameTimeChangeHandler(float newVal)
    {
        if (_eventSender.GameTimeD < 0)
        {
            CancelInvoke("RunGameTimer");
            if (_step != 6 || _step != 7)
            {

                if(_step == 5)
                {
                    _stickm.EnableLineRenderer(true);
                    _eventSender.LeftHandStateChange += LeftHandStateChangeHandler;
                    _eventSender.RigthHandStateChange += RigthHandStateChangeHandler;
                    HandsOn = true;
                    GameObject.Find("Hands_Setup").transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Hands_Setup").transform.GetChild(1).gameObject.SetActive(true);
                }
                if (_step == 8)
                {
                    GameObject.Find("Canvas").GetComponent<ColorSourceManager>().enabled = false;
                    GameObject.Find("RawImage").SetActive(false);
                    GameObject.Find("Canvas").GetComponent<FinalScript>().enabled = false;

                }
                if (_step == 10)
                {
                    _stickm.EnableLineRenderer(false);
                }
                if(_step == 13)
                {
                    HandsOn = false;
                    GameObject.Find("Hands_Setup").transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Hands_Setup").transform.GetChild(1).gameObject.SetActive(false);
                }
                if(_step == 14)
                {
                    SceneManager.LoadScene(GeneralSet._GameId);
                }
                _eventSender.GameTimeD = _TutorialSprite[_step].DelaySeconds;
                NextStep();
                InvokeRepeating("RunGameTimer", 1, 1);
            }
        }
    }

    public void NextStep()
    {
        _TutorBar.sprite = _TutorialSprite[_step].SplashImage;
        _TutorToolTip.text = _TutorialSprite[_step].TutorToolTip;
        _step++;
    }

    void RunGameTimer()
    {
        _eventSender.GameTimeD -= 1;
    }
}

[System.Serializable]
public class TutorSpriteInfo
{
    public int DelaySeconds;
    public Sprite SplashImage;
    public string TutorToolTip;
}
