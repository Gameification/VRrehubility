using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PointGameManager : MonoBehaviour
{
    public GameObject TopLeft;
    public GameObject TopRigth;
    public GameObject BottomLeft;
    public GameObject BottomRigth;

    [Header("Label")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI DebugClickText;
    public GameObject winner;


    [Header("Levels")]
    public List<Level> levels = new List<Level>();
    public int step = 0;
    public int level = 0;
    public bool operations = false;

    public string combo = "";
    public string CurrentCombo = "";
    private GameEvents _eventSender = null;
    private RectTrigger[] rectTriggers = null;
    private float score_float = 0;
    private void Start()
    {
        _eventSender = GameObject.FindObjectOfType<GameEvents>();
        _eventSender.GameTimeChange += GameTimeChangeHandler;
        _eventSender.RepeateTimCehange += _eventSender_RepeateTimCehange;

        InvokeRepeating("RunGameTimer", 1, 1);
        rectTriggers = GameObject.FindObjectsOfType<RectTrigger>();
    }

    private void _eventSender_RepeateTimCehange(float newVal)
    {
        if (_eventSender.RepeateTimeD < 0 && (combo != CurrentCombo))
        {
            CancelInvoke("RunRepeateTimer");
            StartCoroutine(TextDebug("Попробуй снова!"));
            _eventSender.RepeateTimeD = 10f;
            operations = !operations;
            _eventSender.GameTimeD = 1f;
            step = 0;
            combo = "";
            CurrentCombo = "";
            InvokeRepeating("RunGameTimer", 1, 1);
        }
        else
        {
            foreach (RectTrigger rect in rectTriggers)
            {
                if (rect.mIsTriggered)
                {
                    CurrentCombo += rect.id;
                    score.text = "Счёт:" + (score_float += 10);
                    StartCoroutine(TextDebug("Есть!"));
                }
            }

            if (CurrentCombo == combo)
            {
                CancelInvoke("RunRepeateTimer");
                operations = !operations;
                _eventSender.GameTimeD = 1f;
                level++;
                step = 0;
                combo = "";
                CurrentCombo = "";
                StartCoroutine(TextDebug("Следующий уровень!"));
                InvokeRepeating("RunGameTimer", 1, 1);
            }
        }
        
    }

    private void Update()
    {
     
    }

    IEnumerator TextDebug(string textmessage)
    {
        DebugClickText.text = textmessage;
        yield return new WaitForSeconds(1.0f);
        DebugClickText.text = "";
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void GameTimeChangeHandler(float newVal)
    {
        if (_eventSender.GameTimeD < 0)
        {
            if (level > levels.Count)
            {
                CancelInvoke();
                Debug.Log("EndGame");
                winner.SetActive(true);
            }
            if (!operations && step == levels[level].TriggerQueue.Count)
            {
                operations = !operations;
                CancelInvoke("RunGameTimer");
                _eventSender.RepeateTimeD = 10;
                InvokeRepeating("RunRepeateTimer", 1, 1);
            }
            else
            {
                CancelInvoke("RunGameTimer");
                _eventSender.GameTimeD = 1f;
                StartCoroutine(Test(levels[level].TriggerQueue[step]));
                combo += levels[level].TriggerQueue[step].GetComponent<RectTrigger>().id;
                step++;
                InvokeRepeating("RunGameTimer", 1, 1);
            }
        }
    }


    void RunGameTimer()
    {
        _eventSender.GameTimeD -= 1;
    }
    void RunRepeateTimer()
    {
        _eventSender.RepeateTimeD -= 1;
    }

    IEnumerator Test(GameObject TriggeredImage)
    {
        while (true)
        {
            TriggeredImage.GetComponent<Image>().color = TriggeredImage.GetComponent<RectTrigger>().HighLigthColor;
            var color = TriggeredImage.GetComponent<Image>().color;
            for (float i = 1; i >= 0; i -= 0.1f)
            {
                color.a = i;
                TriggeredImage.GetComponent<Image>().color = color;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            for (float i = 0; i < 1; i += 0.1f)
            {
                color.a = i;
                TriggeredImage.GetComponent<Image>().color = color;
                yield return null;
            }
            TriggeredImage.GetComponent<Image>().color = TriggeredImage.GetComponent<RectTrigger>().BaseColor;
            yield return new WaitForSeconds(3f);
            break;
        }
    }
}

[System.Serializable]
public class Level
{
    [Header("Queue triggers")]
    public List<GameObject> TriggerQueue;

}
