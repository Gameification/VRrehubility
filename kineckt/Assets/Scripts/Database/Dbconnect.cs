using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System;

public class Dbconnect : MonoBehaviour
{
   // public List<results> _results = new List<results>();
    public ResultList newResult;
    public List<ItemDb> _items = new List<ItemDb>();
    public IDbConnection _dbconn;
    public IDbCommand _dbcmd;
    public IDataReader _reader;
    public string _sqlQuery;
    string _conn;
    void Start()
    {
        newResult = new ResultList();
        _conn = "URI=file:" + Application.dataPath + "/VRdb.s3db"; //Path to database.
        set_connection(_conn);
        _sqlQuery = "SELECT id_player, Surname, Firstname, Middlename FROM users";
        read_data(set_cmd(_sqlQuery));
        _sqlQuery = "SELECT id_player, Score, Time, Sens, Accuracy FROM results";
        read_result(set_cmd(_sqlQuery));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void set_connection(string _conn)
     {
        _dbconn = (IDbConnection)new SqliteConnection(_conn);
        _dbconn.Open(); //Open connection to the database.
     }
     public void close_connection()
     {
        _reader.Close();
        _reader = null;
        _dbcmd.Dispose();
        _dbcmd = null;
        _dbconn.Close();
        _dbconn = null;
     }
    public IDbCommand set_cmd(string sqlQuery)
    {
        _dbcmd = _dbconn.CreateCommand();
        _dbcmd.CommandText = sqlQuery;
        return _dbcmd;
    }
    public void read_data(IDbCommand _dbcmd)
    {
        _reader = _dbcmd.ExecuteReader();
        while (_reader.Read())
        {
            int _id = _reader.GetInt32(0);
            string _surname = _reader.GetString(1);
            string _firstname = _reader.GetString(2);
            string _middlename = _reader.GetString(3);
            _items.Add(new ItemDb(_id,_surname, _firstname, _middlename));
        }
    }
    public void read_result(IDbCommand _dbcmd)
    {
        _reader = _dbcmd.ExecuteReader();
        while (_reader.Read())
        {
            int _id = _reader.GetInt32(0);
            float _score = _reader.GetFloat(1);
            float _time = _reader.GetFloat(2);
            float _sens = _reader.GetFloat(3);
            float _accuracy = _reader.GetFloat(4);
            newResult.Add(_id, _score, _time, _sens, _accuracy);
        }
    }
    public void edit_data(string sqlQuery)
    {
        set_cmd(sqlQuery);
        _dbcmd.ExecuteNonQuery();
    }
    public void show_results()
    {
        int index = transform.GetSiblingIndex();
        //_conn = "URI=file:" + Application.dataPath + "/VRdb.s3db"; //Path to database.
        //set_connection(_conn);
        ////_sqlQuery = "SELECT id_player, Score, Time, Sens, Accuracy FROM results WHERE id_player='" + obj._id + "'";
        //read_result(set_cmd(_sqlQuery));
        //close_connection();
        //foreach (results tmp in _results) 
        //{
        //    if (tmp._id == _items[index]._id) { Debug.Log("loh"); }
        //}
    }
    public void drop_player()
    {
        int index = transform.GetSiblingIndex();
        Dbconnect bd = Camera.main.GetComponent<Dbconnect>();
        //Debug.Log(bd._items[index]._id);
        _conn = "URI=file:" + Application.dataPath + "/VRdb.s3db"; //Path to database.
        set_connection(_conn);
        _sqlQuery = "DELETE FROM results WHERE id_player='" + bd._items[index]._id + "'";
        edit_data(_sqlQuery);
        _sqlQuery = "DELETE FROM users WHERE id_player='" + bd._items[index]._id + "'";
        edit_data(_sqlQuery);
        bd._items.RemoveAt(index);
        Functions f = Camera.main.GetComponent<Functions>();
        f.UpdateScroll();
    }
}

[System.Serializable]
public class ItemDb
{
    public int _id;
    public string _surname;
    public string _firstname;
    public string _middlename;

    public ItemDb(int id, string surname, string firstname, string middlename) 
    {
        _id = id;
        _surname = surname;
        _firstname = firstname;
        _middlename = middlename;
    }
    public ItemDb(string surname, string firstname, string middlename)
    {
        _surname = surname;
        _firstname = firstname;
        _middlename = middlename;
    }
}

//public class results
//{
//    public int _id;
//    public float _score;
//    public float _time;
//    public float _sens;
//    public float _accuracy;

//    public results(int id, float score, float time, float sens, float accuracy) 
//    {
//        _id = id;
//        _score = score;
//        _time = time;
//        _sens = sens;
//        _accuracy = accuracy;
//    }
//}
[System.Serializable]
public class ResultList
{
    public List<int> _id;
    public List<float> _score;
    public List<float> _time;
    public List<float> _sens;
    public List<float> _accuracy;
    public ResultList() 
    { 
        _id = new List<int>();
        _score = new List<float>();
        _time = new List<float>();
        _sens = new List<float>();
        _accuracy = new List<float>();
    }
    public void Add(int id, float score, float time, float sens, float accuracy) 
    {
        _id.Add(id);
        _score.Add(score);
        _time.Add(time);
        _sens.Add(sens);
        _accuracy.Add(accuracy);
    }
}