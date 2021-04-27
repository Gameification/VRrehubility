using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectPart : MonoBehaviour
{

    public GameObject WinMenu;
    public GameObject MenuGood;
    public TMPro.TMP_Dropdown Dropdown1;




    public GameObject[] lvls;
    private GameObject lvl;
    private float StartTime;
  


    private List<string> currentParts = new List<string>();
    private List<List<string>> parts = new List<List<string>>();
    public static string currentPart;



    public static bool finish = false;
    //private int currentLVL = 0;
    private int score = 0;
    public static int mistakes = 0;

    private float time = 0.0f;

    private List<float> TimeResults = new List<float>();



    private Text task_Text;
    private Text score_Text;
    private Text timer_Text;


    // Start is called before the first frame update
    void Start()
    {
        task_Text = GameObject.Find("task").GetComponent<Text>();
        score_Text = GameObject.Find("score").GetComponent<Text>();
        timer_Text = GameObject.Find("timer").GetComponent<Text>();
        
        parts.Add(new List<string>{"ноги","голова","уши","xвост"});
        parts.Add(new List<string>{"глаза","уши","волосы","зубы"});
        parts.Add(new List<string>{"улыбка","кофта","юбка","обувь"});
        parts.Add(new List<string>{"лицо","сумки","ботинки","рубашка"});


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(StartTime);
        StartTime += Time.deltaTime;
        timer_Text.text = StartTime.ToString("0.0");

        if(finish){

            time += Time.deltaTime;
            if(time > 2) {
                time = 0.0f;
                MenuGood.SetActive(false);
                finish = false;
                score++;

                //Если есть части, то продолжить, иначе закончить уровень
                if(currentParts.Count > 0) {
                    int randomInt = Random.Range(0, currentParts.Count);
                    currentPart =  currentParts[randomInt];
                    currentParts.RemoveAt(randomInt);
                    

                    task_Text.text = "Найди: " + currentPart;
                    score_Text.text = ("Score: " + score.ToString());
                }else{
                    delete_lvl();

                    WinMenu.SetActive(true);

                    WinMenu.transform.FindChild("time").gameObject.GetComponent<TextMeshProUGUI>().text = "Время: " + StartTime.ToString("0.0") +  " сек";
                
                    WinMenu.transform.FindChild("time2").gameObject.GetComponent<TextMeshProUGUI>().text = "Ср. время: " + (StartTime/parts[Dropdown1.value].Count).ToString("0.0") +  " сек";
                    WinMenu.transform.FindChild("mistakes").gameObject.GetComponent<TextMeshProUGUI>().text = "Ошибок: " + mistakes.ToString();
                    
                    mistakes = 0;


                    // StartTime - Время за всю игру
                    // (StartTime/parts[Dropdown1.value].Count) - Ср. время между заданиями
                    // mistakes - ошибки 
                    // Запись в БД ()


                }
            }
        }
    }


    public void add_lvl(int number = -1){

        if(number == -1) number = Dropdown1.value;
        
        lvl = Instantiate(lvls[number], gameObject.transform.position, Quaternion.identity,gameObject.transform);


        foreach (string item in parts[number])
        {
            currentParts.Add(item);
        }
        

        // Взятие случайной части тела или лица, текущего уровня
        int randomInt = Random.Range(0, currentParts.Count);
        currentPart =  currentParts[randomInt];
        currentParts.RemoveAt(randomInt);

        // Запуск таймера
        StartTime = 0.0f;
        task_Text.text = "Найди: " + currentPart;
        score_Text.text = ("Score: " + score.ToString());
    }
    public void delete_lvl(){
       Destroy(lvl);
    }
}
