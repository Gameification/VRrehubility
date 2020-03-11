using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using TMPro;

public class Functions : MonoBehaviour
{
    public GameObject _prefab;
    public Text _inputSname; 
    public Text _inputFname; 
    public Text _inputMname; 
    private Dbconnect db;
    private Info info;
    string _conn;
    void Start()
    { 
        info = Camera.main.GetComponent<Info>();
        db = Camera.main.GetComponent<Dbconnect>();
        _conn = "URI=file:" + Application.dataPath + "/VRdb.s3db"; //Path to database.
        db.set_connection(_conn);
    }
    public void AddToScene(string surname, string firstname, string middlename) // добавить игрока на сцену
    {
        Transform tr = GameObject.Find("Database").transform.GetChild(1).GetChild(0).transform;
        GameObject Item = Instantiate(_prefab, tr);
        Item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = surname + " " + firstname[0] + ".";
    }
    public void UpdateScroll() // обновление листа из БД
    {
        Clear();
        for (int i = 0; i < db._items.Count; i++)
        {
            AddToScene(db._items[i]._surname, db._items[i]._firstname, db._items[i]._middlename);
        }
    }
    public void Clear() 
    {
        GameObject content = GameObject.Find("Database").transform.GetChild(1).GetChild(0).gameObject;
        for (int i = 0; i < content.transform.childCount; i++) 
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    public void AddNewPlayer() // добавить нового игрока
    {
        bool check = false;
        if (_inputSname.text != "" && _inputFname.text != "" && _inputMname.text != "")
        {
            foreach (ItemDb item in db._items)
            {
                if (_inputSname.text == item._surname && _inputFname.text == item._firstname && _inputMname.text == item._middlename) { check = true; ViewMessage("Игрок уже существует"); break; }
            }
             if (check == true)
             {

             }
             else
             {
                AddToScene(_inputSname.text, _inputFname.text, _inputMname.text); // добавить нового игрока на сцену
                db._items.Add(new ItemDb(_inputSname.text.ToString(), _inputFname.text.ToString(), _inputMname.text.ToString()));
                string _sqlQuery = "INSERT INTO users (Surname, Firstname, Middlename) VALUES('" + db._items[db._items.Count - 1]._surname.ToString() + "','" + db._items[db._items.Count - 1]._firstname + "','" + db._items[db._items.Count - 1]._middlename + "')";
                db._dbcmd = db.set_cmd(_sqlQuery);
                db._dbcmd.ExecuteNonQuery();
             }
         }
     }
    public void ViewMessage(string message)
    {
        GameObject _message = GameObject.Find("Database").transform.GetChild(4).gameObject;
        _message.SetActive(true);
        _message.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = message;
    }
    public void Search()
    {
        Debug.Log(_inputSname.text + _inputFname.text + _inputMname.text);
        for (int i = 0; i < db._items.Count; i++)
        {
            if (_inputSname.text == db._items[i]._surname && _inputFname.text == db._items[i]._firstname && _inputMname.text == db._items[i]._middlename)
            {
                info.ViewInfo(db._items[i]);
                break;
            }
            else if (i == db._items.Count - 1) ViewMessage("Игрок не найден");
            }
    }
}
