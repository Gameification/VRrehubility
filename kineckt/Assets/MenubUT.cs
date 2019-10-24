using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenubUT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBut()
    {
        for (int i = 0; i < GameObject.Find("Canvas").transform.GetChild(1).childCount; i++)
        {
            if (GameObject.Find("Canvas").transform.GetChild(1).GetChild(i).gameObject.activeSelf)
                GameObject.Find("Canvas").transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
            else GameObject.Find("Canvas").transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < GameObject.Find("Canvas").transform.GetChild(2).childCount; i++)
        {
            if (GameObject.Find("Canvas").transform.GetChild(2).gameObject.activeSelf)
                GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
            else GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
