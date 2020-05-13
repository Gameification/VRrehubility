using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    public Dbconnect db = null;
    void Start()
    {
        db = Camera.main.GetComponent<Dbconnect>();
    }

    public void ViewUserInfo(ItemDb item)
    {
         GameObject info = GameObject.Find("Database").transform.GetChild(2).GetChild(1).gameObject;
         info.SetActive(true);
         info.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = item._surname;
         info.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = item._firstname;
         info.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = item._middlename;
    }
    
    public void ViewInfoButton()
    {
        GameObject info = GameObject.Find("Database").transform.GetChild(2).GetChild(1).gameObject;
        info.SetActive(true);
        int index = transform.GetSiblingIndex();
        info.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = db._items[index]._surname;
        info.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = db._items[index]._firstname;
        info.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = db._items[index]._middlename;
    }
    public void ViewResultsInfo()
    {
        int index = transform.GetSiblingIndex();
        //float Max_score = db.newResult._score[0];
        //for (int i = 0; i < db._items.Count; i++) 
        //{ 
        //    if (db.newResult._score[i + 1] > Max_score) Max_score = db.newResult._score[i + 1];
        //}
        //Debug.Log(Max_score);
        //db._sqlQuery = "SELECT MAX(Score) FROM users WHERE id_player = '" + index + "'";
        //db._reader = db.set_cmd("SELECT * FROM results WHERE id_player = '" + index + 1 + "' SELECT MAX(Score) FROM results WHERE id_player = '" + index+1 + "'").ExecuteReader();
        //while (db._reader.Read())
        //{
        //    Max_score = db._reader.GetFloat(0);
        //    Debug.Log(Max_score);
        //}
        int count = 0;
        for (int i = 0; i < db.newResult._id.Count; i++)
        {
            if (db.newResult._id[i] == index+1)
            {
                count++;
            }
        }
        GameObject ResultsInfo = GameObject.Find("Database").transform.GetChild(0).GetChild(4).gameObject;
        ResultsInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Кол-во прохождений: " + count.ToString();
    }
}
