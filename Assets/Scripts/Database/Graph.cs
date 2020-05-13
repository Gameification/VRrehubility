using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Dbconnect db = null;
    public LineRenderer lr;
    public float x = 0; 
    public Transform graphStart;
    public List<float> y= new List<float>();
    // Start is called before the first frame update
    void Start()
    { 
        db = Camera.main.GetComponent<Dbconnect>();
        lr = GameObject.Find("Graph").GetComponent<LineRenderer>();
        graphStart = lr.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void graph()
    {
        findResults();
        int index = transform.parent.GetSiblingIndex();
        Debug.Log("GRAPH");
        lr.positionCount = 0;
        lr.positionCount = y.Count;
        x = graphStart.position.x;
        lr.SetPosition(0, new Vector3(x, 0, -1));
        for (int i = 1; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, new Vector3(x+=i, y[i], -1));
        }
    }
    public void findResults()
    {
        y.Clear();
        int index = transform.GetSiblingIndex();
        int current_id = db._items[index]._id;
        for (int i = 0; i < db.newResult._id.Count; i++) 
        {
            if (current_id == db.newResult._id[i]) 
            {
                y.Add(db.newResult._score[i]*0.1f);
            }
        }
    }

}
