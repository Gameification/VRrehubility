using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AroundObj2 : MonoBehaviour
{
    float currCountdownValue;
    public static float timeLeft = 1.5f;
    public static float progressBar = 0;
    public Image progressBarIm1;
    public static bool lCheck = false;
    public LayerMask mask;
    public LayerMask mask2;
    public bool screenon = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, -0.5f);
        if (AroundObj.rCheck == true && lCheck == true && !screenon && SaveTrial.checkScreenshot) 
        {
            this.transform.localPosition = new Vector2(
            Random.Range(-100, -250),
            Random.Range(50, -50));
            progressBarIm1.fillAmount = progressBar = 0;
            Kinect.score += 100;           
            screenon = true;
            Camera.main.cullingMask = mask;
            SaveTrial.checkScreenshot = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "HandLeft" && this.tag == "Hand_pose_left")
        {
            timeLeft -= Time.deltaTime;
            progressBar += Time.deltaTime;
            progressBarIm1.fillAmount = progressBar;
            if (timeLeft < 0)
            {
                lCheck = true;
                timeLeft = 1.5f;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timeLeft = 1.5f;
        progressBar = 0;
        progressBarIm1.fillAmount = progressBar;
        lCheck = false;

    }


}
