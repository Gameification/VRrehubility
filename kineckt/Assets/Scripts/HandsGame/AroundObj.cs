using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AroundObj : MonoBehaviour
{
    float currCountdownValue;
    public static float timeLeft = 1.5f;
    public static float progressBar = 0;
    public Image progressBarIm1;
    public static bool rCheck = false;
    public LayerMask mask;
    public LayerMask mask2;
    public bool screenon = false;
    private int count;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, +0.5f);
        if(AroundObj2.lCheck == true && rCheck == true && !screenon)
        {
            screenon = true;
            SaveTrial.Save(mask2);

        }
        if (AroundObj2.lCheck == true && rCheck == true && screenon && SaveTrial.checkScreenshot)
        {
            {
                float a = 0;
                while (a < 1000) { a += 0.1f; Debug.Log(a); }
                this.transform.localPosition = new Vector2(
                Random.Range(100, 250),
                Random.Range(50, -50));
                progressBarIm1.fillAmount = progressBar = 0;
                Kinect.score += 100;
                Camera.main.cullingMask = mask;
                screenon = false;
                SaveTrial.checkScreenshot = false;
            }
        }    
    }

 

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "HandRigth" && this.tag == "Hand_pose_rigth")
        {
            timeLeft -= Time.deltaTime;
            progressBar += Time.deltaTime;
            progressBarIm1.fillAmount = progressBar;
            if (timeLeft < 0)
            {
                rCheck = true;
                timeLeft = 1.5f;
            }
        }


       
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        timeLeft = 1.5f;
        progressBar = 0;
        progressBarIm1.fillAmount = progressBar;
        rCheck = false;
    }


    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
    }
}