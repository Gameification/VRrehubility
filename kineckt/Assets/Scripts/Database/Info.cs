using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public Dbconnect db = null;
    void Start()
    {
        db = Camera.main.GetComponent<Dbconnect>();
    }

    public void ViewInfo(ItemDb item)
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
}
