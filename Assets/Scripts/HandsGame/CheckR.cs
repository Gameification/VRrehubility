using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckR : MonoBehaviour
{
    public static bool CheckRH;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "HandRigth")
        {
            CheckRH = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HandRigth")
        {
            CheckRH = false;
            HandALL.rCheck = false;
            HandALL.RtimeLeft = 1.5f;
            HandALL.RprogressBarIm.fillAmount = HandALL.RprogressBar = 0;
        }
    }
}
