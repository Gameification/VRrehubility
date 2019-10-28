using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;


public class Database_Manager : MonoBehaviour
{
    public string server = "127.0.0.1";
    public string database = "vrdb";
    public string  uid = "root";
    public string  password = "";
    public string connectionString;

    // Start is called before the first frame update
    void Start()
    {
        connectionString = @"Host=127.0.0.1;UserName=root;Password=;Database=vrdb;";
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        //Debug.Log("database connected");
        //myConnection.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
